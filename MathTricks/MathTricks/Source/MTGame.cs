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
        private SettingsScreen _SettingsScreen;
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
            _SettingsScreen = new SettingsScreen(Window.ClientBounds.Size);
            _SettingsScreen.OnConfirmButtonPressed = () =>
            {
                ApplicationManager.CurrentState = ApplicationState.Game;
                _GameScreen.BeginGame();
            };
            GraphicsManager.CreateRenderer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _MainScreen.LoadContent(Content);
            _EndScreen.LoadContent(Content);
            _GameScreen.LoadContent(Content);
            _GameModeSelectionScreen.LoadContent(Content);
            _SettingsScreen.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            MTMouse.Update();

            switch (ApplicationManager.CurrentState)
            {
                case ApplicationState.MainMenu: _MainScreen.Update(); break;
                case ApplicationState.Settings: _SettingsScreen.Update(); break;
                case ApplicationState.GameModes: _GameModeSelectionScreen.Update(); break;
                case ApplicationState.Game: _GameScreen.Update(); break;
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
                case ApplicationState.Settings: _SettingsScreen.Draw(); break;
                case ApplicationState.GameModes: _GameModeSelectionScreen.Draw(); break;
                case ApplicationState.Game: _GameScreen.Draw(); break;
                case ApplicationState.EndScreen: _EndScreen.Draw(); break;
            }

            GraphicsManager.End();

            base.Draw(gameTime);
        }
    }
}
