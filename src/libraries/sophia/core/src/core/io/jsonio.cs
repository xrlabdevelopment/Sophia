using Sophia.Core;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
namespace Sophia.Core
{
    public static class JsonIO
    {
        //--------------------------------------------------------------------------------------
        // Constants
        private static readonly string JSON_EXTENTION = ".json";

        //--------------------------------------------------------------------------------------
        public static string save<T>(string filePath, string fileName, T toSaveClass)
        {
            Debug.Assert(string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName), "File path and file name needs to be filled in.");

            if (toSaveClass == null)
                return string.Empty;

            //
            // Generate output path
            //
            string path = Path.Combine(filePath, fileName + JSON_EXTENTION);
            if (!File.Exists(path))
                return string.Empty;

            //
            // Write file to disk
            //
            using (StreamWriter file = File.AppendText(filePath))
            {
                string output = JsonConvert.SerializeObject(toSaveClass);
                if (string.IsNullOrEmpty(output))
                    return string.Empty;

                file.Write(output);
                return output;
            }
        }
        //--------------------------------------------------------------------------------------
        public static T load<T>(string filePath, string fileName)
        {
            Debug.Assert(string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName), "File path and file name needs to be filled in.");

            //
            // Generate input path
            //
            string path = Path.Combine(filePath, fileName + JSON_EXTENTION);

            if (!File.Exists(path))
                return default(T);

            //
            // Read file from disk
            //
            using (StreamReader reader = new StreamReader(filePath))
            {
                string input = reader.ReadToEnd();
                if (string.IsNullOrEmpty(input))
                    return default(T);

                T output = JsonConvert.DeserializeObject<T>(input);
                if (output == null)
                    return default(T);

                return output;
            }
        }
    }
}
