using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public class ParticleManager<DataType>
    {
        private double DiscoveryRange = 10;

        public IParticle<DataType> BestParticle { get { return Particles.FirstOrDefault(); } }
        public IParticle<DataType>[] Particles { get; private set; }

        public ParticleManager(IEnumerable<IParticle<DataType>> particles)
        {
            if (particles == null)
                throw new ArgumentNullException("particles");

            if (particles.Count() < 2)
                throw new ArgumentException("ParticleManager requires at least 2 particles to operate on.", "particles");

            Particles = particles.ToArray();
            UpdateFitting();
        }

        public void MoveParticles()
        {
            IParticle<DataType> best = Particles.First();

            foreach (IParticle<DataType> particle in Particles.Skip(1))
            {
                particle.Step(best);

                if (particle.TooClose(best))
                    particle.Orbit(best, ExtendDiscoveryRange());
            }

            foreach (IParticle<DataType> firstParticle in Particles.Skip(1))
                foreach (IParticle<DataType> secondParticle in Particles.Skip(1))
                {
                    if (firstParticle == secondParticle)
                        continue;

                    if (firstParticle.Overlaps(secondParticle))
                        firstParticle.Orbit(best, ExtendDiscoveryRange());
                }

            UpdateFitting();
        }

        private double ExtendDiscoveryRange()
        {
            return DiscoveryRange = Math.Min(double.MaxValue - 10, DiscoveryRange * (1 + 1 / (double)Particles.Count()));
        }

        private void UpdateFitting()
        {
            Particles = Particles.OrderByDescending(particle => particle.Fitness).ToArray();
        }
    }
}