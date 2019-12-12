namespace Sophia.Core
{
    public interface IPickUp
    {
        IActor Parent { get; }

        bool pickup();
        bool drop();
    }
}