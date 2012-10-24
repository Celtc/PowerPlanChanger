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
            this.button_maxPlan = new System.Windows.Forms.Button();
            this.button_ecoPlan = new System.Windows.Forms.Button();
            this.contextMenuStrip_APP.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_APP
            // 
            this.contextMenuStrip_APP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip_APP.Name = "contextMenuStrip_APP";
            this.contextMenuStrip_APP.Size = new System.Drawing.Size(153, 70);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // button_maxPlan
            // 
            this.button_maxPlan.Location = new System.Drawing.Point(2, 1);
            this.button_maxPlan.Name = "button_maxPlan";
            this.button_maxPlan.Size = new System.Drawing.Size(111, 23);
            this.button_maxPlan.TabIndex = 1;
            this.button_maxPlan.Text = "High Performance";
            this.button_maxPlan.UseVisualStyleBackColor = true;
            this.button_maxPlan.Click += new System.EventHandler(this.button_maxPlan_Click);
            // 
            // button_ecoPlan
            // 
            this.button_ecoPlan.Location = new System.Drawing.Point(119, 1);
            this.button_ecoPlan.Name = "button_ecoPlan";
            this.button_ecoPlan.Size = new System.Drawing.Size(115, 23);
            this.button_ecoPlan.TabIndex = 2;
            this.button_ecoPlan.Text = "Energy Saver";
            this.button_ecoPlan.UseVisualStyleBackColor = true;
            this.button_ecoPlan.Click += new System.EventHandler(this.button_ecoPlan_Click);
            // 
            // APP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(236, 25);
            this.ContextMenuStrip = this.contextMenuStrip_APP;
            this.Controls.Add(this.button_maxPlan);
            this.Controls.Add(this.button_ecoPlan);
            this.Name = "APP";
            this.Text = "APP";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.contextMenuStrip_APP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_APP;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button button_maxPlan;
        private System.Windows.Forms.Button button_ecoPlan;
    }
}