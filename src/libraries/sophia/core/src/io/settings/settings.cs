using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Sophia.Core
{
    public abstract class Settings
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public System.EventHandler<SettingEventArgs> SettingsUpdated;

        //-------------------------------------------------------------------------------------
        internal void save(string path)
        {
            File.WriteAllText(path, serialize());
        }

        //-------------------------------------------------------------------------------------
        private string serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class SettingEventArgs : System.EventArgs
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public string SettingsType
        {
            get;
            set;
        }

        //-------------------------------------------------------------------------------------
        public SettingEventArgs(string settings)
        {
            SettingsType = settings;
        }
    }
}
