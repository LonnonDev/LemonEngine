using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Color {
    /// <summary>
    /// A normalized RGBA4f (each value ranges from 0.0 to 1.0 inclusive)
    /// </summary>
    public class RGBA4fn {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }
        /// <summary>
        /// Generates a Normalized <c>RGBA4f</c> (<c>RGBA4fn</c>) from 4 color inputs ranging from 0.0 to 1.0 each
        /// </summary>
        /// <param name="r">Red value (0.0-1.0)</param>
        /// <param name="g">Green value (0.0-1.0)</param>
        /// <param name="b">Blue value (0.0-1.0)</param>
        /// <param name="a">Alpha value (0.0-1.0)</param>
        public RGBA4fn(float r, float g, float b, float a) {
            R=Utils.ClampNormalized(r);
            G=Utils.ClampNormalized(g);
            B=Utils.ClampNormalized(b);
            A=Utils.ClampNormalized(a);
        }
        /// <summary>
        /// Generates a Normalized <c>RGBA4f</c> (<c>RGBA4fn</c>) from a non-normalized <c>RGBA4f</c>
        /// </summary>
        /// <param name="color"><c>RGBA4f</c> to convert</param>
        public RGBA4fn(RGBA4f color) {
            R=Utils.Normalize(color.R, 0.0f, 255.0f);
            G=Utils.Normalize(color.G, 0.0f, 255.0f);
            B=Utils.Normalize(color.B, 0.0f, 255.0f);
            A=Utils.Normalize(color.A, 0.0f, 255.0f);
        }
        /// <summary>
        /// Generates a Normalized <c>RGBA4f</c> (<c>RGBA4fn</c>) from a non-normalized <c>RGBA4f</c>
        /// </summary>
        /// <param name="r">Red value (0.0-255.0)</param>
        /// <param name="g">Green value (0.0-255.0)</param>
        /// <param name="b">Blue value (0.0-255.0)</param>
        /// <param name="a">Alpha value (0.0-255.0)</param>
        /// <returns></returns>
        public static RGBA4fn FromUnnormalized(float r, float g, float b, float a) {
            return new RGBA4fn(
                        Utils.Normalize(r, 0.0f, 255.0f),
                        Utils.Normalize(g, 0.0f, 255.0f),
                        Utils.Normalize(g, 0.0f, 255.0f),
                        Utils.Normalize(a, 0.0f, 255.0f)
                       );
        }
        public override string ToString() {
            return $"Color:{{R:{R*255.0f}, G:{G*255.0f}, B:{B*255.0f}, A:{A*255.0f}}}";
        }
    }
}
