using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class MainScreen : Screen
    {
        private Point _WindowSize;
        private Rectangle _PlayButtonRectanlge;
        private Rectangle _HelpButtonRectangle;

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");
            
            const int buttonWidth = 200;
            const int buttonHeight = 70;

            _PlayButtonRectanlge = new Rectangle(
                                            _WindowSize.X / 2 - buttonWidth / 2,
                                            _WindowSize.Y / 2 - buttonHeight * 2,
                                            buttonWidth,
                                            buttonHeight);
            _HelpButtonRectangle = new Rectangle(
                                            _WindowSize.X / 2 - buttonWidth / 2,
                                            _WindowSize.Y / 2 , buttonWidth,
                                            buttonHeight);
            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            Button _PlayButton = new Button(
                                        _PlayButtonRectanlge,
                                        "Play",
                                        _Font,
                                        _MainScreenManager,
                                        _ButtonTexture);
            Button _HelpButton = new Button(
                                        _HelpButtonRectangle,
                                        "Help",
                                        _Font,
                                        _MainScreenManager,
                                        _ButtonTexture);

            _PlayButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.GameModes;
            };

            _HelpButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.HelpScreen;
            };

            Background = manager.Load<Texture2D>("bg");
            BackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public MainScreen(Point WindowSize)
        {
            _WindowSize = WindowSize;
            _MainScreenManager = new UIManager();
        }


        public override void Update() 
        {
            _MainScreenManager.Update();
        }

        public override void Draw()
        {
            Renderer.AddQuad(BackgroundTransform, Color.White, Background);
            _MainScreenManager.Draw();
        }

        private UIManager _MainScreenManager;
        private SpriteFont _Font;
    }
}
