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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_position = new System.Windows.Forms.ComboBox();
            this.comboBox_size = new System.Windows.Forms.ComboBox();
            this.checkBox_plugCheck = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_saveOpt
            // 
            this.button_saveOpt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_saveOpt.Location = new System.Drawing.Point(94, 145);
            this.button_saveOpt.Name = "button_saveOpt";
            this.button_saveOpt.Size = new System.Drawing.Size(75, 23);
            this.button_saveOpt.TabIndex = 7;
            this.button_saveOpt.Text = "Save";
            this.button_saveOpt.UseVisualStyleBackColor = true;
            this.button_saveOpt.Click += new System.EventHandler(this.button_saveOpt_Click);
            // 
            // comboBox_ecoPlan
            // 
            this.comboBox_ecoPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ecoPlan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_ecoPlan.FormattingEnabled = true;
            this.comboBox_ecoPlan.Location = new System.Drawing.Point(134, 37);
            this.comboBox_ecoPlan.Name = "comboBox_ecoPlan";
            this.comboBox_ecoPlan.Size = new System.Drawing.Size(205, 21);
            this.comboBox_ecoPlan.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Power Saver Plan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "High Performance Plan";
            // 
            // comboBox_maxPlan
            // 
            this.comboBox_maxPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_maxPlan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_maxPlan.FormattingEnabled = true;
            this.comboBox_maxPlan.Location = new System.Drawing.Point(134, 64);
            this.comboBox_maxPlan.Name = "comboBox_maxPlan";
            this.comboBox_maxPlan.Size = new System.Drawing.Size(205, 21);
            this.comboBox_maxPlan.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Change to power saver plan when battery is under ";
            // 
            // checkBox_changePointOn
            // 
            this.checkBox_changePointOn.AutoSize = true;
            this.checkBox_changePointOn.Location = new System.Drawing.Point(13, 95);
            this.checkBox_changePointOn.Name = "checkBox_changePointOn";
            this.checkBox_changePointOn.Size = new System.Drawing.Size(15, 14);
            this.checkBox_changePointOn.TabIndex = 4;
            this.checkBox_changePointOn.UseVisualStyleBackColor = true;
            // 
            // textBox_changePoint
            // 
            this.textBox_changePoint.Location = new System.Drawing.Point(279, 92);
            this.textBox_changePoint.Name = "textBox_changePoint";
            this.textBox_changePoint.Size = new System.Drawing.Size(41, 20);
            this.textBox_changePoint.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "%";
            // 
            // button_cancelOpt
            // 
            this.button_cancelOpt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_cancelOpt.Location = new System.Drawing.Point(190, 145);
            this.button_cancelOpt.Name = "button_cancelOpt";
            this.button_cancelOpt.Size = new System.Drawing.Size(75, 23);
            this.button_cancelOpt.TabIndex = 8;
            this.button_cancelOpt.Text = "Cancel";
            this.button_cancelOpt.UseVisualStyleBackColor = true;
            this.button_cancelOpt.Click += new System.EventHandler(this.button_cancelOpt_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Position";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Size";
            // 
            // comboBox_position
            // 
            this.comboBox_position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_position.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_position.FormattingEnabled = true;
            this.comboBox_position.Items.AddRange(new object[] {
            "Top Left",
            "Top Center",
            "Top Right",
            "Bottom Left",
            "Bottom Center",
            "Bottom Right"});
            this.comboBox_position.Location = new System.Drawing.Point(62, 10);
            this.comboBox_position.Name = "comboBox_position";
            this.comboBox_position.Size = new System.Drawing.Size(117, 21);
            this.comboBox_position.TabIndex = 0;
            // 
            // comboBox_size
            // 
            this.comboBox_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_size.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_size.FormattingEnabled = true;
            this.comboBox_size.Items.AddRange(new object[] {
            "Large",
            "Medium",
            "Small",
            "XSmall"});
            this.comboBox_size.Location = new System.Drawing.Point(218, 10);
            this.comboBox_size.Name = "comboBox_size";
            this.comboBox_size.Size = new System.Drawing.Size(119, 21);
            this.comboBox_size.TabIndex = 1;
            // 
            // checkBox_plugCheck
            // 
            this.checkBox_plugCheck.AutoSize = true;
            this.checkBox_plugCheck.Location = new System.Drawing.Point(13, 117);
            this.checkBox_plugCheck.Name = "checkBox_plugCheck";
            this.checkBox_plugCheck.Size = new System.Drawing.Size(15, 14);
            this.checkBox_plugCheck.TabIndex = 6;
            this.checkBox_plugCheck.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(287, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Automatically change between plans when plug-in plug-out.";
            // 
            // OptionsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 179);
            this.Controls.Add(this.checkBox_plugCheck);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox_size);
            this.Controls.Add(this.comboBox_position);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsGUI";
            this.Text = "Options";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_position;
        private System.Windows.Forms.ComboBox comboBox_size;
        private System.Windows.Forms.CheckBox checkBox_plugCheck;
        private System.Windows.Forms.Label label7;
    }
}

