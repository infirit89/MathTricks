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
        private void _EscapeFromHelpScreenButtonEvent() => ApplicationManager.CurrentState = ApplicationState.MainMenu;

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");

            const int offsetXAndEscapeButtonY = 10;
            const int offsetY = 50;
            const int escapeButtonWidth = 50;
            const int escapeButtonHeight = 20;

            _EscapeFromHelpScreenButtonRectangle = new Rectangle(_WindowSize.X - (escapeButtonWidth + offsetXAndEscapeButtonY) , offsetXAndEscapeButtonY, escapeButtonWidth, escapeButtonHeight);

            Button _EscapeFromHelpScreenButton = new Button(_EscapeFromHelpScreenButtonRectangle, "Back", _Font, _HelpScreenManager);

            _EscapeFromHelpScreenButton.OnButtonPressedEvent = _EscapeFromHelpScreenButtonEvent;

            using (StreamReader streamReader = new StreamReader(@"Content/Help.txt"))
                _HelpText = streamReader.ReadToEnd();

            Text text = new Text(_HelpText, _Font, new Rectangle(offsetXAndEscapeButtonY, offsetY, _WindowSize.X , _WindowSize.Y ), _HelpScreenManager);
            text.Transform = new Rectangle(0, offsetY, text.Transform.Width, text.Transform.Height);

            text.Color = Color.White;

            _pBackground = manager.Load<Texture2D>("bg");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
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
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);
            _HelpScreenManager.Draw();

        }

        private UIManager _HelpScreenManager;
        private SpriteFont _Font;
        private string _HelpText;
    }
}
