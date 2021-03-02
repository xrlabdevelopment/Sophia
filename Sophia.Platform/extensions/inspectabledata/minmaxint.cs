using System;
using System.Diagnostics;

namespace Sophia.Platform.Extension
{
	/// <summary>
	/// Class for representing a bounded range.
	/// </summary>
	[Serializable]
	public class MinMaxInt
	{
        //-------------------------------------------------------------------------------------
        // Properties
        public int Min { get; set; } = 0;
		public int Max { get; set; } = 1;

        public int Level
        {
            get { return (int)((Max - Min) * 0.5f); }
        }
        public int Window
        {
            get { return (int)(Max - Min); }
        }

        //-------------------------------------------------------------------------------------
        public MinMaxInt()
		{
			Min = 0;
			Max = 1;
		}
        //-------------------------------------------------------------------------------------
        public MinMaxInt(int min, int max)
		{
			this.Min = min;
			this.Max = max;

            Debug.Assert(Max > Min, "Maximum is smaller than the minimum");
		}
	}
}
