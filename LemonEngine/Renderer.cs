﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace LemonEngine
{
    class Renderer
    {
        static List<float> _verticesDynamic = new List<float>();
        static List<float> _verticesStatic = new List<float>();

        public static void AddToDynamic(float[] vertices) {
            _verticesDynamic.AddRange(vertices);
        }
        public static void AddToStatic(float[] vertices)
        {
            _verticesStatic.AddRange(vertices);
        }
        public static int TotalVertices() {
            return _verticesDynamic.Count/3+_verticesStatic.Count/3;
        }

        public static int[] Render(
            int VBO,
            int VAO,
            Shader shader,
            float movementUpDown,
            float movementLeftRight,
            Vector2i Size
        ) {
            float[] vertices = _verticesDynamic.ToArray().Concat(_verticesStatic.ToArray()).ToArray(); 



            VBO = GL.GenBuffer();
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, TotalVertices());
            _verticesDynamic = new List<float>();
            return new int[] {VBO, VAO};
        }
    }
}
