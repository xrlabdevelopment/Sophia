using System.Collections.Generic;
using System.Diagnostics;

namespace Sophia
{
    namespace IO
    {
        public enum FileExtension
        {
            NONE,
            XML,
            JSON,
            TXT,
            INI,
            DLL,
            PDB,
            META
        }

        public static class FileExtensions
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
            private static readonly string META_EXTENTION       = ".meta";

            private static readonly string NONE_DISPLAY_STRING  = "Invalid";
            private static readonly string XML_DISPLAY_STRING   = "Extensible Markup Language";
            private static readonly string JSON_DISPLAY_STRING  = "JavaScript Object Notation";
            private static readonly string TXT_DISPLAY_STRING   = "Text";
            private static readonly string INI_DISPLAY_STRING   = "Informal Standard for Configuration Files";
            private static readonly string DLL_DISPLAY_STRING   = "Dynamic Link Library";
            private static readonly string PDB_DISPLAY_STRING   = "Program Database";
            private static readonly string META_DISPLAY_STRING  = "Unity Engine Meta Data";

            private static readonly Dictionary<FileExtension, string> EXTENTIONS = new Dictionary<FileExtension, string>()
            {
                { FileExtension.NONE,   NONE_EXTENTION },
                { FileExtension.XML,    XML_EXTENTION  },
                { FileExtension.JSON,   JSON_EXTENTION },
                { FileExtension.TXT,    TXT_EXTENTION  },
                { FileExtension.INI,    INI_EXTENTION  },
                { FileExtension.DLL,    DLL_EXTENTION  },
                { FileExtension.PDB,    PDB_EXTENTION  },
                { FileExtension.META,   META_EXTENTION }
            };

            private static readonly Dictionary<FileExtension, string> EXTENTION_DISPLAY_STRINGS = new Dictionary<FileExtension, string>()
            {
                { FileExtension.NONE,   NONE_DISPLAY_STRING },
                { FileExtension.XML,    XML_DISPLAY_STRING  },
                { FileExtension.JSON,   JSON_DISPLAY_STRING },
                { FileExtension.TXT,    TXT_DISPLAY_STRING  },
                { FileExtension.INI,    INI_DISPLAY_STRING  },
                { FileExtension.DLL,    DLL_DISPLAY_STRING  },
                { FileExtension.PDB,    PDB_DISPLAY_STRING  },
                { FileExtension.META,   META_DISPLAY_STRING }
            };

            //-------------------------------------------------------------------------------------
            public static string toString(FileExtension type)
            {
                if (EXTENTIONS.ContainsKey(type))
                {
                    return EXTENTIONS[type];
                }

                System.Diagnostics.Debug.Assert(false, "Extension type not found: " + type);
                return string.Empty;
            }
            //-------------------------------------------------------------------------------------
            public static string toDisplayString(FileExtension type)
            {
                if (EXTENTION_DISPLAY_STRINGS.ContainsKey(type))
                {
                    return EXTENTION_DISPLAY_STRINGS[type];
                }

                System.Diagnostics.Debug.Assert(false, "Extension type not found: " + type);
                return string.Empty;
            }
        }
    }
}
