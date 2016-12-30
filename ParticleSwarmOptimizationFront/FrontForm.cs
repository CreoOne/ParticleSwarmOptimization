using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParticleSwarmOptimization;
using System.Numerics;

namespace ParticleSwarmOptimizationFront
{
    public partial class FrontForm : Form
    {
        public FrontForm()
        {
            InitializeComponent();
        }

        private void bSimple2DVisualisation_Click(object sender, EventArgs e)
        {
            try
            {
                using (Simple2DVisualisation form = new Simple2DVisualisation())
                    form.ShowDialog();
            }

            catch(Exception exception)
            {
                ShowException(exception);
            }
        }

        private void ShowException(Exception exception)
        {
            MessageBox.Show(this, exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
