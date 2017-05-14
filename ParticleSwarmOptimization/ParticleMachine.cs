using System;
using System.Collections.Generic;
using System.Linq;

namespace ParticleSwarmOptimization
{
    public abstract class ParticleMachine<DataType>
    {
        private Func<DataType, double> FitnessFunction { get; set; }
        private ParticleHandler<DataType> BestParticle { get; set; }
        private ParticleHandler<DataType>[] Particles { get; set; }

        public ParticleMachine(Func<DataType, double> fitness, IEnumerable<DataType> particles)
        {
            FitnessFunction = fitness ?? throw new ArgumentNullException("fitness");

            if (particles == null)
                throw new ArgumentNullException("particles");

            if (particles.Count() < 2)
                throw new ArgumentException("At least 2 particles required", "particles");

            SetParticles(particles);
            UpdateFitness();
        }

        protected abstract DataType Move(DataType particle, DataType target);
        protected abstract bool TooClose(DataType q, DataType r, double limit);
        protected abstract DataType Orbit(DataType particle, DataType target, double radius);

        public abstract double ProximityLimit { get; protected set; }
        public abstract double OrbitRadius { get; protected set; }

        public void Advance()
        {
            foreach (ParticleHandler<DataType> particle in Particles)
                if (particle != BestParticle)
                    particle.Model = Move(particle.Model, BestParticle.Model);

            int particlesCount = Particles.Count();

            foreach (int primary in Enumerable.Range(0, particlesCount))
                foreach (int secondary in Enumerable.Range(primary + 1, particlesCount - primary - 1))
                {
                    ParticleHandler<DataType> secondaryParticle = Particles[secondary];

                    if (secondaryParticle == BestParticle)
                        continue;

                    if (TooClose(secondaryParticle.Model, Particles[primary].Model, ProximityLimit))
                        secondaryParticle.Model = Orbit(secondaryParticle.Model, BestParticle.Model, OrbitRadius);
                }

            UpdateFitness();
        }

        private void SetParticles(IEnumerable<DataType> particles)
        {
            Particles = particles.Select(p => new ParticleHandler<DataType> { Fitness = double.MaxValue, Model = p }).ToArray();
        }

        private void UpdateFitness()
        {
            ParticleHandler<DataType> best = null;

            foreach (ParticleHandler<DataType> particle in Particles)
            {
                particle.Fitness = FitnessFunction.Invoke(particle.Model);

                if (best == null || particle.Fitness < best.Fitness)
                    best = particle;
            }

            BestParticle = best;
        }

        public DataType GetBestParticle()
        {
            return BestParticle.Model;
        }

        public IEnumerable<DataType> GetParticles()
        {
            return Particles.Select(p => p.Model);
        }
    }
}
