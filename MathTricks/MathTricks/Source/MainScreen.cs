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

        private void PlayButtonEvent() => ApplicationManager.CurrentState = ApplicationState.GameModes;

        private void HelpButtonEvent() => ApplicationManager.CurrentState = ApplicationState.Help;
        
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");
            
            const int buttonWidth = 150;
            const int buttonHeight = 50;

            _PlayButtonRectanlge = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 - buttonHeight * 2, buttonWidth, buttonHeight);
            _HelpButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 , buttonWidth, buttonHeight);
            
            Button _PlayButton = new Button(_PlayButtonRectanlge, "Play", _Font, _MainScreenManager);
            Button _HelpButton = new Button(_HelpButtonRectangle, "Help", _Font, _MainScreenManager);

            _PlayButton.OnButtonPressedEvent = PlayButtonEvent;
            _HelpButton.OnButtonPressedEvent = HelpButtonEvent;

            _pBackground = manager.Load<Texture2D>("unknown");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
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
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);
            _MainScreenManager.Draw();
        }

        private UIManager _MainScreenManager;
        private SpriteFont _Font;
    }
}
