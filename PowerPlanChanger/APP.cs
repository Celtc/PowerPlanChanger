﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PowerPlanChanger.Sources;
using System.Timers;

namespace PowerPlanChanger
{
    public partial class APP : Form
    {
        System.Timers.Timer _timer;
        private PowerStatus _power;
        private BatteryChargeStatus actualChargeStatus;
        private List<ButtonsContainer> _buttonList;
        internal int _powerChangePoint;
        internal bool _changePointOn;
        internal bool _plugCheck;
        internal ButtonSizes _buttonSize;
        internal Positions _position;
        internal Guid _ecoPlan;
        internal Guid _maxPlan;

        internal struct ButtonsContainer
        {
            public Image EnergyButtonNonPressed;
            public Image EnergyButtonPressed;
            public Image PerformanceButtonNonPressed;
            public Image PerformanceButtonPressed;
        }

        public enum Positions
        {
            TopLeft = 0,
            TopCenter = 1,
            TopRight = 2,
            BottomLeft = 3,
            BottomCenter = 4,
            BottomRight = 5
        }

        public enum ButtonSizes
        {
            Large = 0,
            Medium = 1,
            Small = 2,
            XSmall = 3,
        }

        #region Form API Support
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        const int GWL_HWNDPARENT = -8;
        #endregion

        //Builder
        public APP()
        {
            //Inicializacion
            InitializeComponent();
            LoadButtons();
            _power = SystemInformation.PowerStatus;
            _timer = new System.Timers.Timer();
            _timer.Enabled = false;

            //Posicion inicial
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;

            //Carga de valores
            RegistryManager.LoadConfig(this, "SOFTWARE\\PowerPlanChanger");
            APP_Refresh();
        }

        #region Position Functions
        private void APP_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void APP_LocationChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void APP_VisibleChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
        }

        private void APP_Deactivate(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void SetPosition()
        {
            switch (this._position)
            {
                case Positions.BottomCenter: this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - 158,
                                                       Screen.PrimaryScreen.WorkingArea.Height - this.pictureBox_buttonEnergy.Image.Height);
                    break;

                case Positions.BottomLeft: this.Location = new Point(0 - (158 - this.pictureBox_buttonPerformance.Image.Width),
                                                     Screen.PrimaryScreen.WorkingArea.Height - this.pictureBox_buttonEnergy.Image.Height);
                    break;

                case Positions.BottomRight: this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 158 - this.pictureBox_buttonEnergy.Image.Width,
                                                                      Screen.PrimaryScreen.WorkingArea.Height - this.pictureBox_buttonEnergy.Image.Height);
                    break;

                case Positions.TopCenter: this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - 158, 0);
                    break;

                case Positions.TopLeft: this.Location = new Point(0 - (158 - this.pictureBox_buttonPerformance.Image.Width), 0);
                    break;

