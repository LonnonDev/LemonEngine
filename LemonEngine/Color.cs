using OpenTK.Graphics.OpenGL4;

namespace LemonEngine
{
    class Color
    {
        public struct RGBA
        {
            public RGBA(float red, float green, float blue, float alpha)
            {
                R = red / 255f;
                G = green / 255f;
                B = blue / 255f;
                A = alpha / 255f;
            }

            public float R { get; }
            public float G { get; }
            public float B { get; }
            public float A { get; }

            public void SetBackground()
            {
                GL.ClearColor(this.R, this.G, this.B, this.A);
            }
        }
        public struct RGB
        {
            public RGB(float red, float green, float blue)
            {
                R = red / 255f;
                G = green / 255f;
                B = blue / 255f;
            }

            public float R { get; }
            public float G { get; }
            public float B { get; }
        }
    }
}
