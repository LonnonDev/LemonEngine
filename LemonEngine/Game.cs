using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using static LemonEngine.Shapes;
using static LemonEngine.Color;
using System.Linq;
using System;

namespace LemonEngine
{
    public class Game : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private float movementLeftRight = 0;
        private float movementUpDown = 0;
        private float velLeftRight = 0;
        private float velUpDown = 0;

        public float aspectRatio = 800 / 600;
        Shader _shader;


        float[] square;


        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        // Now, we start initializing OpenGL.
        protected override void OnLoad()
        {

            RGBA bgColor = new(255, 192, 203, 255);
            bgColor.SetBackground();

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);
            aspectRatio = (float)Size.X / (float)Size.Y;
            square = new Rectangle(
                new Vector2(100 + movementLeftRight, 100 + movementUpDown, Size),
                new Vector2(100 + movementLeftRight, -100 + movementUpDown, Size),
                new Vector2(-100 + movementLeftRight, 100 + movementUpDown, Size),
                new Vector2(-100 + movementLeftRight, -100 + movementUpDown, Size))
            .ActuallyFuckingCreateObject();

            float[] _vertices = square;

            _vertexBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            SwapBuffers();

            _shader.Use();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = KeyboardState;
            var speed = 20f;


            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.W))
            {
                velUpDown = speed;
            }
            if (input.IsKeyDown(Keys.S))
            {
                velUpDown = -speed;
            }
            if (input.IsKeyDown(Keys.D))
            {
                velLeftRight = speed;
            }
            if (input.IsKeyDown(Keys.A))
            {
                velLeftRight = -speed;
            }

            if (Math.Abs(movementLeftRight) > 0 && Math.Abs(movementUpDown) > 0)
            {
                Console.WriteLine("hi");
                velUpDown /= (float)Math.Sqrt(2f);
                velLeftRight /= (float)Math.Sqrt(2f);
            }
            movementUpDown += velUpDown;
            movementLeftRight += velLeftRight;

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
    }
}
