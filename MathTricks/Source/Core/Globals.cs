namespace MathTricks
{
    public static class ScreenState
    {
        public static readonly string MainMenu = "MainMenu";
        public static readonly string EndScreen = "EndScreen";
        public static readonly string HelpScreen = "HelpScreen";

        public static readonly string GameScreen = "GameScreen";

        public static readonly string GameModes = "GameModes";
        public static readonly string Settings = "Settings";
    }

    static class Globals
    {
        public static int FieldWidth = 8, FieldHeight = 8;
        public static bool IsSinglePlayer = false;
        public static int GameFormat = 1;
        public static string WinningPlayerName = "";
    }
}
