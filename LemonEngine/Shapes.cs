using OpenTK.Mathematics;
using System;
using System.Linq;

namespace LemonEngine
{
    class Shapes
    {
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
            public Vector2i Size { get; }

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
                // Console.WriteLine($"START ({P1.X}, {P1.Y}), ({P2.X}, {P2.Y}), ({P3.X}. {P3.Y})");
                return new float[]
                    {
                        P1.X, P1.Y, 0.0f,
                        P2.X, P2.Y, 0.0f,
                        P3.X, P3.Y, 0.0f,
                    };
            }
        }
        public struct CreateRectangle
        {
            public CreateRectangle(Vector2 Point1, Vector2 Point2, Vector2i Size) : this(
                Point1, 
                Point2, 
                new Vector2(Point1.X, Point2.Y, Size), 
                new Vector2(Point2.X, Point1.Y, Size)
            ){}
            public CreateRectangle(Vector2 Point1, int Width, int Height, Vector2i Size) : this(
                Point1,
                new Vector2(Point1.X + Width, Point1.Y + Height, Size),
                new Vector2(Point1.X, Point1.Y+ Height, Size),
                new Vector2(Point1.X+Width, Point1.Y, Size)
            )
            { }
            public CreateRectangle(Vector2 Point1, Vector2 Point2, Vector2 Point3, Vector2 Point4) {
                P1 = Point1;
                P4 = Point2;
                P3 = Point3;
                P2 = Point4;
            }

            public Vector2 P1 { get; }
            public Vector2 P2 { get; }
            public Vector2 P3 { get; }
            public Vector2 P4 { get; }

            public float[] CreateVertices()
            {
                float[] firstTriangle = new Triangle(P1, P2, P3).CreateVertices();
                float[] secondTriangle = new Triangle(P4, P2, P3).CreateVertices();
                float[] finalVertices = firstTriangle.Concat(secondTriangle).ToArray();
                
                return finalVertices;
            }
        }
        public struct Rectangle {
            public Rectangle(float x, float y, int w, int h)
            {
                X = x;
                Y = y;
                Width = w;
                Height = h;
            }

            public float X { get; set; }
            public float Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}