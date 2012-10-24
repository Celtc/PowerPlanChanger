namespace PowerPlanChanger
{
    partial class OptionsGUI
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
            this.button_saveOpt = new System.Windows.Forms.Button();
            this.comboBox_ecoPlan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_maxPlan = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_changePointOn = new System.Windows.Forms.CheckBox();
            this.textBox_changePoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_cancelOpt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_saveOpt
            // 
            this.button_saveOpt.Location = new System.Drawing.Point(84, 107);
            this.button_saveOpt.Name = "button_saveOpt";
            this.button_saveOpt.Size = new System.Drawing.Size(75, 23);
            this.button_saveOpt.TabIndex = 0;
            this.button_saveOpt.Text = "Save";
            this.button_saveOpt.UseVisualStyleBackColor = true;
            this.button_saveOpt.Click += new System.EventHandler(this.button_saveOpt_Click);
            // 
            // comboBox_ecoPlan
            // 
            this.comboBox_ecoPlan.FormattingEnabled = true;
            this.comboBox_ecoPlan.Location = new System.Drawing.Point(134, 12);
            this.comboBox_ecoPlan.Name = "comboBox_ecoPlan";
            this.comboBox_ecoPlan.Size = new System.Drawing.Size(205, 21);
            this.comboBox_ecoPlan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Power Saver Plan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "High Performance Plan";
            // 
            // comboBox_maxPlan
            // 
            this.comboBox_maxPlan.FormattingEnabled = true;
            this.comboBox_maxPlan.Location = new System.Drawing.Point(134, 39);
            this.comboBox_maxPlan.Name = "comboBox_maxPlan";
            this.comboBox_maxPlan.Size = new System.Drawing.Size(205, 21);
            this.comboBox_maxPlan.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Change to power saver plan when battery is under ";
            // 
            // checkBox_changePointOn
            // 
            this.checkBox_changePointOn.AutoSize = true;
            this.checkBox_changePointOn.Location = new System.Drawing.Point(13, 74);
            this.checkBox_changePointOn.Name = "checkBox_changePointOn";
            this.checkBox_changePointOn.Size = new System.Drawing.Size(15, 14);
            this.checkBox_changePointOn.TabIndex = 6;
            this.checkBox_changePointOn.UseVisualStyleBackColor = true;
            // 
            // textBox_changePoint
            // 
            this.textBox_changePoint.Location = new System.Drawing.Point(281, 70);
            this.textBox_changePoint.Name = "textBox_changePoint";
            this.textBox_changePoint.Size = new System.Drawing.Size(41, 20);
            this.textBox_changePoint.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "%";
            // 
            // button_cancelOpt
            // 
            this.button_cancelOpt.Location = new System.Drawing.Point(180, 107);
            this.button_cancelOpt.Name = "button_cancelOpt";
            this.button_cancelOpt.Size = new System.Drawing.Size(75, 23);
            this.button_cancelOpt.TabIndex = 9;
            this.button_cancelOpt.Text = "Cancel";
            this.button_cancelOpt.UseVisualStyleBackColor = true;
            this.button_cancelOpt.Click += new System.EventHandler(this.button_cancelOpt_Click);
            // 
            // OptionsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 139);
            this.Controls.Add(this.button_cancelOpt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_changePoint);
            this.Controls.Add(this.checkBox_changePointOn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_maxPlan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_ecoPlan);
            this.Controls.Add(this.button_saveOpt);
            this.Name = "OptionsGUI";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_saveOpt;
        private System.Windows.Forms.ComboBox comboBox_ecoPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_maxPlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_changePointOn;
        private System.Windows.Forms.TextBox textBox_changePoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_cancelOpt;
    }
}

