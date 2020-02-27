using System.Collections.Generic;
using System.Diagnostics;

namespace Sophia.IO
{
    public enum Extention
    {
        NONE,
        XML,
        JSON,
        TXT,
        INI,
        DLL,
        PDB
    }

    public static class Extentions
    {
        //-------------------------------------------------------------------------------------
        // Constants
        private static readonly string NONE_EXTENTION       = "";
        private static readonly string XML_EXTENTION        = ".xml";
        private static readonly string JSON_EXTENTION       = ".json";
        private static readonly string TXT_EXTENTION        = ".txt";
        private static readonly string INI_EXTENTION        = ".ini";
        private static readonly string DLL_EXTENTION        = ".dll";
        private static readonly string PDB_EXTENTION        = ".pdb";

        private static readonly string NONE_DISPLAY_STRING  = "Invalid";
        private static readonly string XML_DISPLAY_STRING   = "Extensible Markup Language";
        private static readonly string JSON_DISPLAY_STRING  = "JavaScript Object Notation";
        private static readonly string TXT_DISPLAY_STRING   = "Text";
        private static readonly string INI_DISPLAY_STRING   = "Informal Standard for Configuration Files";
        private static readonly string DLL_DISPLAY_STRING   = "Dynamic Link Library";
        private static readonly string PDB_DISPLAY_STRING   = "Program Database";

        private static readonly Dictionary<Extention, string> EXTENTIONS = new Dictionary<Extention, string>()
        {
            { Extention.NONE,   NONE_EXTENTION },
            { Extention.XML,    XML_EXTENTION },
            { Extention.JSON,   JSON_EXTENTION },
            { Extention.TXT,    TXT_EXTENTION },
            { Extention.INI,    INI_EXTENTION },
            { Extention.DLL,    DLL_EXTENTION },
            { Extention.PDB,    PDB_EXTENTION }
        };

        private static readonly Dictionary<Extention, string> EXTENTION_DISPLAY_STRINGS = new Dictionary<Extention, string>()
        {
            { Extention.NONE,   NONE_DISPLAY_STRING },
            { Extention.XML,    XML_DISPLAY_STRING  },
            { Extention.JSON,   JSON_DISPLAY_STRING },
            { Extention.TXT,    TXT_DISPLAY_STRING  },
            { Extention.INI,    INI_DISPLAY_STRING  },
            { Extention.DLL,    DLL_DISPLAY_STRING  },
            { Extention.PDB,    PDB_DISPLAY_STRING  }
        };

        //-------------------------------------------------------------------------------------
        public static string toString(Extention type)
        {
            if(EXTENTIONS.ContainsKey(type))
            {
                return EXTENTIONS[type];
            }

            Debug.Assert(false, "Extention type not found: " + type);
            return string.Empty;
        }
        //-------------------------------------------------------------------------------------
        public static string toDisplayString(Extention type)
        {
            if (EXTENTION_DISPLAY_STRINGS.ContainsKey(type))
            {
                return EXTENTION_DISPLAY_STRINGS[type];
            }

            Debug.Assert(false, "Extention type not found: " + type);
            return string.Empty;
        }
    }
}
