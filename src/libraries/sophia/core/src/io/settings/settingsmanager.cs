using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Sophia.Core
{
    public class SettingsManager
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<string, Settings> cached_settings;
        private readonly string data_path;
        private readonly string extention = ".json";

        //-------------------------------------------------------------------------------------
        public SettingsManager(string data_path)
        {
            cached_settings = new Dictionary<string, Settings>();
            this.data_path = data_path;
        }

        //-------------------------------------------------------------------------------------
        public T load<T>() where T : Setting
        {
            string setting = typeof(T).Name;
            if (cached_settings.ContainsKey(setting))
            {
                if (cached_settings[setting] is T)
                {
                    return cached_settings[setting] as T;
                }
            }

            string path = Path.Combine(data_path, setting + extention);
            if (!File.Exists(path))
            {
                return default(T);
            }

            T result = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

            Settings loaded_settings = result as Settings;
            loaded_settings.SettingsUpdated += updateCache;

            cached_settings.Add(setting, loaded_settings);

            return result;
        }
        //-------------------------------------------------------------------------------------
        public bool save()
        {
            foreach (KeyValuePair<string, Settings> settings in cached_settings)
            {
                settings.Value.save(Path.Combine(data_path, settings.Key + extention));
            }

            return true;
        }

        //-------------------------------------------------------------------------------------
        public void resetSettings()
        {
            cached_settings.Clear();

            string[] savefiles = Directory.GetFiles(data_path, "*" + extention);
            foreach (string setting in savefiles)
            {
                File.Delete(setting);
            }
        }

        //-------------------------------------------------------------------------------------
        public void updateCache(object changedSettings, SettingEventArgs e)
        {
            cached_settings[e.SettingsType] = (Settings)changedSettings;
        }
    }
}
