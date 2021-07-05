using OpenTK.Mathematics;
using System;
using System.Linq;

namespace LemonEngine.Geometry {
    public class Triangle {
        public Vec2f P1 { get; }
        public Vec2f P2 { get; }
        public Vec2f P3 { get; }

        public Triangle(Vec2f p1, Vec2f p2, Vec2f p3) {
                P1=p1;P2=p2;P3=p3;
        }
        /// <summary>
        /// Converts Triangle into a float array.
        /// </summary>
        /// <returns>The X,Y, and Z(always 0.0f) coordinates of each <c>Vec2f</c> in order, as a 9-length float array.</returns>
        public float[] GetVertices() {
            return new float[] {
                P1.X / Utils.WindowWidth, P1.Y / Utils.WindowHeight, 0.0f,
                P2.X / Utils.WindowWidth, P2.Y / Utils.WindowHeight, 0.0f,
                P3.X / Utils.WindowWidth, P3.Y / Utils.WindowHeight, 0.0f,
            };
        }
        /// <summary>
        /// Offsets the triangle and returns a new triangle.
        /// </summary>
        /// <param name="OffsetAmnt">Amount to offset by</param>
        /// <returns>An offset triangle</returns>
        public Triangle Offset(Vec2f OffsetAmnt) {
            return new Triangle(P1+OffsetAmnt, P2+OffsetAmnt, P3+OffsetAmnt);
        }
    }
}