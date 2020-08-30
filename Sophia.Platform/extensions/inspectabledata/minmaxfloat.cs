using System;

namespace Sophia.Platform.Extension
{
	/// <summary>
	/// Class for representing a bounded range.
	/// </summary>
	[Serializable]
	public class MinMaxFloat
	{
        //-------------------------------------------------------------------------------------
        // Properties
        public float Min { get; set; } = 0.0f;
		public float Max { get; set; } = 1.0f;

        public float Level
        {
            get
            {
                return ((Max - Min) * 0.5f);
            }
        }
        public float Window
        {
            get { return (int)(Max - Min); }
        }

        //-------------------------------------------------------------------------------------
        public MinMaxFloat()
		{
			Min = 0.0f;
			Max = 1.0f;
		}

        //-------------------------------------------------------------------------------------
        public MinMaxFloat(float min, float max)
		{
			this.Min = min;
			this.Max = max;
		}
	}
}
