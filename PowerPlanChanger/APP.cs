using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PowerPlanChanger.Sources;

namespace PowerPlanChanger
{
    public partial class APP : Form
    {
        internal int _powerChangePoint;
        internal bool _changePointOn;
        internal Guid _ecoPlan;
        internal Guid _maxPlan;

        #region Form Dragging API Support
        //The SendMessage function sends a message to a window or windows.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        //ReleaseCapture releases a mouse capture
        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern bool ReleaseCapture();
        #endregion

        //Builder
        public APP()
        {
            //Inicializacion
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Lime;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;

            //Carga de valores
            RegistryManager.LoadConfig(this, "SOFTWARE\\PowerPlanChanger");
        }

        //Setters & Getter
        #region S&G
        public string PowerChangePoint
        {
            set { this._powerChangePoint = int.Parse(value); }
            get { return this._powerChangePoint.ToString(); }
        }

        public string ChangePointOn
        {
            set { this._changePointOn = value == "True"? true : false; }
            get { return this._changePointOn.ToString(); }
        }

        public string EcoPlan
        {
            set { this._ecoPlan = new Guid(value); }
            get { return this._ecoPlan.ToString(); }
        }

        public string MaxPlan
        {
            set { this._maxPlan = new Guid(value); }
            get { return this._maxPlan.ToString(); }
        }
        #endregion

        //Context Menu
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           OptionsGUI optGUI = new OptionsGUI(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Buttons
        private void button_maxPlan_Click(object sender, EventArgs e)
        {
            LogoForm logo = new LogoForm(500, 1200, global::PowerPlanChanger.Properties.Resources.EnergySaver);
            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_maxPlan);
        }

        private void button_ecoPlan_Click(object sender, EventArgs e)
        {
            LogoForm logo = new LogoForm(500, 1200, global::PowerPlanChanger.Properties.Resources.EnergySaver);
            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_ecoPlan);
        }
    }
}
