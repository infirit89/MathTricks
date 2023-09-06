using System;

namespace MathTricks
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Application())
                game.Run();
        }
    }
}
