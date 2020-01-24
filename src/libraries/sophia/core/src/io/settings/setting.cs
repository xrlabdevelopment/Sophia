using Newtonsoft.Json;

namespace Sophia.Core
{
    public class Setting
    {
        //-------------------------------------------------------------------------------------
        // Delegates
        public delegate void Changed(string key, string value);

        public Changed onChanged;

        //-------------------------------------------------------------------------------------
        // Properties
        public string Key
        {
            get
            {
                return setting_key;
            }
        }
        public string Value
        {
            get
            {
                return setting_value;
            }
            set
            {
                if(value != setting_value)
                {
                    setting_value = value;
                    if (onChanged != null)
                        onChanged(setting_key, setting_value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private string setting_key;
        private string setting_value;

        //-------------------------------------------------------------------------------------
        public Setting(string k, string v)
        {
            setting_key = k;
            setting_value = v;
        }

        //-------------------------------------------------------------------------------------
        public int asInt()
        {
            int integer;
            return int.TryParse(setting_value, out integer)
                ? integer
                : 0;
        }
        //-------------------------------------------------------------------------------------
        public float asFloat()
        {
            float floating_point;
            return float.TryParse(setting_value, out floating_point)
                ? floating_point
                : 0.0f;
        }
        //-------------------------------------------------------------------------------------
        public bool asBool()
        {
            bool boolean;
            return bool.TryParse(setting_value, out boolean)
                ? boolean
                : false;
        }
    }
}
