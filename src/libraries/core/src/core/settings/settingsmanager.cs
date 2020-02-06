using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Sophia.Core
{
    public class SettingsManager : IObserver
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<string, Settings> cached_settings;

        private readonly string data_path;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the Settings manager
        /// </summary>
        /// <param name="dataPath">Datapath where we should read the settings from</param>
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
        /// <summary>
        /// Loading a settings file
        /// </summary>
        /// <typeparam name="T">Type of settings we would like to load</typeparam>
        /// <returns>The requested settings</returns>
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
            string path = Path.Combine(data_path, setting_name + Extentions.getExtention(Extention.XML));
            if (!File.Exists(path))
                return settings;

            //
            // Deserialize the settings file
            //
            SettingsDeserializer deserializer = new SettingsDeserializer(File.ReadAllText(path));
            SerializableSettings serializable_settings = deserializer.deserialize();

            foreach(IXMLObject obj in serializable_settings.Settings)
            {
                SerializableSetting setting = obj as SerializableSetting;
                if (setting == null)
                    continue;

                settings.addSetting(new Setting(setting.Key, setting.Value));
            }

            //
            // Attach observer so we get notified when a setting is changed
            //
            settings.attach(this);

            //
            // Cache the setting
            //
            cached_settings.Add(setting_name, settings);

            return settings;
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Save a specific settings file
        /// </summary>
        /// <typeparam name="T">Type of the settings we would like to save</typeparam>
        /// <returns>True if the settings are saved, false if not</returns>
        public bool save<T>()
            where T : Settings
        {
            //
            // Check if we loaded this setting file
            //
            string setting_name = typeof(T).Name;
            if (!cached_settings.ContainsKey(setting_name))
                return false;

            serialize(setting_name);
            return true;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Save all the settings to disk
        /// </summary>
        public void saveAll()
        {
            foreach (KeyValuePair<string, Settings> pair in cached_settings)
                serialize(pair.Key);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Reset the settings to their defaults
        /// </summary>
        public void resetSettings()
        {
            cached_settings.Clear();

            string[] savefiles = Directory.GetFiles(data_path, "*" + Extentions.getExtention(Extention.XML));
            foreach (string setting in savefiles)
                File.Delete(setting);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// When a settings has changed adjust the cache
        /// </summary>
        /// <param name="subject">Setting that has been changed</param>
        /// <returns>Returns true if the cache was adjusted, otherwise false</returns>
        public bool notify(Subject subject)
        {
            Settings settings = subject as Settings;
            if (settings == null)
                return false;

            cached_settings[settings.GetType().Name] = settings;
            return true;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function to save a setting to disk.
        /// </summary>
        private void serialize(string settingName)
        {
            Debug.Assert(cached_settings.ContainsKey(settingName), "Setting should be available here!");

            Serializer serializer = new Serializer(cached_settings[settingName].XMLObject);
            using (StreamWriter file_writer = new StreamWriter(Path.Combine(data_path, settingName + Extentions.getExtention(Extention.XML))))
            {
                serializer.serialize(file_writer);
            }
        }
    }
}
