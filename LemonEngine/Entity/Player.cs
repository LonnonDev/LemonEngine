using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using LemonEngine.Geometry;
using LemonEngine.Color;

namespace LemonEngine.Entity {
    public class Player : Rectangle {
        public Vec2f PosOff { get; set; }
        public RGBA4fn Color { get; set; }

        public const float MoveSpeed = 20f;

        public Player(float x, float y, float w, float h) : base(x, y, w, h) {
            PosOff = new(0f, 0f);
            Color = new(0f, 0f, 0f, 0f);
        }
        public void Update(KeyboardState input) {
            Vec2f vel = new(0f, 0f);

            // Calculate speed on both axes
            if(input.IsKeyDown(Keys.W)) {
                vel.Y += MoveSpeed;
            }
            if(input.IsKeyDown(Keys.S)) {
                vel.Y += -MoveSpeed;
            }
            if(input.IsKeyDown(Keys.D)) {
                vel.X += MoveSpeed;
            }
            if(input.IsKeyDown(Keys.A)) {
                vel.X += -MoveSpeed;
            }
            // If moving in both axes, divide each by sqrt(2) to keep speed on diagonals equal to speed on orthogonals
            if(Math.Abs(vel.X) > 0 && Math.Abs(vel.Y) > 0) {
                vel /= (float)Math.Sqrt(2f);
            }
            PosOff += vel;

            Vec2f coords = Utils.ConvertCoordinates(GetTopLeft()+PosOff);
            float xClamp = (coords.X+1)/2;
            float yClamp = (coords.Y+1)/2;
            float whiteness = (xClamp) * (yClamp);
            float red = Utils.ClampNormalized((1 - xClamp) * yClamp + whiteness);
            float green = Utils.ClampNormalized((1 - xClamp) * (1 - yClamp) + whiteness);
            float blue = Utils.ClampNormalized(xClamp * (1 - yClamp) + whiteness);
            Color = new(red, green, blue, 1.0f);
        }

        public void Collide(Rectangle obj) {
            // Console.WriteLine(this.CollidesWith(obj));
            if(this.CollidesWith(obj) == CollisionType.COLLINEAR) {
                Console.WriteLine("Hello");
            }
            if (this.CollidesWith(obj) == CollisionType.INTERSECTS) {
                Console.WriteLine("Hello");
            }
        }
        /// <summary>
        /// Gets the vertices of the triangles
        /// </summary>
        /// <returns>Array of vertices -- first the top left triangle, then the bottom right.</returns>
        public override float[] GetVertices() {
            return A.Offset(PosOff).GetVertices().Concat(B.Offset(PosOff).GetVertices()).ToArray();
        }
    }
}
