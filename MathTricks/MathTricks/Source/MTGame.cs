using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{

    public class MTGame : Game
    {
        private MainScreen _MainScreen;
        private GameScreen _GameScreen;
        private EndScreen _EndScreen;
        private GameModeSelectionScreen _GameModeSelectionScreen;
        public MTGame()
        {
            GraphicsManager.Init(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _MainScreen = new MainScreen(Window.ClientBounds.Size);
            _EndScreen = new EndScreen(Window.ClientBounds.Size);  
            
            _GameScreen = new GameScreen(Window.ClientBounds.Size);
            _GameModeSelectionScreen = new GameModeSelectionScreen(Window.ClientBounds.Size);
            _GameScreen.OnGameLostEvent = _EndScreen.Lost;
            GraphicsManager.CreateRenderer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _MainScreen.LoadContent(Content);
            _EndScreen.LoadContent(Content);
            _GameScreen.LoadContent(Content);
            _GameModeSelectionScreen.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            MTMouse.Update();

            switch (ApplicationManager.CurrentState)
            {
                case ApplicationState.MainMenu: _MainScreen.Update(); break;
                case ApplicationState.GameModes: _GameModeSelectionScreen.Update(); break;
                case ApplicationState.SinglePlayer:
                case ApplicationState.MultiPlayer:
                    _GameScreen.Update(); break;
                case ApplicationState.EndScreen: _EndScreen.Update(); break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsManager.Begin();

            switch (ApplicationManager.CurrentState)
            {
                case ApplicationState.MainMenu: _MainScreen.Draw(); break;
                case ApplicationState.GameModes: _GameModeSelectionScreen.Draw(); break;
                case ApplicationState.SinglePlayer:
                case ApplicationState.MultiPlayer:
                    _GameScreen.Draw(); break;
                case ApplicationState.EndScreen: _EndScreen.Draw(); break;
            }

            GraphicsManager.End();

            base.Draw(gameTime);
        }
    }
}
