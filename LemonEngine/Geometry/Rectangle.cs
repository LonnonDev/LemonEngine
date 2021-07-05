using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Geometry {
    public class Rectangle {
        public Triangle A { get; private set; }
        public Triangle B { get; private set; }
        
        /// <summary>
        /// Gives you the top-left coordinates of the rectangle.
        /// </summary>
        /// <returns><c>Vec2f</c> corresponding to the top left of this rectangle.</returns>
        public Vec2f GetTopLeft() {
            return A.P1;
        }
        /// <summary>
        /// Gives you the bottom-right coordinates of the rectangle.
        /// </summary>
        /// <returns><c>Vec2f</c> corresponding to the bottom right of this rectangle.</returns>
        public Vec2f GetBottomRight() {
            return B.P1;
        }
        /// <summary>
        /// Gets the vertices of the triangles
        /// </summary>
        /// <returns>Array of vertices -- first the top left triangle, then the bottom right.</returns>
        public virtual float[] GetVertices() {
            return A.GetVertices().Concat(B.GetVertices()).ToArray();
        }

        /// <summary>
        /// Private function used by constructors to generate the triangles to be rendered.
        /// </summary>
        /// <param name="topLeft">Top left point of rectangle</param>
        /// <param name="bottomRight">Bottom right point of rectangle</param>
        private void GenerateTriangles(Vec2f topLeft, Vec2f bottomRight) {
            // Ensure that topLeft is actually in the top left of the rectangle
            if(topLeft.X > bottomRight.X) {
                float xBk = topLeft.X;
                topLeft.X = bottomRight.X;
                bottomRight.X = xBk;
            }
            if(topLeft.Y > bottomRight.Y) {
                float yBk = topLeft.Y;
                topLeft.Y = bottomRight.Y;
                bottomRight.Y = yBk;
            }
            // Create the other two missing vertices of the rectangle
            Vec2f bottomLeft = new Vec2f(topLeft.X, bottomRight.Y);
            Vec2f topRight = new Vec2f(bottomRight.X, topLeft.Y);
            // Generate and set triangles
            A = new Triangle(topLeft, bottomLeft, topRight);
            B = new Triangle(bottomRight, topRight, bottomLeft);
        }

        /// <summary>
        /// Create a rectangle from the top left x and y, and the width and height.
        /// </summary>
        /// <param name="x">Top left coordinate X</param>
        /// <param name="y">Top left coordinate Y</param>
        /// <param name="w">Width of rectangle</param>
        /// <param name="h">Height of rectangle</param>
        public Rectangle(float x, float y, float w, float h) {
            GenerateTriangles(new Vec2f(x, y), new Vec2f(x+w, y+h));
        }
        /// <summary>
        /// Create a rectangle from the top left coordinate and the width and height.
        /// </summary>
        /// <param name="topLeft">Top left <c>Vec2f</c> coordinate</param>
        /// <param name="w">Width of rectangle</param>
        /// <param name="h">Height of rectangle</param>
        public Rectangle(Vec2f topLeft, float w, float h) {
            GenerateTriangles(topLeft, new Vec2f(topLeft.X+w, topLeft.Y+h));
        }
        /// <summary>
        /// Create a rectangle from two opposite <c>Vec2f</c>'s
        /// </summary>
        /// <param name="a">Vector 1</param>
        /// <param name="b">Vector 2</param>
        public Rectangle(Vec2f a, Vec2f b) {
            GenerateTriangles(a, b);
        }

        public CollisionType CollidesWith(Rectangle other) {
            Vec2f coordsTL = GetTopLeft();
            Vec2f coordsBR = GetBottomRight();
            Vec2f otherCoordsTL = other.GetTopLeft();
            Vec2f otherCoordsBR = other.GetBottomRight();

            bool touchOrIntersect = (coordsTL.X<=otherCoordsBR.X &&
                              coordsBR.X>=otherCoordsTL.X &&
                              coordsTL.Y<=otherCoordsBR.Y &&
                              coordsBR.Y>=otherCoordsTL.Y);
            if(touchOrIntersect) {
                bool intersect = (coordsTL.X<otherCoordsBR.X &&
                              coordsBR.X>otherCoordsTL.X &&
                              coordsTL.Y<otherCoordsBR.Y &&
                              coordsBR.Y>otherCoordsTL.Y);
                if(!intersect) {
                    return CollisionType.COLLINEAR;
                } else {
                    return CollisionType.INTERSECTS;
                }
            } else {
                return CollisionType.NONE;
            }
        }
        public override string ToString() {
            Vec2f pos1 = GetTopLeft();
            Vec2f pos2 = GetBottomRight();
            return $"Rectangle:{{x1:{pos1.X}, y1:{pos1.Y}, x2:{pos2.X}, y2:{pos2.Y}}}";
        }
        public string ToVertices() {
            return string.Join(" | ", GetVertices());
        }
    }
}
