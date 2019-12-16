using Sophia.Core;

namespace Sophia.Platform
{
    public abstract class PickUp : BaseMonoBehaviour, IPickUp
    {
        public abstract IActor Parent { get; }

        public abstract int ID { get; }
        public abstract string Name { get; }

        //--------------------------------------------------------------------------------------
        public abstract bool drop();
        //--------------------------------------------------------------------------------------
        public abstract bool pickup();
    }
}