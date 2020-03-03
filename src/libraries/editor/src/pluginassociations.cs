using System.Collections.Generic;
using Sophia.IO;

namespace Sophia.Editor
{
    public class PluginAssociations
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public Dictionary<PluginType, List<Extention>> Extentions
        {
            get { return extention_associations; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<PluginType, List<Extention>> extention_associations = new Dictionary<PluginType, List<Extention>>()
        {
            { PluginType.DEBUG,   new List<Extention>() { Extention.DLL, Extention.PDB, Extention.META} },
            { PluginType.RELEASE, new List<Extention>() { Extention.DLL, Extention.META } }
        };

        //--------------------------------------------------------------------------------------
        public PluginAssociations()
        { }
    }
}
