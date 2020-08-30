using System;

namespace Sophia.Platform.Gameplay
{
    //-------------------------------------------------------------------------------------
    /// <summary>
    /// An interface for an inventory item
    /// </summary>
    public interface IInventoryItem
    {
        Guid ID { get; }

        IInventoryItemView View { get; }
    }
}
