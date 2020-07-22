using System;

namespace Sophia
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
		}
	}
}
