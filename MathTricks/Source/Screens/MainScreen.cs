﻿using System;
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
            _Font = manager.Load<SpriteFont>("Arial");
            
            const float buttonWidth = 0.5f;
            const float buttonHeight = 0.5f;


            Transform2D helpButtonTransform = new Transform2D() 
            {
                Position = new Vector2(
                                    _WindowSize.X / 2 - buttonWidth / 2,
                                    _WindowSize.Y / 2),

                Size = new Vector2(buttonWidth, buttonHeight)
            };

            Texture2D buttonTexture = manager.Load<Texture2D>("niggaButton");

            Transform2D playButtonTransform = new Transform2D() 
            {
                Position = new Vector2(
                                    _WindowSize.X / 2 - (buttonTexture.Bounds.Width / 2 * buttonWidth),
                                    _WindowSize.Y / 2 - (buttonTexture.Bounds.Height / 2 * buttonHeight)),

                Size = new Vector2(buttonWidth, buttonHeight)
            };
            
            Button playButton = new Button(
                                        playButtonTransform,
                                        "Play",
                                        _Font,
                                        buttonTexture);

            _MainScreenManager.AddComponent(playButton);
                  
            // Button helpButton = new Button(
            //                             helpButtonTransform,
            //                             "Help",
            //                             _Font,
            //                             buttonTexture);

            // _MainScreenManager.AddComponent(helpButton);

            playButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.GameModes;
            };

            // helpButton.OnButtonPressedEvent = () => 
            // {
            //     ScreenManager.CurrentScreen = ScreenState.HelpScreen;
            // };

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
