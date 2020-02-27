namespace Sophia.Core
{
    public interface IActor
    {
        //--------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// ID of the actor
        /// </summary>
        int InstanceID { get; }

        /// <summary>
        /// Name of the actor
        /// </summary>
        string Name { get; }
    }
}
