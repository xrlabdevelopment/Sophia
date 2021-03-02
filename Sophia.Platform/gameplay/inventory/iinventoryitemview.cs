using UnityEngine;

namespace Sophia.Platform.Gameplay
{
    //-------------------------------------------------------------------------------------
    /// <summary>
    /// An interface for an inventory item view
    /// </summary>
    public interface IInventoryItemView
    {
        Texture Visualization { get; }

        string DisplayName { get; }
    }
}
