using System.Collections.Generic;
using System.IO;
using Sophia.IO;
using UnityEngine;

namespace Sophia.Editor
{
    public class PluginValidator
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private readonly PluginType plugin_type;
        private readonly string source_directory;
        private readonly PluginAssociations plugin_associations;

        //--------------------------------------------------------------------------------------
        public PluginValidator(string source, PluginType type)
        {
            UnityEngine.Debug.Assert(Directory.Exists(source), "Path does not represent a directory");

            source_directory = source;
            plugin_type = type;
            plugin_associations = new PluginAssociations();
        }

        //--------------------------------------------------------------------------------------
        public void validate()
        {
            //
            //  Cache plugin configuration FileExtensions
            //
            List<FileExtension> FileExtensions = plugin_associations.Extensions[plugin_type];

            //
            // We should only print this information in DEBUG mode.
            //
            if (plugin_type == PluginType.DEBUG)
            {
                List<string> string_list = new List<string>();
                foreach (FileExtension FileExtension in FileExtensions)
                    string_list.Add(FileExtension.ToString());

                UnityEngine.Debug.Log(string.Format("Allowed FileExtensions: {0}", string.Join(", ", string_list.ToArray())));
            }

            //
            // Calculate what files should be removed from the plugin folder
            //
            List<string> to_remove = new List<string>();
            foreach (string file_path in Directory.GetFiles(source_directory))
            {
                string full_file_path = source_directory + Path.GetFileName(file_path);

                //
                // Unsupported file format, should be removed.
                //
                FileExtension file_extionsion = FileExtensions.Find(ex => full_file_path.Contains(IO.FileExtensions.toString(ex)));
                if (file_extionsion == FileExtension.NONE)
                {
                    to_remove.Add(full_file_path);
                    UnityEngine.Debug.Log("FileExtension is NONE, marking for deletion: " + full_file_path);
                    continue;
                }

                //
                // Calculate removal of debugging or release DLL's depending on the setup PluginType.
                //
                switch (plugin_type)
                {
                    case PluginType.DEBUG:   full_file_path = removeReleasePlugin(full_file_path); break;
                    case PluginType.RELEASE: full_file_path = removeDebugPlugin(full_file_path);   break;
                }

                if (full_file_path != string.Empty)
                    to_remove.Add(full_file_path);
            }

            //
            // Remove files from source directory.
            //
            if (to_remove.Count == 0)
                return;

            foreach (string file_path in to_remove)
            {
                File.Delete(file_path);
                UnityEngine.Debug.Log("Deleted: " + file_path);
            }
        }

        //--------------------------------------------------------------------------------------
        private string removeReleasePlugin(string fullFilePath)
        {
            //
            // We are a DLL and do not have the debugging postfix.
            // File should be removed.
            //
            return fullFilePath.Contains(FileExtensions.toString(FileExtension.DLL)) && !fullFilePath.Contains(PostFix.DEBUG_POSTFIX)
                ? fullFilePath
                : string.Empty;
        }
        //--------------------------------------------------------------------------------------
        private string removeDebugPlugin(string fullFilePath)
        {
            //
            // We are a DLL and have the debugging postfix.
            // File should be removed.
            //
            return fullFilePath.Contains(FileExtensions.toString(FileExtension.DLL)) && fullFilePath.Contains(PostFix.DEBUG_POSTFIX)
                ? fullFilePath
                : string.Empty;
        }
    }
}
