using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PowerPlanChanger.Sources;
using System.Timers;
using System.Threading;

namespace PowerPlanChanger
{
    public partial class APP : Form
    {
        System.Timers.Timer _timer;
        private PowerStatus _power;
        private PowerLineStatus actualLineStatus;
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

        /// <summary>
        /// Win32 API imports.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// The action completed successfully.
            /// </summary>
            internal const int ERROR_SUCCESS = 0x0;

            /// <summary>
            /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to
            /// its parent window, or when an accelerator keystroke is translated. 
            /// </summary>
            internal const uint WM_COMMAND = 0x111;

            /// <summary>
            /// Delegate used to call <code>EnumWindowsProc</code>.
            /// </summary>
            /// <param name="handle">Handle received during window enumeration.</param>
            /// <param name="param">Parameter passed to <code>EnumWindowsProc</code>; not used in this application.</param>
            /// <returns>1 to continue windows enumeration; 0 otherwise.</returns>
            public delegate int EnumWindowsProcDelegate(IntPtr handle, int param);

            /// <summary>
            /// The behavior of the SendMessageTimeout. This parameter can be one or more of the following values.
            /// </summary>
            [Flags]
            internal enum SendMessageTimeoutFlags : uint
            {
                /// <summary>
                /// The calling thread is not prevented from processing other requests while waiting for the function to return.
                /// </summary>
                SMTO_NORMAL = 0x0,

                /// <summary>
                /// Prevents the calling thread from processing any other requests until the function returns.
                /// </summary>
                SMTO_BLOCK = 0x1,

                /// <summary>
                /// The function returns without waiting for the time-out period to elapse if the receiving thread appears to not respond or
                /// "hangs."
                /// </summary>
                SMTO_ABORTIFHUNG = 0x2,

                /// <summary>
                /// The function does not enforce the time-out period as long as the receiving thread is processing messages.
                /// </summary>
                SMTO_NOTIMEOUTIFNOTHUNG = 0x8
            }

            [DllImport("user32.dll")]
            internal static extern int EnumWindows(EnumWindowsProcDelegate lpEnumFunc, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProcDelegate lpEnumFunc, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr FindWindow(
                string lpWindowClass,
                string lpWindowName);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr FindWindowEx(
                IntPtr hwndParent,
                IntPtr hwndChildAfter,
                [MarshalAs(UnmanagedType.LPWStr)]string lpszClass,
                [MarshalAs(UnmanagedType.LPWStr)]string lpszWindow);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessageTimeout(
                IntPtr hWnd,
                uint Msg,
                IntPtr wParam,
                IntPtr lParam,
                SendMessageTimeoutFlags fuFlags,
                uint uTimeout,
                out UIntPtr lpdwResult);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern int SetWindowLong(
                IntPtr hWnd, int nIndex,
                IntPtr dwNewLong);
        }

