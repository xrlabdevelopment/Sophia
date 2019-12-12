using Sophia.Core;

namespace Sophia.Platform
{
    public abstract class PickUp : BaseMonoBehaviour, IPickUp
    {
        public abstract IActor Parent { get; }

        public int ID
        {
            get
            {
                return GetInstanceID();
            }
        }
        public string Name
        {
            get
            {
                return gameObject.name;
            }
        }

        //--------------------------------------------------------------------------------------
        public abstract bool drop();
        //--------------------------------------------------------------------------------------
        public abstract bool pickup();
    }
}