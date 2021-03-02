using UnityEngine;

namespace Sophia.Platform.Extension
{
	/// <summary>
	/// Provides some utility functions for Colors.
	/// </summary>
	public static class ColorExtensions
	{
        //-------------------------------------------------------------------------------------
        // Constants
        private const float LightOffset = 0.0625f;
		private const float DarkerFactor = 0.9f;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a color lighter than the given color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color lighter(this Color color)
		{
			return new Color(
				Mathf.Clamp(color.r + LightOffset, 0.0f, 1.0f),
				Mathf.Clamp(color.g + LightOffset, 0.0f, 1.0f),
				Mathf.Clamp(color.b + LightOffset, 0.0f, 1.0f),
                Mathf.Clamp(color.a, 0.0f, 1.0f));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a color darker than the given color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color darker(this Color color)
		{
			return new Color(
                Mathf.Clamp(color.r - LightOffset, 0.0f, 1.0f),
                Mathf.Clamp(color.g - LightOffset, 0.0f, 1.0f),
                Mathf.Clamp(color.b - LightOffset, 0.0f, 1.0f),
                Mathf.Clamp(color.a, 0.0f, 1.0f));
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the brightness of the color, 
        /// defined as the average off the three color channels.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float brightness(this Color color)
		{
			return (color.r + color.g + color.b)/3;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a new color with the RGB values scaled so that the color has the given
        /// brightness.
        /// </summary>
        /// <remarks>
        /// If the color is too dark, a grey is returned with the right brightness. The alpha
        /// is left unchanged.
        /// </remarks>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        public static Color withBrightness(this Color color, float brightness)
		{
			if (color.isApproximatelyBlack())
			{
				return new Color(brightness, brightness, brightness, color.a);
			}
			
			float factor = brightness/color.brightness();

			float r = color.r*factor;
			float g = color.g*factor;
			float b = color.b*factor;

			float a = color.a;

			return new Color(r, g, b, a);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns whether the color is black or almost black.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool isApproximatelyBlack(this Color color)
		{
            return color.r <= float.Epsilon && color.g <= float.Epsilon && color.b <= float.Epsilon;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns whether the color is white or almost white.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool isApproximatelyWhite(this Color color)
		{
            return color.r >= 1.0f - float.Epsilon && color.g >= 1.0f - float.Epsilon && color.b >= 1.0f - float.Epsilon;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns an opaque version of the given color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color opaque(this Color color)
		{
			return new Color(color.r, color.g, color.b);
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the same color, but with the specified alpha.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns>Color.</returns>
        public static Color withAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Returns a new color that is this color inverted.
        /// </summary>
        /// <param name="color">The color to invert.</param>
        /// <returns></returns>
        public static Color invert(this Color color)
		{
			return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b, color.a);
		}
	}
}
