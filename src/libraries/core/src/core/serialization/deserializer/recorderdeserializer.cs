using Sophia.Core;

namespace Sophia.Serialization
{
    public class RecorderDeserializer : Deserializer<SerializableRecorder>
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private IActionFactory action_factory;

        //-------------------------------------------------------------------------------------
        public RecorderDeserializer(IActionFactory factory, string xmlData)
            : base(xmlData)
        {
            action_factory = factory;
        }

        //-------------------------------------------------------------------------------------
        protected override SerializableRecorder create()
        {
            return new SerializableRecorder(action_factory);
        }
    }
}
