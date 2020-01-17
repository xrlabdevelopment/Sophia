using System.Collections.Generic;

namespace Sophia.Core
{
    public class SettingsManager
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private List<Setting> settings;

        //-------------------------------------------------------------------------------------
        public SettingsManager()
        {
            settings = new List<Setting>();
        }

        //-------------------------------------------------------------------------------------
        public bool load(string path)
        {
            return false;
        }
        //-------------------------------------------------------------------------------------
        public bool save(string path)
        {
            return false;
        }
    }
}
