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
        
        public HelpScreen(Point WindowSize)
        {
            _WindowSize = WindowSize; 
            _HelpScreenManager = new UIManager();    
        }

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Salvar");

            const int offsetXAndEscapeButtonY = 10;
            const int offsetY = 50;
            const int escapeButtonWidth = 70;
            const int escapeButtonHeight = 40;

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

            Vector2 buttonOffset = new Vector2(10.0f, 10.0f);
            Vector2 buttonSize = new Vector2(70.0f, 40.0f);
            Button escapeFromHelpScreenButton = new Button(
                                                        buttonOffset,
                                                        buttonSize,
                                                        "Back",
                                                        _Font,
                                                        Anchor.TopRight,
                                                        _ButtonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    ScreenManager.CurrentScreen = ScreenState.MainMenu;
                }
            };
            
            escapeFromHelpScreenButton.Text.Color = Color.WhiteSmoke;

            _HelpScreenManager.AddComponent(escapeFromHelpScreenButton);

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
            text.Color = Color.WhiteSmoke;

            _HelpScreenManager.AddComponent(text);
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
