using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Geometry {
    public class Vec2f {
        //public struct Vec2f {
        public float X { get; set; }
        public float Y { get; set; }

        public Vec2f(float x, float y) {
            X=x;
            Y=y;
        }
        public Vec2f Add(Vec2f Other) {
            return new Vec2f(X+Other.X, Y+Other.Y);
        }
        public Vec2f Subtract(Vec2f Other) {
            return new Vec2f(X-Other.X, Y-Other.Y);
        }
        public Vec2f DivideScalar(float Scalar) {
            if(Scalar == 0f) throw new ArgumentException("Cannot divide by scalar value of 0", "Scalar");
            return new Vec2f(X/Scalar, Y/Scalar);
        }
        public Vec2f MultiplyScalar(float Scalar) {
            return new Vec2f(X*Scalar, Y*Scalar);
        }
        public static Vec2f operator +(Vec2f v) => v;
        public static Vec2f operator -(Vec2f v) => v;
        public static Vec2f operator +(Vec2f v0, Vec2f v1) => v0.Add(v1);
        public static Vec2f operator -(Vec2f v0, Vec2f v1) => v0.Subtract(v1);
        public static Vec2f operator *(Vec2f v, float s) => v.MultiplyScalar(s);
        public static Vec2f operator *(float s, Vec2f v) => v.MultiplyScalar(s);
        public static Vec2f operator /(Vec2f v, float s) => v.DivideScalar(s);

        public override string ToString() => $"({X}, {Y})";
        //}
    }
}
