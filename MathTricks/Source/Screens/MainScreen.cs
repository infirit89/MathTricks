using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class MainScreen : Screen
    {
        private Point _WindowSize;

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Salvar");

            const int buttonWidth = 200;
            const int buttonHeight = 70;

            VerticalLayout layout = new VerticalLayout(
                                                    Vector2.Zero,
                                                    new Vector2(10.0f, 10.0f),
                                                    Anchor.CenterTop,
                                                    Sizing.ParentHeight);
            layout.Spacing = 10.0f;
                                                    
            _MainScreenManager.AddComponent(layout);

            Texture2D buttonTexture = manager.Load<Texture2D>("niggaButton");

            
            Button playButton = new Button(
                                        Vector2.Zero,
                                        new Vector2(buttonWidth, buttonHeight),
                                        "Play",
                                        _Font,
                                        Anchor.None,
                                        buttonTexture);
            playButton.Text.Color = Color.WhiteSmoke;

            layout.AddChild(playButton);
                  
            Button helpButton = new Button(
                                        Vector2.Zero,
                                        new Vector2(buttonWidth, buttonHeight),
                                        "Help",
                                        _Font,
                                        Anchor.None,
                                        buttonTexture);
            helpButton.Text.Color = Color.WhiteSmoke;

            layout.AddChild(helpButton);

            playButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.GameModes;
            };

            helpButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.HelpScreen;
            };

            Background = manager.Load<Texture2D>("MenuBg");

            int aspectRatio = _WindowSize.X / _WindowSize.Y;
            // BackgroundTransform = new Rectangle();

            // BackgroundTransform.Size = new Point((_WindowSize.X / 2) / 
            //                                         Background.Bounds.Size.X,
            //                                     (_WindowSize.Y / 2) / 
            //                                         Background.Bounds.Size.Y);

            // BackgroundTransform.Location = new Point(
            //                                     _WindowSize.X / 2 -
            //                                         BackgroundTransform.Size.X / 2,
            //                                     _WindowSize.Y / 2);
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

        public override void OnResize(Viewport viewport)
        {
            // int aspectRatio = viewport.Bounds.Size.X / viewport.Bounds.Size.Y;

            // BackgroundTransform.Size = new Point(Background.Bounds.Size.X / 
            //                                         (viewport.Bounds.Size.X / 2),
            //                                         Background.Bounds.Size.Y /
            //                                         (viewport.Bounds.Size.Y / 2));

            // BackgroundTransform.Location = new Point(
            //                                     viewport.Bounds.Size.X / 2 -
            //                                         BackgroundTransform.Size.X / 2,
            //                                     viewport.Bounds.Size.Y / 2);
        }

        public override void Draw()
        {
            // Renderer.AddQuad(BackgroundTransform, Color.White, Background);
            _MainScreenManager.Draw();
        }

        private UIManager _MainScreenManager;
        private SpriteFont _Font;
    }
}
