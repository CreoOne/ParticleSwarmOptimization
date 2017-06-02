using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public abstract class ParticleMachine<DataType>
    {
        private OperationMode Mode { get; set; }
        private Func<DataType, double> FitnessFunction { get; set; }
        private ParticleHandler<DataType> BestParticle { get; set; }
        private ParticleHandler<DataType>[] Particles { get; set; }

        public ParticleMachine(Func<DataType, double> fitness, IEnumerable<DataType> particles, OperationMode mode)
        {
            FitnessFunction = fitness ?? throw new ArgumentNullException("fitness");
            Mode = mode;
            SetParticles(particles);
        }

        protected abstract DataType Move(DataType particle, DataType target);
        protected abstract bool TooClose(DataType q, DataType r, double limit);
        protected abstract DataType Orbit(DataType particle, DataType target, double radius);

        public abstract double ProximityLimit { get; protected set; }
        public abstract double OrbitRadius { get; protected set; }

        public void Advance()
        {
            Move();
            Collide();
            UpdateFitness();
        }

        private void Move()
        {
            Action<ParticleHandler<DataType>> moveAction = particle =>
            {
                if (particle != BestParticle)
                    particle.Model = Move(particle.Model, BestParticle.Model);
            };

            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(Particles, parallelOptions, moveAction);
        }

        private void Collide()
        {
            Action<int> collideAction = primary =>
            {
                foreach (int secondary in Enumerable.Range(primary + 1, Particles.Length - primary - 1))
                {
                    ParticleHandler<DataType> secondaryParticle = Particles[secondary];

                    if (secondaryParticle == BestParticle)
                        return;

                    if (TooClose(secondaryParticle.Model, Particles[primary].Model, ProximityLimit))
                        secondaryParticle.Model = Orbit(secondaryParticle.Model, BestParticle.Model, OrbitRadius);
                }
            };

            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(Enumerable.Range(0, Particles.Length), parallelOptions, collideAction);
        }

        private void UpdateFitness()
        {
            ParticleHandler<DataType> best = null;

            foreach (ParticleHandler<DataType> particle in Particles)
            {
                particle.Fitness = FitnessFunction.Invoke(particle.Model);

                if (IsSuperrior(particle, best))
                    best = particle;
            }

            BestParticle = best;
        }

        private bool IsSuperrior(ParticleHandler<DataType> particleInQuestion, ParticleHandler<DataType> currentBestParticle)
        {
            if (currentBestParticle == null)
                return true;

            if (Mode == OperationMode.Minimization && particleInQuestion.Fitness < currentBestParticle.Fitness)
                return true;

            if (Mode == OperationMode.Maximization && particleInQuestion.Fitness > currentBestParticle.Fitness)
                return true;

            return false;
        }

        private void SetParticles(IEnumerable<DataType> particles)
        {
            if (particles == null)
                throw new ArgumentNullException("particles");

            if (particles.Count() < 2)
                throw new ArgumentException("At least 2 particles required", "particles");

            Particles = new ParticleHandler<DataType>[] { };
            AddParticles(particles);
        }

        public void AddParticle(DataType particle)
        {
            AddParticles(new[] { particle });
        }

        public void AddParticles(IEnumerable<DataType> particles)
        {
            Particles = Particles.Concat(particles.Select(p => new ParticleHandler<DataType> { Model = p })).ToArray();
            UpdateFitness();
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
