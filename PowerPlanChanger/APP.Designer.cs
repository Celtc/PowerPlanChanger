using System.Windows.Forms;
namespace PowerPlanChanger
{
    partial class APP
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

        protected class RightAlignPictureBox : PictureBox
        {
            protected override void OnPaint(PaintEventArgs pe)
            {
                if (this.Image != null)
                {
                    //calculate how much the image needs to be shifted in order to be in the center
                    int xOffset = (this.Width - this.Image.Width);

                    //shift all drawings to the amount calculated
                    pe.Graphics.TranslateTransform(xOffset, 0);
                }
                base.OnPaint(pe);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip_APP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_buttonPerformance = new PowerPlanChanger.APP.RightAlignPictureBox();
            this.pictureBox_buttonEnergy = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_APP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_buttonPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_buttonEnergy)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip_APP
            // 
            this.contextMenuStrip_APP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip_APP.Name = "contextMenuStrip_APP";
            this.contextMenuStrip_APP.Size = new System.Drawing.Size(117, 48);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pictureBox_buttonPerformance
            // 
            this.pictureBox_buttonPerformance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_buttonPerformance.Image = global::PowerPlanChanger.Properties.Resources.PerformanceButton_medium;
            this.pictureBox_buttonPerformance.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_buttonPerformance.Name = "pictureBox_buttonPerformance";
            this.pictureBox_buttonPerformance.Size = new System.Drawing.Size(158, 58);
            this.pictureBox_buttonPerformance.TabIndex = 3;
            this.pictureBox_buttonPerformance.TabStop = false;
            this.pictureBox_buttonPerformance.Click += new System.EventHandler(this.pictureBox_buttonPerformance_Click);
            // 
            // pictureBox_buttonEnergy
            // 
            this.pictureBox_buttonEnergy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_buttonEnergy.Image = global::PowerPlanChanger.Properties.Resources.EnergyButton_medium;
            this.pictureBox_buttonEnergy.Location = new System.Drawing.Point(158, 0);
            this.pictureBox_buttonEnergy.Name = "pictureBox_buttonEnergy";
            this.pictureBox_buttonEnergy.Size = new System.Drawing.Size(158, 58);
            this.pictureBox_buttonEnergy.TabIndex = 4;
            this.pictureBox_buttonEnergy.TabStop = false;
            this.pictureBox_buttonEnergy.Click += new System.EventHandler(this.pictureBox_buttonEnergy_Click);
            // 
            // APP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(316, 58);
            this.ContextMenuStrip = this.contextMenuStrip_APP;
            this.Controls.Add(this.pictureBox_buttonEnergy);
            this.Controls.Add(this.pictureBox_buttonPerformance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "APP";
            this.Text = "APP";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Deactivate += new System.EventHandler(this.APP_Deactivate);
            this.Load += new System.EventHandler(this.APP_Load);
            this.LocationChanged += new System.EventHandler(this.APP_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.APP_VisibleChanged);
            this.contextMenuStrip_APP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_buttonPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_buttonEnergy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_APP;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private RightAlignPictureBox pictureBox_buttonPerformance;
        private System.Windows.Forms.PictureBox pictureBox_buttonEnergy;
    }
}