using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace PowerPlanChanger.Sources
{
    public static class RegistryManager
    {
        //Atributo
        private static bool _administrator = true;

        //Setea los valores actuales en el registro
        public static bool SaveConfig(string name, string value, string path)
        {
            //Si no es adm sale
            if (!_administrator)
                return false;

            //Variable local
            RegistryKey key = null;

            try
            {
                //Crea el subregistro
                key = Registry.LocalMachine.CreateSubKey(path);
                if (key == null)
                {
                    MessageBox.Show("No se pudo escribir en el registro. Verifique que tenga los permisos necesarios. El programa continuara ejecutando pero sus configuraciones se perderan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _administrator = false;
                    return false;
                }

                //Agrega los atributos
                key.SetValue(name, value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        //Setea los valores por defecto en el registro
        private static RegistryKey saveRegistryDefault()
        {
            //Variable local
            RegistryKey key = null;

            try
            {
                //Crea el subregistro
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\PowerPlanChanger");
                if (key == null)
                    return null;

                //Agrega los atributos
                key.SetValue("EcoPlan", "00000000-0000-0000-0000-000000000000");
                key.SetValue("MaxPlan", "00000000-0000-0000-0000-000000000000");
                key.SetValue("PlugCheck", "False");
                key.SetValue("ChangePointOn", "False");
                key.SetValue("PowerChangePoint", "30");
                key.SetValue("Position", "1");
                key.SetValue("ButtonSize", "2");
            }
            catch
            {
                return null;
            }

            return key;
        }

        //Lee los valores en el registro
        public static bool LoadConfig(APP caller, string path)
        {
            try
            {
                TRY_REGREAD:

                // Opening the registry key
                RegistryKey key = Registry.LocalMachine.OpenSubKey(path);
                if (key == null)
                {
                    if ((key = saveRegistryDefault()) == null)
                    {
                        MessageBox.Show("No se pudo escribir en el registro. Verifique que tenga los permisos necesarios. Se continuara la carga del programa pero sus configuraciones se perderan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _administrator = false;
                        return true;
                    }
                }

                //Lee
                try
                {
                    caller.PowerChangePoint = (key.GetValue("PowerChangePoint").ToString());
                    caller.PlugCheck = (key.GetValue("PlugCheck").ToString());
                    caller.ChangePointOn = (key.GetValue("ChangePointOn").ToString());
                    caller.ButtonSize = (key.GetValue("ButtonSize").ToString());
                    caller.Position = (key.GetValue("Position").ToString());
                    caller.EcoPlan = (key.GetValue("EcoPlan").ToString());
                    caller.MaxPlan = (key.GetValue("MaxPlan").ToString());
                }
                catch
                {
                    saveRegistryDefault();
                    goto TRY_REGREAD;
                }

            }
            catch
            {
                MessageBox.Show("Hubo un problema al leer los datos del registro!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
