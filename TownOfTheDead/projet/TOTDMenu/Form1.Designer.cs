namespace TOTDMenu
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_Jouer = new System.Windows.Forms.Button();
            this.btn_Options = new System.Windows.Forms.Button();
            this.btn_Quitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Jouer
            // 
            this.btn_Jouer.BackColor = System.Drawing.Color.Transparent;
            this.btn_Jouer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Jouer.BackgroundImage")));
            this.btn_Jouer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Jouer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Jouer.FlatAppearance.BorderSize = 0;
            this.btn_Jouer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Jouer.Location = new System.Drawing.Point(469, 627);
            this.btn_Jouer.Name = "btn_Jouer";
            this.btn_Jouer.Size = new System.Drawing.Size(281, 99);
            this.btn_Jouer.TabIndex = 0;
            this.btn_Jouer.UseVisualStyleBackColor = false;
            this.btn_Jouer.Click += new System.EventHandler(this.btn_Jouer_Click);
            // 
            // btn_Options
            // 
            this.btn_Options.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Options.BackgroundImage")));
            this.btn_Options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Options.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Options.Location = new System.Drawing.Point(851, 627);
            this.btn_Options.Name = "btn_Options";
            this.btn_Options.Size = new System.Drawing.Size(279, 99);
            this.btn_Options.TabIndex = 1;
            this.btn_Options.UseVisualStyleBackColor = true;
            this.btn_Options.Click += new System.EventHandler(this.btn_Options_Click);
            // 
            // btn_Quitter
            // 
            this.btn_Quitter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Quitter.BackgroundImage")));
            this.btn_Quitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Quitter.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Quitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Quitter.Location = new System.Drawing.Point(73, 627);
            this.btn_Quitter.Name = "btn_Quitter";
            this.btn_Quitter.Size = new System.Drawing.Size(282, 99);
            this.btn_Quitter.TabIndex = 2;
            this.btn_Quitter.UseVisualStyleBackColor = true;
            this.btn_Quitter.Click += new System.EventHandler(this.btn_Quitter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1166, 742);
            this.Controls.Add(this.btn_Quitter);
            this.Controls.Add(this.btn_Options);
            this.Controls.Add(this.btn_Jouer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Jouer;
        private System.Windows.Forms.Button btn_Options;
        private System.Windows.Forms.Button btn_Quitter;
    }
}

