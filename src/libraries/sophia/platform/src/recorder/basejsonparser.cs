using Sophia.Core;
using System.IO;
using UnityEngine;

namespace Sophia.Platform
{
    public class BaseJsonParser : BaseMonoBehaviour
    {
        protected string file_name = "Test_File";
        protected string file_path = "";

        protected void Awake()
        {
            file_path = Application.dataPath;
        }

        //--------------------------------------------------------------------------------------
        public virtual bool save<T>(ref T toSaveClass)
        {
            //If class is null fail the save function
            if (toSaveClass == null)
                return false;

            file_path += "/" + file_name + ".json";
            //string output = JsonConvert.SerializeObject(toSaveClass);
            Debug.Log("saved to: " + file_path);

            //Try to write to file
            using (StreamWriter file = File.AppendText(file_path))
            {
              //  file.Write(output);
                return true;
            }

            //if it got here the streamwriter failed and the save function should fail
            return false;
        }

        //--------------------------------------------------------------------------------------
        public virtual bool load<T>(ref T toLoadClass)
        {
            file_path += file_name + ".json";
            string output = "";

            using (StreamReader reader = new StreamReader(file_path))
            {
                output = reader.ReadToEnd();
               //toLoadClass = JsonConvert.DeserializeObject<T>(output);
                return true;
            }

            return false;

        }
    }
}