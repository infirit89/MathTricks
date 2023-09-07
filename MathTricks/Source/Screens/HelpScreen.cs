using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class HelpScreen : Screen
    {
        private Point _WindowSize;
        private Rectangle _HelpButtonRectangle;
        
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");

            const int offsetXAndEscapeButtonY = 10;
            const int offsetY = 50;
            const int escapeButtonWidth = 50;
            const int escapeButtonHeight = 20;

            Transform2D returnToMainMenuButtonTransform = new Transform2D() 
            {
                Position = new Vector2(
                                    _WindowSize.X - 
                                        (escapeButtonWidth + 
                                        offsetXAndEscapeButtonY), 
                                    offsetXAndEscapeButtonY),

                Size = new Vector2(escapeButtonWidth, escapeButtonHeight)
            };

            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            Button _EscapeFromHelpScreenButton = new Button(
                                                        returnToMainMenuButtonTransform,
                                                        "Back",
                                                        _Font,
                                                        _ButtonTexture);

            _EscapeFromHelpScreenButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.MainMenu;
            };

            using (StreamReader streamReader = new StreamReader(@"Content/Help.txt"))
                _HelpText = streamReader.ReadToEnd();

            Text text = new Text(
                            _HelpText,
                            _Font,
                            new Rectangle(
                                        offsetXAndEscapeButtonY,
                                        offsetY,
                                        _WindowSize.X,
                                        _WindowSize.Y));
            text.Transform.Position = new Vector2(0, offsetY);

            text.Color = Color.White;
        }

        public HelpScreen(Point WindowSize)
        {
            _WindowSize = WindowSize; 
            _HelpScreenManager = new UIManager();    
        }


        public override void Update() 
        {
            _HelpScreenManager.Update();
        }

        public override void Draw() 
        {
            _HelpScreenManager.Draw();
        }

        private UIManager _HelpScreenManager;
        private SpriteFont _Font;
        private string _HelpText;
    }
}
