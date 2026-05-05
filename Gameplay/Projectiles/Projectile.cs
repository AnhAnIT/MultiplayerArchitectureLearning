using Fusion;
using MultiplayCore;
using UnityEngine;

namespace MultiplayCore
{
    public abstract class Projectile : ContextBehaviour
    {
        // PUBLIC METHODS

        public abstract void Fire(NetworkObject owner, Vector3 firePosition, Vector3 initialVelocity, LayerMask hitMask, EHitType hitType);
    }
}
