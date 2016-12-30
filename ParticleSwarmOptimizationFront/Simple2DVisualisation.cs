using ParticleSwarmOptimization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleSwarmOptimizationFront
{
    public partial class Simple2DVisualisation : Form
    {
        private ParticleManager<Vector2> ParticleManager;
        private Func<Vector2, double> FitnessFunction;

        public Simple2DVisualisation()
        {
            InitializeComponent();

            FitnessFunction = (Vector2 model) => { return (Vector2.Zero - model).Length(); };
            GenerateParticles(20);
        }

        public void GenerateParticles(int amount)
        {
            Vector2Particle[] particles = new Vector2Particle[amount];
            Random rng = new Random(DateTime.Now.Millisecond);

            foreach (int i in Enumerable.Range(0, amount))
                particles[i] = new Vector2Particle(new Vector2(rng.Next(-10, 10), rng.Next(-10, 10)), FitnessFunction);

            ParticleManager<Vector2> manager = new ParticleManager<Vector2>(particles);
            manager.FitnessPriority = FitnessPriorityEnum.Falling;
        }
    }
}
