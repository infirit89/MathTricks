using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MathTricks
{
    public class Application : Game
    {
        public static Application Instance => s_Instance;

        public Application()
        {
            s_Instance = this;
            _GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize()
        {
            Renderer.Init();

            ScreenManager.Init();

            ScreenManager.AddScreen(
                                ScreenState.MainMenu,
                                new MainScreen(Window.ClientBounds.Size));

            ScreenManager.AddScreen(
                                ScreenState.EndScreen,
                                new EndScreen(Window.ClientBounds.Size));

            ScreenManager.AddScreen(
                                ScreenState.HelpScreen,
                                new HelpScreen(Window.ClientBounds.Size));

            ScreenManager.AddScreen(
                                ScreenState.GameScreen,
                                new GameScreen(Window.ClientBounds.Size));

            ScreenManager.AddScreen(
                                ScreenState.GameModes,
                                new GameModeSelectionScreen(Window.ClientBounds.Size));
            ScreenManager.AddScreen(
                                ScreenState.Settings, 
                                new SettingsScreen(Window.ClientBounds.Size));

            ScreenManager.CurrentScreen = ScreenState.MainMenu;

            base.Initialize();
        }

        protected override void EndRun()
        {
            Renderer.Shutdown();
            base.EndRun();
        }

        protected override void LoadContent()
        {
            ScreenManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            ScreenManager.UpdateCurrent();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Renderer.Begin();

            ScreenManager.DrawCurrent();

            Renderer.End();

            base.Draw(gameTime);
        }

        private void OnResize(object sender, EventArgs e) 
        {
            _GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            _GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height;
            _GraphicsDeviceManager.ApplyChanges();
        }

        private static Application s_Instance;
        private GraphicsDeviceManager _GraphicsDeviceManager;
    }
}
