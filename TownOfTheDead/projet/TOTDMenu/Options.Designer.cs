namespace TOTDMenu
{
    partial class Options
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
            this.chk_Musique = new System.Windows.Forms.CheckBox();
            this.chk_Effets = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chk_Musique
            // 
            this.chk_Musique.AutoSize = true;
            this.chk_Musique.Location = new System.Drawing.Point(13, 13);
            this.chk_Musique.Name = "chk_Musique";
            this.chk_Musique.Size = new System.Drawing.Size(66, 17);
            this.chk_Musique.TabIndex = 0;
            this.chk_Musique.Text = "Musique";
            this.chk_Musique.UseVisualStyleBackColor = true;
            this.chk_Musique.CheckedChanged += new System.EventHandler(this.chk_Musique_CheckedChanged);
            // 
            // chk_Effets
            // 
            this.chk_Effets.AutoSize = true;
            this.chk_Effets.Location = new System.Drawing.Point(13, 37);
            this.chk_Effets.Name = "chk_Effets";
            this.chk_Effets.Size = new System.Drawing.Size(53, 17);
            this.chk_Effets.TabIndex = 1;
            this.chk_Effets.Text = "Effets";
            this.chk_Effets.UseVisualStyleBackColor = true;
            this.chk_Effets.CheckedChanged += new System.EventHandler(this.chk_Effets_CheckedChanged);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(116, 59);
            this.Controls.Add(this.chk_Effets);
            this.Controls.Add(this.chk_Musique);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_Musique;
        private System.Windows.Forms.CheckBox chk_Effets;
    }
}