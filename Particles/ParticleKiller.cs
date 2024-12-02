using UnityEngine;

namespace _UTIL_
{
    internal class ParticleKiller : ParticleModule
    {
        protected override void _OnParticleCollision(GameObject other, ParticleCollisionEvent collision)
        {
            Debug.DrawRay(collision.intersection, Vector3.up, Color.red, 1f);
            Debug.DrawRay(collision.intersection, 10 * collision.normal, Color.red, 1f);

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
            int numParticlesAlive = particleSystem.GetParticles(particles);

            // Variables pour stocker la particule la plus proche
            float closestDistance = float.MaxValue;
            int closestParticleIndex = -1;

            for (int i = 0; i < numParticlesAlive; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                // Calculer la distance de la particule à la position de collision
                float distance = (particle.position - collision.intersection).sqrMagnitude;

                // Vérifier si la particule est "devant" la collision (par rapport à la normale)
                bool isInFront = /*distance < .1f ||*/ Vector3.Dot(particle.velocity, particle.position - collision.intersection) > 0;

                // Garder la particule la plus proche ET devant
                if (/*isInFront &&*/ distance < closestDistance)
                {
                    closestDistance = distance;
                    closestParticleIndex = i;
                }
            }

            // Tuer la particule la plus proche
            if (closestParticleIndex != -1)
            {
                particles[closestParticleIndex].remainingLifetime = 0;
                Debug.DrawRay(particles[closestParticleIndex].position, Vector3.up, Color.black, 1f);
            }

            // Appliquer les modifications
            particleSystem.SetParticles(particles, numParticlesAlive);
        }
    }
}