namespace Sophia
{
    namespace IO
    {
        public static class Helpers
        {
            public static bool isDebugFile(string name)
            {
                return name.Contains(Sophia.IO.PostFix.DEBUG_POSTFIX + ".");
            }
        }
    }
}
