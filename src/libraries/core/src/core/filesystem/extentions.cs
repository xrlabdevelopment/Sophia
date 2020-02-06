using System.Collections.Generic;
using System.Diagnostics;

namespace Sophia.Core
{
    public enum Extention
    {
        XML,
        JSON,
        TXT,
        INI
    }

    public static class Extentions
    {
        //-------------------------------------------------------------------------------------
        // Constants
        private static readonly string XML_EXTENTION    = ".xml";
        private static readonly string JSON_EXTENTION   = ".json";
        private static readonly string TXT_EXTENTION    = ".txt";
        private static readonly string INI_EXTENTION    = ".ini";

        private static readonly Dictionary<Extention, string> EXTENTIONS = new Dictionary<Extention, string>()
        {
            { Extention.XML,    XML_EXTENTION },
            { Extention.JSON,   JSON_EXTENTION },
            { Extention.TXT,    TXT_EXTENTION },
            { Extention.INI,    INI_EXTENTION },
        };

        //-------------------------------------------------------------------------------------
        public static string getExtention(Extention type)
        {
            if(EXTENTIONS.ContainsKey(type))
            {
                return EXTENTIONS[type];
            }

            Debug.Assert(false, "Extention type not found: " + type);
            return string.Empty;
        }
    }
}
