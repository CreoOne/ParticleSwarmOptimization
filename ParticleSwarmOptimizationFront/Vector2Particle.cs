using ParticleSwarmOptimization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
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

        private void UpdateFitness()
        {
            Fitness = FitnessFunction.Invoke(Model);
        }

        public void Step(IParticle<Vector2> target)
        {
            History.Add(Model);
            Model = Model + (target.Model - Model) / new Vector2(10);
            UpdateFitness();
        }

        public bool TooClose(IParticle<Vector2> target, double distance)
        {
            return (Model - target.Model).Length() < distance;
        }

        public void Orbit(IParticle<Vector2> target, double radius)
        {
            double theta = GetUniformRandUnit() * Math.PI * 2;
            History.Add(Model);
            Model = target.Model + new Vector2((float)(Math.Sin(-theta) * radius), (float)(Math.Cos(-theta) * radius));
            UpdateFitness();
        }

        private double GetUniformRandUnit()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[sizeof(double)];
                rng.GetBytes(bytes);
                // bit-shift 11 and 53 based on double's mantissa bits
                var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                return ul / (double)(1UL << 53);
            }
        }
    }
}
