using System;
using System.Collections.Generic;
using ParticleSwarmOptimization;
using System.Numerics;

namespace ParticleSwarmOptimizationFront
{
    class Vector2Machine : ParticleMachine<Vector2>
    {
        private static Random Rand = new Random();

        public Vector2Machine(Func<Vector2, double> fitness, IEnumerable<Vector2> particles) : base(fitness, particles)
        {
            OrbitRadius = 10;
        }

        public override double ProximityLimit { get { return 5; } protected set { } }
        public override double OrbitRadius { get; protected set; }

        protected override Vector2 Move(Vector2 particle, Vector2 target)
        {
            return particle + (target - particle) / new Vector2(10);
        }

        protected override Vector2 Orbit(Vector2 particle, Vector2 target, double radius)
        {
            OrbitRadius += 1;
            Vector2 unnormalized;
            do { unnormalized = new Vector2((float)Rand.NextDouble() * 2 - 1, (float)Rand.NextDouble() * 2 -1); }
            while (unnormalized.Length() <= float.Epsilon);

            return target + Vector2.Normalize(unnormalized) * new Vector2((float)radius);
        }

        protected override bool TooClose(Vector2 q, Vector2 r, double limit)
        {
            return Vector2.DistanceSquared(q, r) <= limit * limit;
        }
    }
}
