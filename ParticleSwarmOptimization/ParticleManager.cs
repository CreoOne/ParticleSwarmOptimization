using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public class ParticleManager<DataType>
    {
        public double TooClose { get; private set; }
        private double DiscoveryRange;

        public IParticle<DataType> BestParticle { get { return Particles.FirstOrDefault(); } }
        public IParticle<DataType>[] Particles { get; private set; }

        public ParticleManager(IEnumerable<IParticle<DataType>> particles, double tooClose)
        {
            if (particles == null)
                throw new ArgumentNullException("particles");

            if (particles.Count() < 2)
                throw new ArgumentException("ParticleManager requires at least 2 particles to operate on.", "particles");

            if (tooClose <= 0)
                throw new ArgumentOutOfRangeException("tooClose");

            TooClose = tooClose;
            DiscoveryRange = TooClose;
            Particles = particles.ToArray();
            UpdateFitting();
        }

        public void MoveParticles()
        {
            IParticle<DataType> best = Particles.First();

            foreach (IParticle<DataType> particle in Particles.Skip(1))
            {
                particle.Step(best);

                if (particle.TooClose(best, TooClose))
                    particle.Orbit(best, ExtendDiscoveryRange());
            }

            UpdateFitting();
        }

        private double ExtendDiscoveryRange()
        {
            return DiscoveryRange = Math.Min(float.MaxValue - 1, DiscoveryRange + (1 + 1 / (double)Particles.Count()));
        }

        private void UpdateFitting()
        {
            Particles = Particles.OrderByDescending(particle => particle.Fitness).ToArray();
        }
    }
}