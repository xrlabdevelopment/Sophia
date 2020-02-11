using System.Xml;

namespace Sophia.Serialization
{
    public abstract class Deserializer<T>
        where T : IXMLObject
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly string xml_data;

        //-------------------------------------------------------------------------------------
        public Deserializer(string xmlData)
        {
            xml_data = xmlData;
        }

        //-------------------------------------------------------------------------------------
        public T deserialize()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml_data);

            T obj = create();
            obj.deserialize(document.DocumentElement);

            return obj;
        }

        //-------------------------------------------------------------------------------------
        protected abstract T create();
    }
}
