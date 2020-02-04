using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace Sophia.Core
{
    public abstract class Settings : Subject
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private List<Setting> settings;

        //-------------------------------------------------------------------------------------
        protected Settings()
        {
            settings = new List<Setting>();
        }

        //-------------------------------------------------------------------------------------
        public void addSetting(Setting newSetting)
        {
            Setting setting = settings.Find(s => s.Key == newSetting.Key);
            if (setting != null)
                return;

            newSetting.onChanged += onSettingChanged;
            settings.Add(newSetting);
        }
        //-------------------------------------------------------------------------------------
        public Setting getSetting(string name)
        {
            Setting setting = settings.Find(s => s.Key == name);
            return setting != null
                ? setting
                : null;
        }

        //-------------------------------------------------------------------------------------
        public string serialize()
        {
            return JsonConvert.SerializeObject(settings);
        }
        //-------------------------------------------------------------------------------------
        public void deserialize(string serializedData)
        {
            settings = JsonConvert.DeserializeObject<List<Setting>>(serializedData);
        }

        //-------------------------------------------------------------------------------------
        private void onSettingChanged(string key, string value)
        {
            Debug.Assert(string.Empty == key, "Setting key is empty");
            Debug.Assert(string.Empty == value, "Setting value is empty");

            notify();
        }
    }
}
