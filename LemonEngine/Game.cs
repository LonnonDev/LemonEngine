using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using LemonEngine.Entity;
using LemonEngine.Color;
using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Rectangle = LemonEngine.Geometry.Rectangle;
using Image = SixLabors.ImageSharp.Image;
using System.Collections.Generic;

namespace LemonEngine {
    public class Game : GameWindow {
        private int vbo;
        private int vao;

        Player player;
        Rectangle cube;
        Shader shader;


        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) {}
        /// <summary>
        /// Initializes OpenGL
        /// </summary>
        protected override void OnLoad() {
            Utils.SetWindowDimensions(Size.X, Size.Y);

            RGBA4fn bgColor = RGBA4fn.FromUnnormalized(255, 192, 203, 255);
            GL.ClearColor(bgColor.R, bgColor.G, bgColor.B, bgColor.A);
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            shader.Use();

            player = new Player(100, 100, 100, 100);
            cube = new Rectangle(300, 300, 300, 300);
            Renderer.AddToStatic(cube.GetVertices());
            Renderer.AddToStatic(cube.GetVertices());

            base.OnLoad();
        }
        protected override void OnRenderFrame(FrameEventArgs e) {
            shader.Use();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "objColor");
            GL.Uniform4(vertexColorLocation, player.Color.R, player.Color.G, player.Color.B, player.Color.A);
            
            Renderer.AddToDynamic(player.GetVertices());
            //Renderer.AddToDynamic(cube.GetVertices());
            Renderer.Render(out vbo, out vao);

            GC.KeepAlive(player);
            GC.KeepAlive(cube);
            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape)) {
                Close();
            }
            player.Update(input);
            player.Collide(cube);

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            Utils.SetWindowDimensions(Size.X, Size.Y);
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);

            base.OnUnload();
        }
    }
}
