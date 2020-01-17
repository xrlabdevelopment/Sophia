using System.Collections.Generic;

namespace Sophia.Core
{
    public class Settings
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private List<Setting> settings;

        //-------------------------------------------------------------------------------------
        public Settings()
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
