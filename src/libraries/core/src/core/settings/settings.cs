using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Xml;
using Sophia.Serialization;
using Sophia.Core;

namespace Sophia.Serialization
{
    public class SerializableSettings : ISettings, IXMLObject
    {
        //-------------------------------------------------------------------------------------
        // Constants
        public static readonly string SETTINGS_TAG = "Settings";
        public static readonly string SETTINGS_NAME_ATTRIB_TAG = "name";

        //-------------------------------------------------------------------------------------
        // Properties
        public string Name
        {
            get { return name; }
        }
        public List<IXMLObject> Settings
        {
            get { return Settings; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly string name;
        private readonly List<IXMLObject> settings;

        //-------------------------------------------------------------------------------------
        public SerializableSettings()
        {
            settings = new List<IXMLObject>();
        }
        //-------------------------------------------------------------------------------------
        public SerializableSettings(string name, List<IXMLObject> settings)
        {
            this.name = name;
            this.settings = settings;
        }

        //-------------------------------------------------------------------------------------
        public void serialize(XmlDocument document, XmlElement root)
        {
            //
            // <Settings name = "...">
            //
            XmlElement settings_element = document.CreateElement(SETTINGS_TAG);
            XmlAttribute settings_name_attibute = document.CreateAttribute(SETTINGS_NAME_ATTRIB_TAG);
            settings_name_attibute.Value = name;
            settings_element.Attributes.Append(settings_name_attibute);

            root.AppendChild(settings_element);

            //
            // Append all settings to this settings tag
            //
            foreach (IXMLObject setting in settings)
                setting.serialize(document, settings_element);
        }
        //-------------------------------------------------------------------------------------
        public void deserialize(XmlElement root)
        {
            foreach (XmlElement element in root.GetElementsByTagName(SerializableSetting.SETTING_TAG))
            {
                SerializableSetting setting = new SerializableSetting();
                setting.deserialize(element);

                settings.Add(setting);
            }
        }
    }
}

namespace Sophia.Core
{
    /// <summary>
    /// Abstract class for all settings
    /// </summary>
    public abstract class Settings : Subject, ISettings, ISerializable
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public string Name
        {
            get { return GetType().ToString(); }
        }
        public IXMLObject XMLObject
        {
            get
            {
                List<IXMLObject> xml_settings = new List<IXMLObject>();
                foreach (Setting setting in settings)
                    xml_settings.Add(new SerializableSetting(setting.Key, setting.Value));

                return new SerializableSettings(Name, xml_settings);
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly List<Setting> settings;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of a settings class
        /// </summary>
        protected Settings()
        {
            settings = new List<Setting>();
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Add a new setting.
        /// </summary>
        /// <param name="newSetting">The setting to be added</param>
        public void addSetting(Setting newSetting)
        {
            Setting setting = settings.Find(s => s.Key == newSetting.Key);
            if (setting != null)
                return;

            newSetting.onChanged += onSettingChanged;
            settings.Add(newSetting);
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve a setting
        /// </summary>
        /// <param name="name">Name of the setting to be retrieved</param>
        /// <returns>The required setting</returns>
        public Setting getSetting(string name)
        {
            Setting setting = settings.Find(s => s.Key == name);
            return setting != null
                ? setting
                : null;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// When settings have been changed this function will be called
        /// </summary>
        /// <param name="key">Key of the settings that has been changed</param>
        /// <param name="value">Value of the setting that has been changed</param>
        private void onSettingChanged(string key, string value)
        {
            Debug.Assert(string.Empty == key, "Setting key is empty");
            Debug.Assert(string.Empty == value, "Setting value is empty");

            notify();
        }
    }
}
