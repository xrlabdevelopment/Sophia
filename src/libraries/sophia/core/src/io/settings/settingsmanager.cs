using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Sophia.Core
{

    public class SettingsManager
    {

        //-------------------------------------------------------------------------------------
        // Fields
        private Dictionary<string, Settings> cachedsettings;
        private string datapath;

        //-------------------------------------------------------------------------------------
        public SettingsManager(string datapath)
        {
            cachedsettings = new Dictionary<string, Settings>();
            this.datapath = datapath;
        }
        //-------------------------------------------------------------------------------------
        public T load<T>()
        {
            if (cachedsettings.ContainsKey(typeof(T).Name))
            {
                if (cachedsettings[typeof(T).Name] is T)
                {
                    return (T)(object)cachedsettings[typeof(T).Name];
                }
            }

            string path = Path.Combine(datapath, typeof(T).Name + ".json");
            if (!File.Exists(path))
            {
                return default(T);
            }

            string json = File.ReadAllText(path);
            T result = JsonConvert.DeserializeObject<T>(json);
            Settings loadedSettings = (Settings)(object)result;
            loadedSettings.SettingsUpdated += updateCache;
            cachedsettings.Add(typeof(T).Name, loadedSettings);
            return result;
        }
        //-------------------------------------------------------------------------------------
        public bool save<T>(Settings settings)
        {
            string path = Path.Combine(datapath, typeof(T).Name + ".json");
            settings.save(path);
            return false;
        }
        //-------------------------------------------------------------------------------------
        public void resetSettings()
        {
            cachedsettings.Clear();
            string[] savefiles = Directory.GetFiles(datapath, "*.json");
            foreach (string setting in savefiles)
            {
                File.Delete(setting);
            }
        }
        //-------------------------------------------------------------------------------------
        public void updateCache(object changedSettings, System.EventArgs e)
        {
            cachedsettings[e.ToString()] = (Settings)changedSettings;
        }
    }
}
