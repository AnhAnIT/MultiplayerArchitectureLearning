using UnityEngine;
using UnityEngine.InputSystem;
namespace MultiplayCore
{
    public class  SceneCamera : SceneService
    {
        //Public members
        public Camera Camera => _camera;
        public ShakeEffect ShakeEffect => _shakeEffect;
        public bool EnableCamera { get; set; } = true;

        //Private members
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private AudioListener _audioListener;
        [SerializeField]
        private ShakeEffect _shakeEffect;

        private int _cameraCullingMask;

        //ScenceService interface 
        protected override void OnInitialize()
        {
            base.OnInitialize();
            _cameraCullingMask = _camera.cullingMask;
        }

        protected override void OnTick()
        {
            if(Scene is Gameplay)
            {
                _audioListener.enabled = Context.HasInput;
                _camera.enabled = Context.HasInput;

                // Disable rendering for other players cameras, but keep the camera enabled for shake effect and audio listener
                _camera.cullingMask = EnableCamera == true ? _cameraCullingMask : 0;
            }
        }
    }
}