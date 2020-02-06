using System.Xml;

namespace Sophia.Core
{
    public interface IXMLObject
    {
        void serialize(XmlDocument document, XmlElement root);
        void deserialize(XmlElement root);
    }
}
