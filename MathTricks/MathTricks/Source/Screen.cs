using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    abstract class Screen
    {
        public abstract void LoadContent(ContentManager manager);
        public abstract void Draw();
        public abstract void Update();

        protected Texture2D _pBackground;
        protected Rectangle _pBackgroundTransform;
    }

    public enum ApplicationState
    {
        MainMenu,
        Help,
        GameModes,
        SinglePlayer,
        MultiPlayer,
        EndScreen
    }

    static class ApplicationManager 
    {
        public static ApplicationState CurrentState = ApplicationState.MainMenu;
    }
}
