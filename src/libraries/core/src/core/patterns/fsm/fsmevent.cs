namespace Sophia.Core
{
    public class FSMEvent
    {
        //--------------------------------------------------------------------------------
        // Fields
        private int id;
        private string name;

        //--------------------------------------------------------------------------------
        /// <summary>
        /// The name of the event
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="id">ID of the event</param>
        /// <param name="name">Name of the event</param>
        public FSMEvent(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Check if an event equals another event
        /// </summary>
        /// <param name="obj">The other event</param>
        /// <returns>True if we are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(FSMEvent))
                return false;
            FSMEvent other = (FSMEvent)obj;
            return id == other.id;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
