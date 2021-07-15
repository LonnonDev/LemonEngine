
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace LemonEngine {
    class Renderer {
        static List<float> verticesDynamic = new List<float>();
        static List<float> verticesStatic = new List<float>();

        public static void AddToDynamic(float[] vertices) {
            verticesDynamic.AddRange(vertices);
        }
        public static void AddToStatic(float[] vertices) {
            verticesStatic.AddRange(vertices);
        }
        public static int TotalVertices() {
            return verticesDynamic.Count/3+verticesStatic.Count/3;
        }
        public static void Render(out int vbo,out int vao) {
            float[] vertices = verticesDynamic.Concat(verticesStatic).ToArray();

            vbo = GL.GenBuffer();
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, TotalVertices());
            verticesDynamic = new List<float>();
        }
    }
}