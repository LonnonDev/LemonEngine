using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static LemonEngine.Shapes;

namespace LemonEngine
{
    class PlayerController
    {
        public static float[] Movement(
            KeyboardState input,
            float movementUpDown,
            float movementLeftRight,
            float playerX,
            float playerY,
            int playerWidth,
            int playerHeight,
            Vector2i Size
        ) {
            var speed = 20f;
            float velUpDown = 0;
            float velLeftRight = 0;

            if (input.IsKeyDown(Keys.W)) {
                velUpDown = speed;
            }
            if (input.IsKeyDown(Keys.S)) {
                velUpDown = -speed;
            }
            if (input.IsKeyDown(Keys.D)) {
                velLeftRight = speed;
            }
            if (input.IsKeyDown(Keys.A)) {
                velLeftRight = -speed;
            }
            if (Math.Abs(velLeftRight) > 0 && Math.Abs(velUpDown) > 0) {
                velUpDown /= (float)Math.Sqrt(2f);
                velLeftRight /= (float)Math.Sqrt(2f);
            }
            
            float[] playerCollisionArray = PlayerCollision(
                movementUpDown,
                movementLeftRight,
                velUpDown,
                velLeftRight,
                playerX,
                playerY,
                playerWidth,
                playerHeight,
                Size
            );
            movementUpDown = playerCollisionArray[0];
            movementLeftRight = playerCollisionArray[1];
            if (movementUpDown != -Math.Pow(4,63)) {
                movementUpDown += velUpDown;
                movementLeftRight += velLeftRight;
            } else {
                movementUpDown = 0;
                movementLeftRight = 0;
            }

            return new float[] {
                movementUpDown, 
                movementLeftRight
            };
        }

        public static float[] PlayerCollision(
            float movementUpDown,
            float movementLeftRight,
            float velUpDown,
            float velLeftRight,
            float playerX,
            float playerY,
            int playerWidth,
            int playerHeight,
            Vector2i Size
        ) {
            Rectangle Cube = new(
                300,
                300,
                100,
                100
            );
            //Console.WriteLine($"{playerX < Cube.X + Cube.Width} {playerX + playerWidth > Cube.Y} {playerY < Cube.Y + Cube.Height} {playerY + playerHeight > Cube.Y}");
            // AABB
            if (playerX < Cube.X + Cube.Width &&
                playerX + playerWidth > Cube.Y &&
                playerY < Cube.Y + Cube.Height &&
                playerY + playerHeight > Cube.Y
            ){
                movementUpDown = -(float)Math.Pow(4, 63);
                movementLeftRight = 0;
            }
            return new float[] {
                movementUpDown, movementLeftRight
            };
        }
    }
}
