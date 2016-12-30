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
        public Simple2DVisualisation()
        {
            InitializeComponent();

            Func<Vector2, double> fittnessFunction = delegate (Vector2 model)
            {
                return -(Vector2.Zero - model).Length();
            };

            Vector2Particle[] particles = new Vector2Particle[20];
            Random rng = new Random(DateTime.Now.Millisecond);

            foreach (int i in Enumerable.Range(0, 20))
                particles[i] = new Vector2Particle(new Vector2(rng.Next(-10, 10), rng.Next(-10, 10)), fittnessFunction);

            ParticleManager<Vector2> manager = new ParticleManager<Vector2>(particles);

            foreach (int i in Enumerable.Range(0, 100))
                manager.MoveParticles();
        }
    }
}
