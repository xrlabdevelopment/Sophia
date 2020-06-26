using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Sophia.Deprecated
{
    public class SerializableRecorder : IXMLObject
    {
        //-------------------------------------------------------------------------------------
        // Constants
        public static readonly string RECORDER_TAG = "Recorder";

        //-------------------------------------------------------------------------------------
        // Properties
        public List<IXMLObject> RecordedActions
        {
            get { return recorded_actions; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly IActionFactory action_factory;
        private readonly List<IXMLObject> recorded_actions;

        //-------------------------------------------------------------------------------------
        public SerializableRecorder(IActionFactory actionFactory)
        {
            action_factory = actionFactory;
            recorded_actions = new List<IXMLObject>();
        }
        //-------------------------------------------------------------------------------------
        public SerializableRecorder(IActionFactory actionFactory, List<IXMLObject> action)
        {
            action_factory = actionFactory;
            recorded_actions = action;
        }

        //-------------------------------------------------------------------------------------
        public void serialize(XmlDocument document, XmlElement root)
        {
            //
            // <Recorder>
            //
            XmlElement recorder_element = document.CreateElement(RECORDER_TAG);
            root.AppendChild(recorder_element);

            foreach (IXMLObject action in recorded_actions)
                action.serialize(document, recorder_element);
        }
        //-------------------------------------------------------------------------------------
        public void deserialize(XmlElement root)
        {
            foreach (XmlElement element in root.GetElementsByTagName(SerializableAction.ACTION_TAG))
            {
                XmlElement type_element = element.GetElementsByTagName(SerializableAction.ACTION_TYPE_TAG)[0] as XmlElement;

                int type;
                int.TryParse(type_element.InnerText.ToString(), out type);

                IXMLObject action = action_factory.createSerializableAction(type) as IXMLObject;
                action.deserialize(element);

                recorded_actions.Add(action);
            }
        }
    }
}

namespace Sophia.Deprecated
{ 
    public abstract class Recorder : ISerializable
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public abstract IActionFactory ActionFactory { get; }

        public IXMLObject XMLObject
        {
            get 
            {
                List<IXMLObject> serialized_actions = new List<IXMLObject>();
                for (int i = 0; i <= recording_index; ++i)
                    serialized_actions.Add(records[i].XMLObject);

                return new SerializableRecorder(ActionFactory, serialized_actions); 
            }
        }

        private int BeginRecordingIndex
        {
            get { return -1; }
        }
        private int EndRecordingIndex
        {
            get { return records.Count - 1; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly List<BaseAction> records;

        private int recording_index;

        //-------------------------------------------------------------------------------------
        public Recorder()
        {
            records = new List<BaseAction>();
            recording_index = BeginRecordingIndex;
        }

        //-------------------------------------------------------------------------------------
        public void record(BaseAction r)
        {
            if (recording_index != EndRecordingIndex)
                records.RemoveRange(recording_index + 1, (EndRecordingIndex - recording_index));

            records.Add(r);
            recording_index = EndRecordingIndex;
        }

        //-------------------------------------------------------------------------------------
        public void undo()
        {
            if (recording_index > 0)
            {
                BaseAction r = records[recording_index];
                r.unexecute();

                recording_index = Algorithms.clamp(recording_index - 1, BeginRecordingIndex, EndRecordingIndex);
            }
        }
        //-------------------------------------------------------------------------------------
        public void redo()
        {
            if (recording_index < EndRecordingIndex)
            {
                recording_index = Algorithms.clamp(recording_index + 1, BeginRecordingIndex, EndRecordingIndex);

                BaseAction r = records[recording_index];
                r.execute();
            }
        }

        //-------------------------------------------------------------------------------------
        public override string ToString()
        {
            string s = string.Empty;

            s += "Records recorded: ";
            s += "------------------";
            s += Environment.NewLine;

            foreach (IAction r in records)
            {
                s += r.GetType().ToString();
                if (r == records.Last())
                    continue;

                s += Environment.NewLine;
            }

            return s;
        }
    }
}
