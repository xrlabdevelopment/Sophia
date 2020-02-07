namespace Sophia.Platform
{
    using System.Collections.Generic;
    using Sophia.Core;
    using UnityEngine;

    public class SceneContainer
    {
        public int PrefabId = 0;
        public float PositionX = 0.0f;
        public float PositionY = 0.0f;
        public float PositionZ = 0.0f;
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float RotationZ = 0.0f;
        public float ScaleX = 0.0f;
        public float ScaleY = 0.0f;
        public float ScaleZ = 0.0f;
    }

    public class SaveLoadObjectContainer
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public SceneContainer[] SavedObjects;

    }
}
