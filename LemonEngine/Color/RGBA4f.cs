using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Color {
    /// <summary>
    /// 4-float color (r,g,b, and a), with no normalization, just 0-255.
    /// </summary>
    public class RGBA4f {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }
        /// <summary>
        /// Creates a non-normalized (0-255) <c>RGBA4f</c> from four 0-255 floats.
        /// </summary>
        /// <param name="r">Red value (0-255)</param>
        /// <param name="g">Green value (0-255)</param>
        /// <param name="b">Blue value (0-255)</param>
        /// <param name="a">Alpha value (0-255)</param>
        public RGBA4f(float r, float g, float b, float a) {
            R=Utils.Clamp(r, 0, 255);
            G=Utils.Clamp(g, 0, 255);
            B=Utils.Clamp(b, 0, 255);
            A=Utils.Clamp(a, 0, 255);
        }
        /// <summary>
        /// Generates a non-normalized (0-255) <c>RGBA4f</c> from a normalized <c>RGBA4fn</c>.
        /// </summary>
        /// <param name="color">Normalized color (r,g,b,a ranging from 0.0 to 1.0)</param>
        public RGBA4f(RGBA4fn color) {
            R=color.R*255;
            G=color.G*255;
            B=color.B*255;
            A=color.A*255;
        }
        public override string ToString() {
            return $"Color:{{R:{R}, G:{G}, B:{B}, A:{A}}}";
        }
    }
}
