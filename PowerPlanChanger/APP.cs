using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PowerPlanChanger.Sources;

namespace PowerPlanChanger
{
    public partial class APP : Form
    {
        internal int _powerChangePoint;
        internal bool _changePointOn;
        internal Guid _ecoPlan;
        internal Guid _maxPlan;

        //Builder
        public APP()
        {
            //Inicializacion
            InitializeComponent();

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
    }
}
