//using Newtonsoft.Json;

namespace Sophia.Core
{
    public class Setting
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public string Key
        {
            get
            {
                return key;
            }
        }
        public string Value
        {
            get { return value; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private string key;
        private string value;

        //-------------------------------------------------------------------------------------
        public Setting(string k, string v)
        {
            key = k;
            value = v;
        }

        //-------------------------------------------------------------------------------------
        public string serialize()
        {
            //return JsonConvert.SerializeObject(this);
            return string.Empty;
        }

        //-------------------------------------------------------------------------------------
        public int asInt()
        {
            int integer;
            return int.TryParse(value, out integer)
                ? integer
                : 0;
        }
        //-------------------------------------------------------------------------------------
        public float asFloat()
        {
            float floating_point;
            return float.TryParse(value, out floating_point)
                ? floating_point
                : 0.0f;
        }
        //-------------------------------------------------------------------------------------
        public bool asBool()
        {
            bool boolean;
            return bool.TryParse(value, out boolean)
                ? boolean
                : false;
        }
    }
}
