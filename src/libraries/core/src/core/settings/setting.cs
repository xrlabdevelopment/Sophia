using System.IO;
using System.Xml;
using Sophia.Core;
using Sophia.Serialization;

namespace Sophia.Serialization
{
    internal class SerializableSetting : ISetting, IXMLObject
    {
        //-------------------------------------------------------------------------------------
        // Constants
        public static readonly string SETTING_TAG = "Setting";
        public static readonly string SETTING_KEY_TAG = "Key";
        public static readonly string SETTING_VALUE_TAG = "Value";

        //-------------------------------------------------------------------------------------
        // Properties
        public string Key
        {
            get { return setting_key; }
        }
        public string Value
        {
            get { return setting_value; }
            set { setting_value = value; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private string setting_key;
        private string setting_value;

        //-------------------------------------------------------------------------------------
        public SerializableSetting()
        {
            setting_key = string.Empty;
            setting_value = string.Empty;
        }
        //-------------------------------------------------------------------------------------
        public SerializableSetting(string key, string value)
        {
            setting_key = key;
            setting_value = value;
        }

        //-------------------------------------------------------------------------------------
        public void serialize(XmlDocument document, XmlElement root)
        {
            //
            // <Settings>
            //
            XmlElement setting_element = document.CreateElement(SETTING_TAG);
            root.AppendChild(setting_element);

            //
            // <Key>
            //
            XmlElement key_element = document.CreateElement(SETTING_KEY_TAG);
            key_element.InnerText = setting_key;
            //
            // <Value>
            //
            XmlElement value_element = document.CreateElement(SETTING_VALUE_TAG);
            value_element.InnerText = setting_value;

            setting_element.AppendChild(key_element);
            setting_element.AppendChild(value_element);
        }
        //-------------------------------------------------------------------------------------
        public void deserialize(XmlElement root)
        {
            XmlElement key_element = root.GetElementsByTagName(SerializableSetting.SETTING_KEY_TAG)[0] as XmlElement;
            XmlElement value_element = root.GetElementsByTagName(SerializableSetting.SETTING_VALUE_TAG)[0] as XmlElement;

            setting_key = key_element.InnerText.ToString();
            setting_value = value_element.InnerText.ToString();
        }
    }
}

namespace Sophia.Core
{
    public class Setting : ISetting, ISerializable
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

        public IXMLObject XMLObject
        {
            get
            {
                return new SerializableSetting(Key, Value);
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
