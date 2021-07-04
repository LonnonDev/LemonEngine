using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace LemonEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new()
            {
                Size = new Vector2i(800, 800),
                Title = "LemonEngine",
            };

            using (Game game = new(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}
