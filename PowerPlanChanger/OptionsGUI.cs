using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PowerPlanChanger.Sources;
using System.IO;

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
            this.caller = caller;

            //Seteo de valores de GUI
            SetGUIFields();

            //Muestra
            this.Show();
        }

        //Gui Fields
        private void RefreshMaxCombo()
        {        
            this.comboBox_maxPlan.DataSource = new BindingSource(this._powerPlans, null);
            this.comboBox_maxPlan.DisplayMember = "FriendlyName";
            this.comboBox_maxPlan.SelectedIndex = this._powerPlans.FindIndex(delegate(PowerPlanInfo ppi) { return ppi._friendlyName == PowerSchemeHelper.GetSchemeName(caller._maxPlan); });
        }

        private void RefreshEcoCombo()
        {
            this.comboBox_ecoPlan.DataSource = new BindingSource(this._powerPlans, null);
            this.comboBox_ecoPlan.DisplayMember = "FriendlyName";
            this.comboBox_ecoPlan.SelectedIndex = this._powerPlans.FindIndex(delegate(PowerPlanInfo ppi) { return ppi._friendlyName == PowerSchemeHelper.GetSchemeName(caller._ecoPlan); });
        }

        private void SetGUIFields()
        {
            this._powerPlans = PowerSchemeHelper.GetAllPowerSchemas();
            RefreshEcoCombo();
            RefreshMaxCombo();

            this.comboBox_position.SelectedIndex = int.Parse(caller.Position);
            this.comboBox_size.SelectedIndex = int.Parse(caller.ButtonSize);

            this.checkBox_changePointOn.Checked = caller._changePointOn;
            this.checkBox_plugCheck.Checked = caller._plugCheck;
            this.textBox_changePoint.Text = caller.PowerChangePoint;
        }

        //Internal functions
        #region Constantes
        internal const UInt32 SAVER_PLAN = 0x0001;
        internal const UInt32 PERFORMANCE_PLAN = 0x0002;
        #endregion
        private Guid createPlan(UInt32 plan)
        {
            //Variables locales
            string newPlanName = string.Empty;
            Guid newPlanGuid = Guid.Empty;
            byte[] newPlanFile = { 0 };

            if (plan == SAVER_PLAN)
            {
                newPlanName = "Saver Plan";
                newPlanFile = global::PowerPlanChanger.Properties.Resources.saverPlan;
            }
            else if (plan == PERFORMANCE_PLAN)
            {
                newPlanName = "Performance Plan";
                newPlanFile = global::PowerPlanChanger.Properties.Resources.performancePlan;
            }
            else
                return newPlanGuid;

            //Verifica que no exista ya el GUID
            int origPwrPlan = this._powerPlans.FindIndex(delegate(PowerPlanInfo ppi) { return ppi._friendlyName == newPlanName; });
            if (origPwrPlan != -1)
            {
                var ret = MessageBox.Show("Scheme already exists. Want to overwrite the existing one?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (ret == System.Windows.Forms.DialogResult.OK)
                {
                    if (!PowerSchemeHelper.DeleteScheme(this._powerPlans[origPwrPlan].SchemeGuid))
                    {
                        MessageBox.Show("Error deleting the scheme. If active you need to change the scheme before deleting it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return newPlanGuid;
                    }
                }
                else
                    return newPlanGuid;
            }
            else
            {
                this._powerPlans.Add(new PowerPlanInfo(newPlanName, Guid.Empty));
                origPwrPlan = this._powerPlans.Count - 1;
            }

            //Crea el scheme
            try
            {
                string filename = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ltemp.bin";
                File.WriteAllBytes(filename, newPlanFile);
                newPlanGuid = PowerSchemeHelper.ImportSchemeFile(filename);
                File.Delete(filename);
            }
            catch
            {
                MessageBox.Show("Error creating the scheme!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (newPlanGuid != Guid.Empty)
                this._powerPlans[origPwrPlan].SchemeGuid = newPlanGuid;

            return newPlanGuid;
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

        private void button_createEcoPlan_Click(object sender, EventArgs e)
        {
            //Crea el scheme
            Guid newPlanGuid = this.createPlan(SAVER_PLAN);

            //Asigna el plan creado como el nuevo predefinido
            if (newPlanGuid != Guid.Empty)
            {
                caller._ecoPlan = newPlanGuid;
                RefreshEcoCombo();
            }
        }

        private void button_createMaxPlan_Click(object sender, EventArgs e)
        {
            //Crea el scheme
            Guid newPlanGuid = this.createPlan(PERFORMANCE_PLAN);

            //Asigna el plan creado como el nuevo predefinido
            if (newPlanGuid != Guid.Empty)
            {
                caller._maxPlan = newPlanGuid;
                RefreshMaxCombo();
            }
        }
    }
}
