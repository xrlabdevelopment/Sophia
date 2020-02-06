using System.Xml;

namespace Sophia.Serialization
{
    public interface IXMLObject
    {
        void serialize(XmlDocument document, XmlElement root);
        void deserialize(XmlElement root);
    }
}
