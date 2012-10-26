using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PowerPlanChanger.Sources;

namespace PowerPlanChanger
{
    public partial class OptionsGUI : Form
    {
        private APP caller;
        private List<PowerPlanInfo> _powerPlans;

        //Builder
        public OptionsGUI(APP caller)
        {
            //Inicializacion
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this._powerPlans = PowerSchemeHelper.GetAllPowerSchemas();
            this.caller = caller;

            //Seteo de valores de GUI
            SetGUIFields();

            //Muestra
            this.Show();
        }

        //Gui Fields
        void SetGUIFields()
        {
            this.comboBox_ecoPlan.DataSource = new BindingSource(this._powerPlans, null);
            this.comboBox_ecoPlan.DisplayMember = "FriendlyName";
            this.comboBox_ecoPlan.SelectedIndex = this._powerPlans.FindIndex(delegate(PowerPlanInfo ppi) { return ppi.SchemeGuid == caller._ecoPlan; });

            this.comboBox_maxPlan.DataSource = new BindingSource(this._powerPlans, null);
            this.comboBox_maxPlan.DisplayMember = "FriendlyName";
            this.comboBox_maxPlan.SelectedIndex = this._powerPlans.FindIndex(delegate(PowerPlanInfo ppi) { return ppi.SchemeGuid == caller._maxPlan; });

            this.comboBox_position.SelectedIndex = int.Parse(caller.Position);
            this.comboBox_size.SelectedIndex = int.Parse(caller.ButtonSize);

            this.checkBox_changePointOn.Checked = caller._changePointOn;
            this.checkBox_plugCheck.Checked = caller._plugCheck;
            this.textBox_changePoint.Text = caller.PowerChangePoint;
        }

        //Bottons
        private void button_saveOpt_Click(object sender, EventArgs e)
        {
            //Verifica las opciones seleccionadas
            if (this.comboBox_ecoPlan.SelectedIndex < 0 || this.comboBox_maxPlan.SelectedIndex < 0)
            {
                var ans = MessageBox.Show("Not valid option selected!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (ans == System.Windows.Forms.DialogResult.Cancel)
                    this.Close();
                return;
            }

            //Vuelca los GUIFields al las variables internas
            caller._buttonSize = (APP.ButtonSizes)this.comboBox_size.SelectedIndex;
            caller._position = (APP.Positions)this.comboBox_position.SelectedIndex;
            caller._powerChangePoint = int.Parse(this.textBox_changePoint.Text);
            caller._changePointOn = this.checkBox_changePointOn.Checked;
            caller._plugCheck = this.checkBox_plugCheck.Checked;
            caller._ecoPlan = this._powerPlans[this.comboBox_ecoPlan.SelectedIndex].SchemeGuid;
            caller._maxPlan = this._powerPlans[this.comboBox_maxPlan.SelectedIndex].SchemeGuid;

            //Guarda en el regsitro
            RegistryManager.SaveConfig("ButtonSize", caller.ButtonSize, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("Position", caller.Position, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("EcoPlan", caller.EcoPlan, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("MaxPlan", caller.MaxPlan, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("PlugCheck", caller.PlugCheck, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("ChangePointOn", caller.ChangePointOn, "SOFTWARE\\PowerPlanChanger");
            RegistryManager.SaveConfig("PowerChangePoint", caller.PowerChangePoint, "SOFTWARE\\PowerPlanChanger");

            //Redibuja y Cierra
            caller.APP_Refresh();
            this.Close();
        }

        private void button_cancelOpt_Click(object sender, EventArgs e)
        {
            //Cierra
            this.Close();
        }
    }
}
