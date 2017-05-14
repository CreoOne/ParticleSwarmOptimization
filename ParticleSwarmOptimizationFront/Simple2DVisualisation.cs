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
        private Vector2Machine Machine;
        private Func<Vector2, double> FitnessFunction;
        private Bitmap FitnessBitmap;

        public Simple2DVisualisation()
        {
            InitializeComponent();
            FitnessFunction = (Vector2 model) =>
            {
                double distanceFromCenter = (new Vector2(fitnessMap.Width / 2f, fitnessMap.Height / 2f) - model).Length();
                double sin = Math.Sin(model.X / 10d) / 2d + 0.5;
                double cos = Math.Cos(model.Y / 10d) / 2d + 0.5;
                
                double result = ((sin + cos) / 2d) * 100 + distanceFromCenter / 2d;

                return Math.Max(0, Math.Min(255, result));
            };
        }

        public void GenerateParticles(int amount)
        {
            Random rng = new Random();

            IEnumerable<Vector2> particles = Enumerable.Range(0, amount).Select(i => new Vector2(rng.Next(0, fitnessMap.Width), rng.Next(0, fitnessMap.Height)));
            Machine = new Vector2Machine(FitnessFunction, particles, 10, 2);
        }

        private void GenerateFitnessBitmap()
        {
            Bitmap result = new Bitmap(fitnessMap.Width, fitnessMap.Height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                float sampleSize = 4;

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

                if (FitnessBitmap != null)
                    FitnessBitmap.Dispose();

                FitnessBitmap = result;
            }
        }

        public void RedrawFitnessMap()
        {
            if (Machine == null)
                return;

            if (fitnessMap.Image != null)
                fitnessMap.Image.Dispose();

            fitnessMap.Image = new Bitmap(fitnessMap.Width, fitnessMap.Height);

            using (Graphics graphics = Graphics.FromImage(fitnessMap.Image))
            {
                graphics.DrawImage(FitnessBitmap, 0, 0);

                foreach (Vector2 particle in Machine.GetParticles())
                {
                    bool best = Machine.GetBestParticle() == particle;
                    using (SolidBrush brush = new SolidBrush(best ? Color.Aqua : Color.Red))
                        graphics.FillPie(brush, particle.X, particle.Y, 5, 5, 0, 360);
                }
            }
        }

        private void Simple2DVisualisation_ResizeEnd(object sender, EventArgs e)
        {
            GenerateFitnessBitmap();
            RedrawFitnessMap();
        }

        private void Simple2DVisualisation_Shown(object sender, EventArgs e)
        {
            GenerateParticles(20);
            GenerateFitnessBitmap();
            RedrawFitnessMap();
        }

        private void bMove_Click(object sender, EventArgs e)
        {
            Machine.Advance();
            RedrawFitnessMap();
        }

        private void bRegenerateParticles_Click(object sender, EventArgs e)
        {
            GenerateParticles(20);
            GenerateFitnessBitmap();
            RedrawFitnessMap();
        }
    }
}
