using System;
using System.IO;
using System.Xml;

namespace Sophia.Serialization
{
    public class Serializer
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private IXMLObject xml_object;

        //-------------------------------------------------------------------------------------
        public Serializer(IXMLObject xmlObject)
        {
            xml_object = xmlObject;
        }

        //-------------------------------------------------------------------------------------
        public void serialize(TextWriter writer)
        {
            //
            // Create an XML Document
            // Assign and append the root node
            //
            XmlDocument xml_document = new XmlDocument();
            XmlElement root_node = xml_document.CreateElement("root");

            xml_document.AppendChild(root_node);

            //
            // Serialize the given XML object
            //
            xml_object.serialize(xml_document, root_node);

            //
            // Write the entire XML document to a string
            //
            using (XmlWriter xml_writer = XmlWriter.Create(writer))
            {
                xml_document.WriteTo(xml_writer);
                xml_writer.Flush();
            }
        }
    }
}
