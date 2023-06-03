using System;

namespace MathTricks
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new QueensGame())
                game.Run();
        }
    }
}
