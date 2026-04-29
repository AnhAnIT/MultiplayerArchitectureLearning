using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace MultiplayCore
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool SpawnEnabled = true;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, 2.0f);
        }
    }
}