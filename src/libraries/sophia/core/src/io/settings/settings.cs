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
