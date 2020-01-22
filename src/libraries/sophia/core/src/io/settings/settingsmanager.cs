using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Sophia.Core
{

    public class SettingsManager
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<string, Settings> cachedsettings;
        private readonly string datapath;
        private readonly string extention = ".json";
        //-------------------------------------------------------------------------------------
        public SettingsManager(string datapath)
        {
            cachedsettings = new Dictionary<string, Settings>();
            this.datapath = datapath;
        }
        //-------------------------------------------------------------------------------------
        public T load<T>() where T : Setting
        {
            string setting = typeof(T).Name;
            if (cachedsettings.ContainsKey(setting))
            {
                if (cachedsettings[setting] is T)
                {
                    return cachedsettings[setting] as T;
                }
            }

            string path = Path.Combine(datapath, setting + extention);
            if (!File.Exists(path))
            {
                return default(T);
            }

            string json = File.ReadAllText(path);
            T result = JsonConvert.DeserializeObject<T>(json);
            Settings loaded_settings = result as Settings;
            loaded_settings.SettingsUpdated += updateCache;
            cachedsettings.Add(setting, loaded_settings);
            return result;
        }
        //-------------------------------------------------------------------------------------
        public bool save()
        {
            foreach (KeyValuePair<string, Settings> settings in cachedsettings)
            {
                string path = Path.Combine(datapath, settings.Key + extention);
                settings.Value.save(path);
            }
            return false;
        }
        //-------------------------------------------------------------------------------------
        public void resetSettings()
        {
            cachedsettings.Clear();
            string[] savefiles = Directory.GetFiles(datapath, "*" + extention);
            foreach (string setting in savefiles)
            {
                File.Delete(setting);
            }
        }
        //-------------------------------------------------------------------------------------
        public void updateCache(object changedSettings, SettingEventArgs e)
        {
            cachedsettings[e.SettingsType] = (Settings)changedSettings;
        }
    }
}
