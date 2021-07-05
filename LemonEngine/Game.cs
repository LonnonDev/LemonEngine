using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using static LemonEngine.Shapes;
using static LemonEngine.Color;
using System.Linq;
using System;
using OpenTK.Mathematics;

namespace LemonEngine
{
    public class Game : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private float movementLeftRight = 0;
        private float movementUpDown = 0;

        Rectangle player = new(
            100,
            100,
            100,
            100
        );
        Rectangle Cube = new(
            300,
            300,
            100,
            100
        );

        public float aspectRatio = 800 / 600;
        Shader _shader;


        float[] square;
        float[] cube;


        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        // Now, we start initializing OpenGL.
        protected override void OnLoad()
        {
            cube = new CreateRectangle(
                new Shapes.Vector2(
                    Cube.X,
                    Cube.Y,
                    Size
                ),
                Cube.Width,
                Cube.Height,
                Size
            )
            .CreateVertices();
            Renderer.AddToStatic(cube);

            RGBA bgColor = new(255, 192, 203, 255);
            bgColor.SetBackground();

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

            base.OnLoad();
        }

        private static float Clamp(float input, float min, float max)
        {
            return input < min ? min : (input > max ? max : input);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _shader.Use();
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            aspectRatio = (float)Size.X / (float)Size.Y;
            player.X = player.X + movementLeftRight;
            player.Y = player.Y + movementUpDown;

            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "objColor");

            float xClamp = (movementLeftRight / (float)Size.X + 1) / 2;
            float yClamp = (movementUpDown / (float)Size.Y + 1) / 2;
            float whiteness = (xClamp) * (yClamp);
            float red = Clamp((1 - xClamp) * yClamp + whiteness, 0f, 1f);
            float green = Clamp((1 - xClamp) * (1 - yClamp) + whiteness, 0f, 1f);
            float blue = Clamp(xClamp * (1 - yClamp) + whiteness, 0f, 1f);
            Console.WriteLine(red);

            GL.Uniform4(vertexColorLocation, red, green, blue, 255f);

            square = new CreateRectangle(
                new Shapes.Vector2(
                    player.X,
                    player.Y,
                    Size
                ),
                player.Width,
                player.Height,
                Size
                ).CreateVertices();
            Renderer.AddToDynamic(square);
            int[] rendererArray = Renderer.Render(
                _vertexArrayObject,
                _vertexBufferObject,
                _shader,
                movementUpDown,
                movementLeftRight,
                Size
            );
            _vertexBufferObject = rendererArray[0];
            _vertexArrayObject = rendererArray[1];
            

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            movementUpDown = 0;
            movementLeftRight = 0;
            float[] playerMovement = PlayerController.Movement(
                input, 
                movementUpDown, 
                movementLeftRight,
                player.X,
                player.Y,
                player.Width,
                player.Height,
                Size
            );
            movementUpDown = playerMovement[0];
            movementLeftRight = playerMovement[1];
            

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            aspectRatio = Size.X / Size.Y;
            GL.Viewport(0, 0, Size.X, Size.Y);
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            base.OnUnload();
        }

        private void PlayerBorderCollision() {
            
        }
    }
}
