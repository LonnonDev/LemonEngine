using OpenTK.Graphics.OpenGL4;

namespace LemonEngine
{
    class Color
    {
        public struct RGBA
        {
            public RGBA(float red, float green, float blue, float alpha)
            {
                Red = red / 255f;
                Green = green / 255f;
                Blue = blue / 255f;
                Alpha = alpha / 255f;
            }

            public float Red { get; }
            public float Green { get; }
            public float Blue { get; }
            public float Alpha { get; }

            public void SetBackground()
            {
                GL.ClearColor(this.Red, this.Green, this.Blue, this.Alpha);
            }
        }
    }
}
