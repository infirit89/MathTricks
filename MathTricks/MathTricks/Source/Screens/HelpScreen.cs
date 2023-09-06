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
        private Rectangle _EscapeFromHelpScreenButtonRectangle;
        
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");

            const int offsetXAndEscapeButtonY = 10;
            const int offsetY = 50;
            const int escapeButtonWidth = 50;
            const int escapeButtonHeight = 20;

            _EscapeFromHelpScreenButtonRectangle = new Rectangle(
                                                            _WindowSize.X - 
                                                                (escapeButtonWidth + 
                                                                offsetXAndEscapeButtonY) , 
                                                            offsetXAndEscapeButtonY, 
                                                            escapeButtonWidth, 
                                                            escapeButtonHeight);

            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            Button _EscapeFromHelpScreenButton = new Button(
                                                        _EscapeFromHelpScreenButtonRectangle,
                                                        "Back",
                                                        _Font,
                                                        _HelpScreenManager,
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
                                        _WindowSize.Y),
                            _HelpScreenManager);
            text.Transform = new Rectangle(0, offsetY, text.Transform.Width, text.Transform.Height);

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
