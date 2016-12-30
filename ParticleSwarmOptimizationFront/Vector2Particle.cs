using ParticleSwarmOptimization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimizationFront
{
    class Vector2Particle : IParticle<Vector2>
    {
        private Func<Vector2, double> FitnessFunction { get; set; }
        public double Fitness { get; private set; }
        public List<Vector2> History { get; private set; }
        public Vector2 Model { get; private set; }

        public Vector2Particle(Vector2 model, Func<Vector2, double> fitnessFunction)
        {
            if (fitnessFunction == null)
                throw new ArgumentNullException("fitnessFunction");

            Model = model;
            FitnessFunction = fitnessFunction;
            History = new List<Vector2>();
            UpdateFitness();
        }

        public void Step(IParticle<Vector2> target)
        {
            Model = Model + (target.Model - Model) / new Vector2(10);
            UpdateFitness();
        }

        private void UpdateFitness()
        {
            History.Add(Model);
            Fitness = FitnessFunction.Invoke(Model);
        }
    }
}
