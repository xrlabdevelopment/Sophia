using System.Collections.Generic;
using Sophia.IO;

namespace Sophia.Editor
{
    public class PluginAssociations
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public Dictionary<PluginType, List<FileExtension>> Extensions { get; } = new Dictionary<PluginType, List<FileExtension>>()
        {
            { PluginType.DEBUG,   new List<FileExtension>() { FileExtension.DLL, FileExtension.PDB, FileExtension.META} },
            { PluginType.RELEASE, new List<FileExtension>() { FileExtension.DLL, FileExtension.META } }
        };

        //--------------------------------------------------------------------------------------
        public PluginAssociations()
        { }
    }
}
