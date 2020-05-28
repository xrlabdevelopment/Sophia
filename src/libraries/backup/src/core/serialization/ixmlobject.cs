using System.Xml;

namespace Sophia.Deprecated
{
    public interface IXMLObject
    {
        void serialize(XmlDocument document, XmlElement root);
        void deserialize(XmlElement root);
    }
}
