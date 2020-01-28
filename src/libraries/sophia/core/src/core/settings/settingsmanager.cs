using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Sophia.Core
{
    public class SettingsManager : IObserver
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly string EXTENTION = ".json";

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<string, Settings> cached_settings;

        private readonly string data_path;

        //-------------------------------------------------------------------------------------
        public SettingsManager(string dataPath)
        {
            cached_settings = new Dictionary<string, Settings>();

            // When the directory does not exist we create one.
            if (!Directory.Exists(dataPath))
            {
                Debug.WriteLine("Directory: " + dataPath + " does not exist");
                Debug.WriteLine("Creating: " + dataPath);

                Directory.CreateDirectory(dataPath);
            }

            data_path = dataPath;
        }

        //-------------------------------------------------------------------------------------
        public T load<T>()
            where T : Settings, new()
        {
            //
            // Check if we already loaded this setting file
            //
            string setting_name = typeof(T).Name;
            if (cached_settings.ContainsKey(setting_name))
            {
                if (cached_settings[setting_name] is T)
                {
                    return cached_settings[setting_name] as T;
                }
            }

            //
            // Create a new setting of type T
            //
            T settings = new T();

            //
            // Check if a file exists for there settings
            //
            string path = Path.Combine(data_path, setting_name + EXTENTION);
            if (!File.Exists(path))
                return settings;

            //
            // Deserialize the settings file
            //
            settings.deserialize(File.ReadAllText(path));
            settings.attach(this);

            cached_settings.Add(setting_name, settings);

            return settings;
        }
        //-------------------------------------------------------------------------------------
        public bool save()
        {
            foreach (KeyValuePair<string, Settings> pair in cached_settings)
                File.WriteAllText(Path.Combine(data_path, pair.Key + EXTENTION), pair.Value.serialize());

            return true;
        }

        //-------------------------------------------------------------------------------------
        public void resetSettings()
        {
            cached_settings.Clear();

            string[] savefiles = Directory.GetFiles(data_path, "*" + EXTENTION);
            foreach (string setting in savefiles)
                File.Delete(setting);
        }

        //-------------------------------------------------------------------------------------
        public bool notify(Subject subject)
        {
            Settings settings = subject as Settings;
            if (settings == null)
                return false;

            cached_settings[settings.GetType().Name] = settings;
            return true;
        }
    }
}
