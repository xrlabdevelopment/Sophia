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
        public void addSetting(Setting setting)
        {
            Setting s = settings.Find(s => s.Key == setting.Key);
            if (s != null)
                return;

            setting.onChanged += onSettingChanged;
            settings.Add(setting);
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
