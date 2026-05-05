using MultiplayCore;
using UnityEngine;
using UnityEngine.UIElements;

namespace MultiplayCore
{
    public sealed class RotateToCamera : CoreBehavior
    {
        // PRIVATE MEMBERS

        private Transform _cameraTransform;

        // MONOBEHAVIOUR

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (_cameraTransform != null)
            {
                transform.rotation = Quaternion.LookRotation(_cameraTransform.position - transform.position);
            }
        }
    }
}