        internal static IntPtr shellWindowHandle;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "All exceptions are caught to prevent enumeration stop")]
        private static int EnumWindowsProc(IntPtr handle, int param)
        {
            try
            {
                IntPtr foundHandle = NativeMethods.FindWindowEx(handle, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (!foundHandle.Equals(IntPtr.Zero))
                {
                    shellWindowHandle = foundHandle;
                    return 0;
                }
            }
            catch { }

            return 1;
        }

        private static IntPtr FindShellWindow()
        {
            IntPtr progmanHandle;
            IntPtr defaultViewHandle = IntPtr.Zero;
            IntPtr workerWHandle;
            int errorCode = NativeMethods.ERROR_SUCCESS;

            // Try "SHELLDLL_DefView" as a child window of "Progman".
            progmanHandle = NativeMethods.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            if (!progmanHandle.Equals(IntPtr.Zero))
            {
                defaultViewHandle = NativeMethods.FindWindowEx(progmanHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                errorCode = Marshal.GetLastWin32Error();
            }

            if (!defaultViewHandle.Equals(IntPtr.Zero))
            {
                return defaultViewHandle;
            }
            else if (errorCode != NativeMethods.ERROR_SUCCESS)
            {
                Marshal.ThrowExceptionForHR(errorCode);
            }

            // Try "SHELLDLL_DefView" as a child of "WorkerW".
            errorCode = NativeMethods.ERROR_SUCCESS;
            workerWHandle = NativeMethods.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "WorkerW", null);

            if (!workerWHandle.Equals(IntPtr.Zero))
            {
                defaultViewHandle = NativeMethods.FindWindowEx(workerWHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                errorCode = Marshal.GetLastWin32Error();
            }

            if (!defaultViewHandle.Equals(IntPtr.Zero))
            {
                return defaultViewHandle;
            }
            else if (errorCode != NativeMethods.ERROR_SUCCESS)
            {
                Marshal.ThrowExceptionForHR(errorCode);
            }

            // Try "SHELLDLL_DefView" as a child or a child of "Progman".
            shellWindowHandle = IntPtr.Zero;
            if (NativeMethods.EnumWindows(EnumWindowsProc, progmanHandle) == 0)
            {
                errorCode = Marshal.GetLastWin32Error();
                if (errorCode != NativeMethods.ERROR_SUCCESS)
                {
                    Marshal.ThrowExceptionForHR(errorCode);
                }
            }

            // Try "SHELLDLL_DefView" as if were in another desktop.
            if (shellWindowHandle.Equals(IntPtr.Zero))
            {
                if (NativeMethods.EnumDesktopWindows(IntPtr.Zero, EnumWindowsProc, progmanHandle))
                {
                    errorCode = Marshal.GetLastWin32Error();
                    if (errorCode != NativeMethods.ERROR_SUCCESS)
                    {
                        Marshal.ThrowExceptionForHR(errorCode);
                    }
                }
            }

            return shellWindowHandle;
        }

        const int GWL_HWNDPARENT = -8;
        #endregion

        //Builder
        public APP()
        {
            //Inicializacion
            InitializeComponent();

            //Posicion inicial
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
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
            try
            {
                LogoForm logo = new LogoForm(500, 1500, 20, global::PowerPlanChanger.Properties.Resources.PerformanceBattery);
                PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_maxPlan);
            }
            catch { }
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
            try
            {
                LogoForm logo = new LogoForm(500, 1500, 20, global::PowerPlanChanger.Properties.Resources.EnergySaver);
                PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_ecoPlan);
            }
            catch { }
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
                this.actualLineStatus = PowerLineStatus.Unknown;
                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                _timer.Interval = 2000;
                _timer.Enabled = true;
            }
        }

        private void APP_Load(object sender, EventArgs e)
        {
            //Traslada el padre
            IntPtr hprog = NativeMethods.FindWindowEx(FindShellWindow() , IntPtr.Zero, "SysListView32", "FolderView");
            NativeMethods.SetWindowLong(this.Handle, GWL_HWNDPARENT, hprog);

            //Carga los botones
            LoadButtons();

            //Carga el Timer
            _power = SystemInformation.PowerStatus;
            _timer = new System.Timers.Timer();
            _timer.Enabled = false;

            //Carga de valores de registro
            RegistryManager.LoadConfig(this, "SOFTWARE\\PowerPlanChanger");

            //Refresca el APP
            APP_Refresh();
        }

        //Timer events
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if(this._changePointOn)
                BatteryChargeCheck();

            if(this._plugCheck)
                BatterPlugCheck();
        }
        
        private void BatteryChargeCheck()
        {
            if ((int)(_power.BatteryLifePercent * 100) <= this._powerChangePoint)
            {
                if (PowerSchemeHelper.GetPowerActiveScheme() != this._ecoPlan)
                {
                    try
                    {
                        LogoForm logo = new LogoForm(500, 1500, 20, global::PowerPlanChanger.Properties.Resources.EnergySaver);
                        PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_ecoPlan);
                    }
                    catch { }
                }
            }
        }

        private void BatterPlugCheck()
        {
            if(this.actualLineStatus != _power.PowerLineStatus)
            {
                this.actualLineStatus = _power.PowerLineStatus;
                if (_power.PowerLineStatus == PowerLineStatus.Online)
                {
                    if (PowerSchemeHelper.GetPowerActiveScheme() != _maxPlan)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            LogoForm logo = new LogoForm(500, 1500, 20, global::PowerPlanChanger.Properties.Resources.PerformanceBattery);
                            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_maxPlan);
                        }
                        catch { }
                    }
                }
                else if (_power.PowerLineStatus == PowerLineStatus.Offline)
                {
                    if (PowerSchemeHelper.GetPowerActiveScheme() != _ecoPlan)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            LogoForm logo = new LogoForm(500, 1500, 20, global::PowerPlanChanger.Properties.Resources.EnergySaver);
                            PowerPlanChanger.Sources.PowerSchemeHelper.SetPowerScheme(_ecoPlan);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
