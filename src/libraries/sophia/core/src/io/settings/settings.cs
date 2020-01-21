using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Sophia.Core
{
    public abstract class Settings
    {
        public System.EventHandler SettingsUpdated;
        //-------------------------------------------------------------------------------------
        public Settings()
        {
        }

        //-------------------------------------------------------------------------------------
        internal void save(string path)
        {
            string data = serialize();
            File.WriteAllText(path, data);
        }
        //-------------------------------------------------------------------------------------
        private string serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void update()
        {
            //SettingsUpdated?.Invoke(this, new SettingEventArgs(GetType().Name)); //C# language version 6
        }
    }

    public class SettingEventArgs : System.EventArgs
    {
        public string SettingsType { get; set; }
        public SettingEventArgs(string settings)
        {
            SettingsType = settings;
        }
    }
}
