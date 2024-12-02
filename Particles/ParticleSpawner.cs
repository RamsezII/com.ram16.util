using UnityEngine;

namespace _UTIL_
{
    internal class ParticleSpawner : ParticleModule
    {
        protected override void _OnParticleCollision(GameObject other, ParticleCollisionEvent collision)
        {
            //ParticleSystem.EmitParams emitParams = new()
            //{
            //    position = collision.intersection,
            //    velocity = Vector3.zero,
            //};
            //particleSystem.Emit(emitParams, 1);
            //particleSystem.Play();
        }
    }
}