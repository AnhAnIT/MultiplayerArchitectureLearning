using UnityEngine;
namespace MultiplayCore
{
    public class CoreBehavior : MonoBehaviour
    {
        //Private members
        private string _cachedName;
        private bool _nameCached;
        private GameObject _cachedGameObject;
        private bool _gameObjectCached;
        private Transform _cachedTransform;
        private bool _transformCached;

        //Public members
        public new string name
        {
            get { 


#if UNITY_EDITOR
                if(Application.isPlaying == false)
                {
                    return base.name;
                }
#endif
                if (_nameCached == false)
                {
                    _cachedName = base.name;
                    _nameCached = true;
                }
                return _cachedName;
            }
         }


        public new GameObject gameObject
        {
            get
            {


#if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    return base.gameObject;
                }
#endif
                if (_gameObjectCached == false)
                {
                   _cachedGameObject = base.gameObject;
                    _gameObjectCached = true;
                }
                return _cachedGameObject;
            }
        }

        public new Transform transform
        {
            get
            {


#if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    return base.transform;
                }
#endif
                if (_transformCached == false)
                {
                    _cachedTransform = base.transform;
                    _gameObjectCached = true;
                }
                return _cachedTransform;
            }
        }

    }
}