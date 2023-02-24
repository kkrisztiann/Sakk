
namespace Sakk
{
    partial class PromociKivalasztas
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kiralynoPbox = new System.Windows.Forms.PictureBox();
            this.bastyaPbox = new System.Windows.Forms.PictureBox();
            this.futoPbox = new System.Windows.Forms.PictureBox();
            this.loPbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.kiralynoPbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bastyaPbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.futoPbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loPbox)).BeginInit();
            this.SuspendLayout();
            // 
            // kiralynoPbox
            // 
            this.kiralynoPbox.Location = new System.Drawing.Point(0, 0);
            this.kiralynoPbox.Name = "kiralynoPbox";
            this.kiralynoPbox.Size = new System.Drawing.Size(90, 90);
            this.kiralynoPbox.TabIndex = 0;
            this.kiralynoPbox.TabStop = false;
            this.kiralynoPbox.Click += new System.EventHandler(this.kiralynoPbox_Click_1);
            // 
            // bastyaPbox
            // 
            this.bastyaPbox.Location = new System.Drawing.Point(0, 90);
            this.bastyaPbox.Name = "bastyaPbox";
            this.bastyaPbox.Size = new System.Drawing.Size(90, 90);
            this.bastyaPbox.TabIndex = 1;
            this.bastyaPbox.TabStop = false;
            this.bastyaPbox.Click += new System.EventHandler(this.bastyaPbox_Click);
            // 
            // futoPbox
            // 
            this.futoPbox.Location = new System.Drawing.Point(0, 180);
            this.futoPbox.Name = "futoPbox";
            this.futoPbox.Size = new System.Drawing.Size(90, 90);
            this.futoPbox.TabIndex = 2;
            this.futoPbox.TabStop = false;
            this.futoPbox.Click += new System.EventHandler(this.futoPbox_Click);
            // 
            // loPbox
            // 
            this.loPbox.Location = new System.Drawing.Point(0, 270);
            this.loPbox.Name = "loPbox";
            this.loPbox.Size = new System.Drawing.Size(90, 90);
            this.loPbox.TabIndex = 3;
            this.loPbox.TabStop = false;
            this.loPbox.Click += new System.EventHandler(this.loPbox_Click);
            // 
            // PromociKivalasztas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.Controls.Add(this.loPbox);
            this.Controls.Add(this.futoPbox);
            this.Controls.Add(this.bastyaPbox);
            this.Controls.Add(this.kiralynoPbox);
            this.Location = new System.Drawing.Point(0, 410);
            this.Name = "PromociKivalasztas";
            this.Size = new System.Drawing.Size(90, 360);
            ((System.ComponentModel.ISupportInitialize)(this.kiralynoPbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bastyaPbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.futoPbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loPbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox kiralynoPbox;
        private System.Windows.Forms.PictureBox bastyaPbox;
        private System.Windows.Forms.PictureBox futoPbox;
        private System.Windows.Forms.PictureBox loPbox;
    }
}
