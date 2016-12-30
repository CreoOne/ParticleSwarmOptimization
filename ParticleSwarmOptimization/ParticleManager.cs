using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public class ParticleManager<DataType>
    {
        private FitnessPriorityEnum _FitnessPriority = FitnessPriorityEnum.Rising; 
        public FitnessPriorityEnum FitnessPriority
        {
            get { return _FitnessPriority; }
            set
            {
                _FitnessPriority = value;
                UpdateFitting();
            }
        }

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
                particle.Step(best);

            UpdateFitting();
        }

        private void UpdateFitting()
        {
            if (_FitnessPriority == FitnessPriorityEnum.Falling)
                Particles = Particles.OrderBy(particle => particle.Fitness).ToArray();
            else
                Particles = Particles.OrderByDescending(particle => particle.Fitness).ToArray();
        }
    }
}
