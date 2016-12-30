namespace ParticleSwarmOptimizationFront
{
    partial class FrontForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bSimple2DVisualisation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bSimple2DVisualisation
            // 
            this.bSimple2DVisualisation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSimple2DVisualisation.Location = new System.Drawing.Point(12, 12);
            this.bSimple2DVisualisation.Name = "bSimple2DVisualisation";
            this.bSimple2DVisualisation.Size = new System.Drawing.Size(194, 23);
            this.bSimple2DVisualisation.TabIndex = 0;
            this.bSimple2DVisualisation.Text = "Simple 2D Visualisation";
            this.bSimple2DVisualisation.UseVisualStyleBackColor = true;
            this.bSimple2DVisualisation.Click += new System.EventHandler(this.bSimple2DVisualisation_Click);
            // 
            // FrontForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 262);
            this.Controls.Add(this.bSimple2DVisualisation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrontForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bSimple2DVisualisation;
    }
}