                case Positions.TopRight: this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 158 - this.pictureBox_buttonEnergy.Image.Width, 0);
                    break;
            }
        }
        #endregion

        #region Setters & Getters
        public string ButtonSize
        {
            set { this._buttonSize = (ButtonSizes) int.Parse(value); }
            get { return ((int) this._buttonSize).ToString(); }
        }

        public string Position
        {
            set { this._position = (Positions) int.Parse(value); }
            get { return ((int) this._position).ToString(); }
        }

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

        public string PlugCheck
        {
            set { this._plugCheck = value == "True" ? true : false; }
            get { return this._plugCheck.ToString(); }
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
        private void LoadButtons()
        {
            this._buttonList = new List<ButtonsContainer>(4);
            for (int i = 0; i < 4; i++)
            {
                ButtonsContainer bContainer = new ButtonsContainer();
                switch (i)
                {
                    case (int)ButtonSizes.Large: 
                        bContainer.EnergyButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButton_large;
                        bContainer.PerformanceButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButton_large;
                        bContainer.EnergyButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButtonPressed_large;
                        bContainer.PerformanceButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButtonPressed_large;
                        break;

                    case (int)ButtonSizes.Medium:
                        bContainer.EnergyButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButton_medium;
                        bContainer.PerformanceButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButton_medium;
                        bContainer.EnergyButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButtonPressed_medium;
                        bContainer.PerformanceButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButtonPressed_medium;
                        break;

                    case (int)ButtonSizes.Small:
                        bContainer.EnergyButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButton_small;
                        bContainer.PerformanceButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButton_small;
                        bContainer.EnergyButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButtonPressed_small;
                        bContainer.PerformanceButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButtonPressed_small;
                        break;

                    case (int)ButtonSizes.XSmall:
                        bContainer.EnergyButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButton_xsmall;
                        bContainer.PerformanceButtonNonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButton_xsmall;
                        bContainer.EnergyButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.EnergyButtonPressed_xsmall;
                        bContainer.PerformanceButtonPressed = (Image)global::PowerPlanChanger.Properties.Resources.PerformanceButtonPressed_xsmall;
                        break;
                }
                this._buttonList.Add(bContainer);
            }
        }

        private void RefreshButtonsBitmap()
        {
            this.pictureBox_buttonEnergy.Image = this._buttonList[(int)this._buttonSize].EnergyButtonNonPressed;
            this.pictureBox_buttonPerformance.Image = this._buttonList[(int)this._buttonSize].PerformanceButtonNonPressed;
        }

        private void pictureBox_buttonPerformance_Click(object sender, EventArgs e)
        {
            LogoForm logo = new LogoForm(500, 1200, global::PowerPlanChanger.Properties.Resources.PerformanceBattery);
            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_maxPlan);
        }

        private void pictureBox_buttonPerformance_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_buttonPerformance.Image = this._buttonList[(int)this._buttonSize].PerformanceButtonPressed;
        }

        private void pictureBox_buttonPerformance_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox_buttonPerformance.Image = this._buttonList[(int)this._buttonSize].PerformanceButtonNonPressed;
        }

        private void pictureBox_buttonEnergy_Click(object sender, EventArgs e)
        {
            LogoForm logo = new LogoForm(500, 1200, global::PowerPlanChanger.Properties.Resources.EnergySaver);
            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_ecoPlan);
        }

        private void pictureBox_buttonEnergy_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_buttonEnergy.Image = this._buttonList[(int)this._buttonSize].EnergyButtonPressed;
        }

        private void pictureBox_buttonEnergy_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox_buttonEnergy.Image = this._buttonList[(int)this._buttonSize].EnergyButtonNonPressed;
        }

        //Form Loading
        public void APP_Refresh()
        {
            this.RefreshButtonsBitmap();
            this.SetPosition();

            if (this._plugCheck || this._changePointOn)
            {
                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                _timer.Interval = 2000;
                _timer.Enabled = true;
            }
        }

        private void APP_Load(object sender, EventArgs e)
        {
            IntPtr hprog = FindWindowEx( FindWindowEx(FindWindow("Progman", "Program Manager"), IntPtr.Zero, "SHELLDLL_DefView", ""), IntPtr.Zero, "SysListView32", "FolderView");
            SetWindowLong(this.Handle, GWL_HWNDPARENT, hprog);
        }

        //Timer events
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            BatteryChargeCheck();
            BatterPlugCheck();
        }
        
        private void BatteryChargeCheck()
        {
            if( (int) (_power.BatteryLifePercent * 100) <= this._powerChangePoint)
                if(PowerSchemeHelper.GetPowerActiveScheme() != this._ecoPlan)
                    PowerSchemeHelper.SetPowerScheme(this._ecoPlan);
        }

        private void BatterPlugCheck()
        {
            if(_power.BatteryChargeStatus != this.actualChargeStatus)
            {
                if (_power.BatteryChargeStatus == BatteryChargeStatus.Charging)
                    PowerSchemeHelper.SetPowerScheme(this._ecoPlan);
                else 
                    PowerSchemeHelper.SetPowerScheme(this._maxPlan);

                this.actualChargeStatus = _power.BatteryChargeStatus;
            }
        }
    }
}
