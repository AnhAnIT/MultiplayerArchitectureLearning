using MultiplayCore;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace MultiplayCore.UI
{
    public interface IDelayBlurView
    {
        int DelayFrames { get; }
    }

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIView : UIWidget, IBackHandler
    {
        //Public members
        public event Action HasOpened;
        public event Action HasClosed;

        //Private members
        [SerializeField]
        private bool _canHandleBackAction;
        [SerializeField]
        private bool _needsCursor;
        [SerializeField]
        private bool _useSafeArea = true;
        private int _priority;
        private Rect _lastSafeArea;
    }
}