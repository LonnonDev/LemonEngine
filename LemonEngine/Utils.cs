using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Geometry;

namespace LemonEngine {
    public static class Utils {
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }
        /// <summary>
        /// Clamps a value such that <c>min</c> <= <c>input</c> <= <c>max</c>.
        /// </summary>
        /// <param name="input">Input to clamp</param>
        /// <param name="min">Minimum value of output</param>
        /// <param name="max">Maximum value of output</param>
        /// <returns>Clamped value</returns>
        public static float Clamp(float input, float min, float max) {
            return input < min ? min : (input > max ? max : input);
        }
        /// <summary>
        /// Clamps a value between 0.0 and 1.0.
        /// </summary>
        /// <param name="input">Value to clamp</param>
        /// <returns>A number ranging from 0.0 to 1.0</returns>
        public static float ClampNormalized(float input) {
            return Utils.Clamp(input, 0.0f, 1.0f);
        }
        /// <summary>
        /// Normalizes an input from min-max to 0.0-1.0
        /// </summary>
        /// <param name="input">Input to be normalized</param>
        /// <param name="min">Minimum value before normalization</param>
        /// <param name="max">Maximum value before normalization</param>
        /// <returns>Normalized input</returns>
        public static float Normalize(float input, float min, float max) {
            input = Utils.Clamp(input, min, max) - min;
            max = max - min;

            return input / max;
        }
        /// <summary>
        /// Convert coordinates from 0-width, 0-height to -1-1 coords
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        public static Vec2f ConvertCoordinates(Vec2f coords) {
            return new Vec2f(coords.X / WindowWidth, coords.Y / WindowHeight);
        }
        /// <summary>
        /// Update window dimensions when resized.
        /// </summary>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        public static void SetWindowDimensions(int width, int height) {
            WindowWidth = width;
            WindowHeight = height;
        }
    }
}
