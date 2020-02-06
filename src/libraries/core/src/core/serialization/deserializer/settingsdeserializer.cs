using Sophia.Core;

namespace Sophia.Serialization
{
    public class SettingsDeserializer : Deserializer<SerializableSettings>
    {
        //-------------------------------------------------------------------------------------
        public SettingsDeserializer(string xmlData)
            :base(xmlData)
        {}

        //-------------------------------------------------------------------------------------
        protected override SerializableSettings create()
        {
            return new SerializableSettings();
        }
    }
}
