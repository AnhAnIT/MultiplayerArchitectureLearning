using System;
using System.Collections.Generic;
using UnityEngine;
using MultiplayCore;

namespace MultiplayCore.UI
{
    public class SceneUI : SceneService , IBackHandler
    {
        //Public members
        public Canvas Canvas { get; private set; }
        public Camera UICamera { get; private set; }

        //Private members
        [SerializeField]
        private UIView[] _defaultViews;
        [SerializeField]
        private AudioEffect[] _audioEffects;
        [SerializeField]
        private AudioSetup _clickSound;

        private ScreenOrientation _lastScreenOrientation;

        // SceneUI INTERFACE

        protected UIView[] _views;
        protected virtual void OnInitializeInternal() { }
        protected virtual void OnDeinitializeInternal() { }
        protected virtual void OnTickInternal() { }
        protected virtual bool OnBackAction() { return false; }
        protected virtual void OnViewOpened(UIView view) { }
        protected virtual void OnViewClosed(UIView view) { }

        //Public methods
        public T Get<T>() where T : UIView
        {
            if (_views == null)
                return null;
            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;

                if (view != null)
                    return view;
            }
            return null;
        }

        public T Open<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    OpenView(view);
                    return view;
                }
            }

            return null;
        }

        public void Open(UIView view)
        {
            if (_views == null)
                return;

            int index = Array.IndexOf(_views, view);

            if (index < 0)
            {
                Debug.LogError($"Cannot find view {view.name}");
                return;
            }

            OpenView(view);
        }
        public T Close<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    view.Close();
                    return view;
                }
            }

            return null;
        }
        public void Close(UIView view)
        {
            if (_views == null)
                return;

            int index = Array.IndexOf(_views, view);

            if (index < 0)
            {
                Debug.LogError($"Cannot find view {view.name}");
                return;
            }

            CloseView(view);
        }
        public T Toggle<T>() where T : UIView
        {
            if (_views == null)
                return null;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    if (view.IsOpen == true)
                    {
                        CloseView(view);
                    }
                    else
                    {
                        OpenView(view);
                    }

                    return view;
                }
            }

            return null;
        }
        public bool IsOpen<T>() where T : UIView
        {
            if (_views == null)
                return false;

            for (int i = 0; i < _views.Length; ++i)
            {
                T view = _views[i] as T;
                if (view != null)
                {
                    return view.IsOpen;
                }
            }

            return false;
        }

        public bool IsTopView(UIView view , bool interactableOnly = false)
        {
            if (view.IsOpen == false)
                return false;
            if (_views == null)
                return false;
            int highestPriority = -1;
            for (int i = 0; i < _views.Length; ++i)
            {
                var otherView = _views[i];
                if (otherView == view)
                    continue;

                if (otherView.IsOpen == false)
                    continue;

                if (interactableOnly == true && otherView.IsInteractable == false)
                    continue;
                highestPriority = Math.Max(highestPriority, otherView.Priority);
            }

            return view.Priority > highestPriority;
        }
        public void CloseAll()
        {
            if (_views == null)
                return;

            for (int i = 0; i < _views.Length; ++i)
            {
                CloseView(_views[i]);
            }
        }

        public void GetAll<T>(List<T> list)
        {
            if (_views == null)
                return;

            for (int i = 0; i < _views.Length; ++i)
            {
                if (_views[i] is T element)
                {
                    list.Add(element);
                }
            }
        }

        public bool PlaySound(AudioSetup effectSetup , EForceBehaviour force = EForceBehaviour.None)
        {
            return _audioEffects.PlaySound(effectSetup, force);
        }

        public bool PlayClickSound()
        {
            return PlaySound(_clickSound);
        }

        // IBackHadler interface 
        int IBackHandler.Priority =>  1;
        bool IBackHandler.IsActive => true;

        bool IBackHandler.OnBackAction() { return OnBackAction(); }


        //GameService interface
        protected sealed override void OnInitialize()
        {
            base.OnInitialize();
            Canvas = GetComponent<Canvas>();
            UICamera = Canvas.worldCamera;
            _views = GetComponentsInChildren<UIView>(true);

            for (int i = 0; i < _views.Length; ++i)
            {
                UIView view = _views[i];

                view.Initialize(this, null);
                view.SetPriority(i);

                view.gameObject.SetActive(false);
            }

        }


    }
}