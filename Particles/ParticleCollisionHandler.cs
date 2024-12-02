using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public class ParticleCollisionHandler : MonoBehaviour
    {
        public new ParticleSystem particleSystem;
        public Action<GameObject, ParticleCollisionEvent> onParticleCollision;
        readonly List<ParticleCollisionEvent> collisionEvents = new();

        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>(true);

            // Exemple de configuration des données de collision
            ParticleSystem.CollisionModule collision = particleSystem.collision;
            collision.sendCollisionMessages = true;
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnParticleCollision(GameObject other)
        {
            if (onParticleCollision == null)
                Debug.LogWarning($"{this}({transform.GetPath(true)}).{nameof(onParticleCollision)}({onParticleCollision.GetType().FullName}) not set", this);
            else
            {
                int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, collisionEvents);
                for (int i = 0; i < numCollisionEvents; i++)
                    onParticleCollision(other, collisionEvents[i]);
            }
        }
    }
}