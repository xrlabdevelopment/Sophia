using System.Xml;

namespace Sophia.Deprecated
{
    public class SerializableAction : IAction, IXMLObject
    {
        //-------------------------------------------------------------------------------------
        // Constants
        public static readonly string ACTION_TAG = "Action";
        public static readonly string ACTION_NAME_TAG = "Name";
        public static readonly string ACTION_TYPE_TAG = "Type";

        //-------------------------------------------------------------------------------------
        // Properties
        public int ActionType
        {
            get { return action_type; }
        }
        public string Name
        {
            get { return action_name; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private int action_type;
        private string action_name;

        //-------------------------------------------------------------------------------------
        public SerializableAction()
        { }
        //-------------------------------------------------------------------------------------
        public SerializableAction(string name, int type)
        {
            action_name = name;
            action_type = type;
        }

        //-------------------------------------------------------------------------------------
        public void serialize(XmlDocument document, XmlElement root)
        {
            XmlElement action_element = document.CreateElement(ACTION_TAG);
            root.AppendChild(action_element);
            XmlElement action_name_element = document.CreateElement(ACTION_NAME_TAG);
            action_name_element.InnerText = action_name;
            XmlElement action_type_element = document.CreateElement(ACTION_TYPE_TAG);
            action_type_element.InnerText = action_type.ToString();

            action_element.AppendChild(action_name_element);
            action_element.AppendChild(action_type_element);
        }
        //-------------------------------------------------------------------------------------
        public void deserialize(XmlElement root)
        {
            XmlElement name_element = root.GetElementsByTagName(SerializableAction.ACTION_NAME_TAG)[0] as XmlElement;
            XmlElement type_element = root.GetElementsByTagName(SerializableAction.ACTION_TYPE_TAG)[0] as XmlElement;

            action_name = name_element.InnerText.ToString();
            int.TryParse(type_element.InnerText.ToString(), out action_type);
        }
    }
}

namespace Sophia.Deprecated
{ 
    public abstract class BaseAction : IAction, ISerializable
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public abstract int ActionType { get; }
        public abstract string Name { get; }

        public abstract IXMLObject XMLObject { get; }

        //-------------------------------------------------------------------------------------
        public abstract void execute();
        //-------------------------------------------------------------------------------------
        public abstract void unexecute();
    }
}
