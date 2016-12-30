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
        private Bitmap FitnessBitmap;

        public Simple2DVisualisation()
        {
            InitializeComponent();

            FitnessFunction = (Vector2 model) => { return Math.Max(0, Math.Min(255-(new Vector2(50, 50) - model).Length(), 255)); };
        }

        public void GenerateParticles(int amount)
        {
            Vector2Particle[] particles = new Vector2Particle[amount];
            Random rng = new Random(DateTime.Now.Millisecond);

            foreach (int i in Enumerable.Range(0, amount))
                particles[i] = new Vector2Particle(new Vector2(rng.Next(0, fitnessMap.Width), rng.Next(0, fitnessMap.Height)), FitnessFunction);

            ParticleManager = new ParticleManager<Vector2>(particles);
            ParticleManager.FitnessPriority = FitnessPriorityEnum.Rising;
        }

        private Bitmap GenerateFitnessBitmap()
        {
            Bitmap result = new Bitmap(fitnessMap.Width, fitnessMap.Height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                float sampleSize = 5;

                int ySamples = (int)Math.Ceiling(fitnessMap.Height / sampleSize) + 1;
                int xSamples = (int)Math.Ceiling(fitnessMap.Width / sampleSize) + 1;

                foreach (float y in Enumerable.Range(0, ySamples))
                {
                    foreach (float x in Enumerable.Range(0, xSamples))
                    {
                        int value = (int)FitnessFunction.Invoke(new Vector2(x * sampleSize, y * sampleSize));
                        using (Brush color = new SolidBrush(Color.FromArgb(value, value, value)))
                            graphics.FillRectangle(color, x * sampleSize, y * sampleSize, sampleSize, sampleSize);
                    }
                }

                return result;
            }
        }

        public void RedrawFitnessMap()
        {
            if (FitnessBitmap != null)
                FitnessBitmap.Dispose();

            FitnessBitmap = GenerateFitnessBitmap();

            if (ParticleManager == null)
                return;

            if (fitnessMap.Image != null)
                fitnessMap.Image.Dispose();

            fitnessMap.Image = new Bitmap(fitnessMap.Width, fitnessMap.Height);

            using (Graphics graphics = Graphics.FromImage(fitnessMap.Image))
            {
                graphics.DrawImage(FitnessBitmap, 0, 0);

                using (SolidBrush brush = new SolidBrush(Color.Red))
                    foreach (Vector2Particle particle in ParticleManager.Particles)
                        graphics.FillPie(brush, particle.Model.X, particle.Model.Y, 5, 5, 0, 360);
            }
        }

        private void Simple2DVisualisation_ResizeEnd(object sender, EventArgs e)
        {
            RedrawFitnessMap();
        }

        private void Simple2DVisualisation_Shown(object sender, EventArgs e)
        {
            GenerateParticles(20);
            RedrawFitnessMap();
        }

        private void bMove_Click(object sender, EventArgs e)
        {
            ParticleManager.MoveParticles();
            RedrawFitnessMap();
        }

        private void bRegenerateParticles_Click(object sender, EventArgs e)
        {
            GenerateParticles(20);
            RedrawFitnessMap();
        }
    }
}
