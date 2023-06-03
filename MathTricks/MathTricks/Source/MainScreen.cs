using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    class MainScreen : Screen
    {
        private Point _WindowSize;
        private Rectangle _PlayButtonRectanlge;
        private Rectangle _HelpButtonRectangle;
        private Rectangle _EscapeFromHelpScreenButtonRectanlge;
        private bool _HelpScreenIsShown = false;

        private void PlayButtonEvent() => ApplicationManager.CurrentState = ApplicationState.Game;

        private void HelpButtonEvent() => _HelpScreenIsShown = true;

        private void _EscapeFromHelpScreenButtonEvent() => _HelpScreenIsShown = false;

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");

            const int offsetXAndEscapeButtonY = 10;
            const int offsetY = 50;
            const int buttonWidth = 150;
            const int buttonHeight = 50;
            const int escapeButtonWidth = 50;
            const int escapeButtonHeight = 20;

            _PlayButtonRectanlge = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 - buttonHeight * 2, buttonWidth, buttonHeight);
            _HelpButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 , buttonWidth, buttonHeight);
            _EscapeFromHelpScreenButtonRectanlge = new Rectangle(_WindowSize.X - (escapeButtonWidth + offsetXAndEscapeButtonY) , offsetXAndEscapeButtonY, escapeButtonWidth, escapeButtonHeight);

            Button _PlayButton = new Button(_PlayButtonRectanlge, "Play", _Font, _MainScreenManager);
            Button _HelpButton = new Button(_HelpButtonRectangle, "Help", _Font, _MainScreenManager);
            Button _EscapeFromHelpScreenButton = new Button(_EscapeFromHelpScreenButtonRectanlge, "Back", _Font, _HelpScreenManager);

            _PlayButton.OnButtonPressedEvent = PlayButtonEvent;
            _HelpButton.OnButtonPressedEvent = HelpButtonEvent;
            _EscapeFromHelpScreenButton.OnButtonPressedEvent = _EscapeFromHelpScreenButtonEvent;

            using (StreamReader streamReader = new StreamReader(@"Content/Help.txt"))
                _HelpText = streamReader.ReadToEnd();

            Text text = new Text(_HelpText, _Font, new Rectangle(offsetXAndEscapeButtonY, offsetY, _WindowSize.X , _WindowSize.Y ), _HelpScreenManager);
            text.Transform = new Rectangle(0, offsetY, text.Transform.Width, text.Transform.Height);

            text.Color = Color.White;

            _pBackground = manager.Load<Texture2D>("unknown");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public MainScreen(Point WindowSize)
        {
            _WindowSize = WindowSize;

            _MainScreenManager = new UIManager();
            _HelpScreenManager = new UIManager();    
        }


        public override void Update() 
        {
            if (_HelpScreenIsShown)
                _HelpScreenManager.Update();
            else
                _MainScreenManager.Update();
        }

        public override void Draw() 
        {
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);

            if (_HelpScreenIsShown) {
                _HelpScreenManager.Draw();
            }

            else
                _MainScreenManager.Draw();
            

        }

        private UIManager _MainScreenManager, _HelpScreenManager;
        private SpriteFont _Font;
        private string _HelpText;
    }
}
