namespace ParticleSwarmOptimizationFront
{
    partial class Simple2DVisualisation
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
            this.fitnessMap = new System.Windows.Forms.PictureBox();
            this.bMove = new System.Windows.Forms.Button();
            this.bRegenerateParticles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fitnessMap)).BeginInit();
            this.SuspendLayout();
            // 
            // fitnessMap
            // 
            this.fitnessMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fitnessMap.Location = new System.Drawing.Point(0, 41);
            this.fitnessMap.Name = "fitnessMap";
            this.fitnessMap.Size = new System.Drawing.Size(543, 340);
            this.fitnessMap.TabIndex = 0;
            this.fitnessMap.TabStop = false;
            // 
            // bMove
            // 
            this.bMove.Location = new System.Drawing.Point(12, 12);
            this.bMove.Name = "bMove";
            this.bMove.Size = new System.Drawing.Size(75, 23);
            this.bMove.TabIndex = 1;
            this.bMove.Text = "Move";
            this.bMove.UseVisualStyleBackColor = true;
            this.bMove.Click += new System.EventHandler(this.bMove_Click);
            // 
            // bRegenerateParticles
            // 
            this.bRegenerateParticles.Location = new System.Drawing.Point(93, 12);
            this.bRegenerateParticles.Name = "bRegenerateParticles";
            this.bRegenerateParticles.Size = new System.Drawing.Size(135, 23);
            this.bRegenerateParticles.TabIndex = 2;
            this.bRegenerateParticles.Text = "Regenerate Particles";
            this.bRegenerateParticles.UseVisualStyleBackColor = true;
            this.bRegenerateParticles.Click += new System.EventHandler(this.bRegenerateParticles_Click);
            // 
            // Simple2DVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 381);
            this.Controls.Add(this.bRegenerateParticles);
            this.Controls.Add(this.bMove);
            this.Controls.Add(this.fitnessMap);
            this.Name = "Simple2DVisualisation";
            this.Text = "Simple2DVisualisation";
            this.Shown += new System.EventHandler(this.Simple2DVisualisation_Shown);
            this.ResizeEnd += new System.EventHandler(this.Simple2DVisualisation_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.fitnessMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox fitnessMap;
        private System.Windows.Forms.Button bMove;
        private System.Windows.Forms.Button bRegenerateParticles;
    }
}