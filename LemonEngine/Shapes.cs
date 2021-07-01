using OpenTK.Mathematics;
using System;
using System.Linq;

namespace LemonEngine
{
    class Shapes
    {
        static public float map(float value,
                              float istart,
                              float istop,
                              float ostart,
                              float ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }

        private const float divideBy = 1000f;
        public struct Vector2
        {
            public Vector2(float x, float y, Vector2i size)
            {
                X = x;
                Y = y;
                Size = size;
            }
            public float X { get; }
            public float Y { get; }

            public Vector2i Size { get; private set; }

            public override string ToString() => $"({X}, {Y})";
            public Vector2 SizeIt()
            {
                float NewX = X / (float)Size.X;
                float NewY = Y / (float)Size.Y;
                return new Vector2(NewX, NewY, Size);
            }
        }

        public struct Triangle
        {
            public Triangle(Vector2 Point1, Vector2 Point2, Vector2 Point3)
            {
                P1 = Point1.SizeIt();
                P2 = Point2.SizeIt();
                P3 = Point3.SizeIt();
            }

            public Vector2 P1 { get; }
            public Vector2 P2 { get; }
            public Vector2 P3 { get; }
            public float[] CreateVertices()
            {
                return new float[]
                    {
                        P1.X, P1.Y, 0.0f,
                        P2.X, P2.Y, 0.0f,
                        P3.X, P3.Y, 0.0f,
                    };
            }
        }
        public struct Rectangle
        {
            public Rectangle(Vector2 Point1, Vector2 Point2, Vector2 Point3, Vector2 Point4)
            {
                P1 = Point1;
                P2 = Point2;
                P3 = Point3;
                P4 = Point4;
            }

            public Vector2 P1 { get; }
            public Vector2 P2 { get; }
            public Vector2 P3 { get; }
            public Vector2 P4 { get; }

            public float[] CreateVertices()
            {
                float[] firstTriangle = new Triangle(P4, P2, P3).CreateVertices();
                float[] secondTriangle = new Triangle(P1, P2, P3).CreateVertices();
                float[] finalVertices = firstTriangle.Concat(secondTriangle).ToArray();

                return finalVertices;
            }
            public float[] ActuallyFuckingCreateObject()
            {
                float[] firstTriangle = new Triangle(P4, P2, P3).CreateVertices();
                float[] secondTriangle = new Triangle(P1, P2, P3).CreateVertices();
                float[] finalVertices = firstTriangle.Concat(secondTriangle).ToArray();

                return finalVertices;
            }
        }
    }
}
