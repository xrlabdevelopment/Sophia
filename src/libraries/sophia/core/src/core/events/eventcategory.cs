namespace Sophia.Core
{
    public enum EventCategory
    {
        NONE        = 0,
        GAMEPLAY    = 1 << 0,
        LOGGING     = 1 << 1,

        ALL         = GAMEPLAY | LOGGING
    }
}
