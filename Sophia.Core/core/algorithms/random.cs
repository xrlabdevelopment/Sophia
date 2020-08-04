namespace Sophia
{
	/// <summary>
	/// Some convenience functions for random booleans and integers.
	/// </summary>
	public static class Random
	{
		private class RandomImpl : IRandom
        {
            //--------------------------------------------------------------------------------------
            // Fields
            private readonly System.Random random;

            //--------------------------------------------------------------------------------------
            public RandomImpl()
			{
				random = new System.Random();
			}
            //--------------------------------------------------------------------------------------
            public RandomImpl(int seed)
			{
				random = new System.Random(seed);
			}

            //--------------------------------------------------------------------------------------
            public double nextDouble()
			{
				return random.NextDouble();
			}
            //--------------------------------------------------------------------------------------
            public void nextBytes(byte[] bytes)
            {
                random.NextBytes(bytes);
            }

            //--------------------------------------------------------------------------------------
            public int next()
			{
				return random.Next();
			}
            //--------------------------------------------------------------------------------------
            public int next(int maxValue)
			{
				return random.Next(maxValue);
			}
            //--------------------------------------------------------------------------------------
            public int next(int minValue, int maxValue)
			{
				return random.Next(minValue, maxValue);
			}

            //--------------------------------------------------------------------------------------
            public override string ToString()
			{
				return random.ToString();
			}
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Globally accessible <see cref="System.Random"/> object for random calls
        /// </summary>
        public static readonly IRandom g_Random = new RandomImpl();

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates either -1.0f or 1.0f randomly.
        /// </summary>
        /// <returns></returns>
        public static float sign()
		{
			return boolean(0.5f) ? -1.0f : 1.0f;
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates a random bool, true with the given probability.
        /// </summary>
        /// <param name="probability"></param>
        /// <returns></returns>
        public static bool boolean(float probability)
		{
			return g_Random.nextDouble() < probability;
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates a Random integer between 0 inclusive and the given max, exclusive.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int range(int max)
		{
			return g_Random.next(max);
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates a Random integer between the given min inclusive and the given max, exclusive.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int range(int min, int max)
		{
			return g_Random.next(min, max);
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates a Random float between 0.0f inclusive and the given max
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float range(float max)
		{
			return (float)g_Random.nextDouble() * max;
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Generates a Random float between the given min inclusive and the given max, exclusive.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float range(float min, float max)
		{
			return range(max - min) + min;
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Gives a random value within a given range centered around a given value.
        /// </summary>
        /// <param name="value">The value around which the random values will be centered.</param>
        /// <param name="range">The range of the returned value.</param>
        /// <returns>A random value between value - range/2 and value + range/2.</returns>
        public static float randomOffset(float value, float range)
		{
            double offset = g_Random.nextDouble()*range - range/2;
			return (float) (value + offset);
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Gets a new random generator.
        /// </summary>
        /// <returns>A new random generator</returns>
        public static IRandom getRandom()
		{
			return new RandomImpl();
		}

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Gets a new random generator.
        /// </summary>
        /// <param name="seed">The seed to instantiate the generator with.</param>
        /// <returns>A seeded instance of a random generator.</returns>
        public static IRandom getRandom(int seed)
		{
			return new RandomImpl(seed);
		}
	}

	/// <summary>
	/// Represents a random generator.
	/// </summary>
	public interface IRandom
	{
		/// <summary>
		/// Gets the next the random double value.
		/// </summary>
		double nextDouble();

        /// <summary>
        /// Fills the given array with random bytes.
        /// </summary>
        void nextBytes(byte[] bytes);

        /// <summary>
        /// Gets the next the random integer value.
        /// </summary>
        int next();

		/// <summary>
		/// Gets the next the random integer value below the given maximum.
		/// </summary>
		int next(int maxValue);

		/// <summary>
		/// Gets the next the random integer value greater than or equal to the minimum 
		/// and below the given maximum.
		/// </summary>
		int next(int minValue, int maxValue);
	}
}
