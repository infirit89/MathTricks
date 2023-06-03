using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class GameModeSelectionScreen : Screen
    {
        private Point _WindowSize;
        private Rectangle _SinglePlayerButtonRectangle;
        private Rectangle _MultiPlayerButtonRectangle;
        
        private void _SinglePlayerButtonEvent() => ApplicationManager.CurrentState = ApplicationState.SinglePlayer;
        private void _MultiPlayerButtonEvent() => ApplicationManager.CurrentState = ApplicationState.MultiPlayer;
        
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");
            
            const int buttonWidth = 150;
            const int buttonHeight = 50;

            _SinglePlayerButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 - buttonHeight * 2, buttonWidth, buttonHeight);
            _MultiPlayerButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 , buttonWidth, buttonHeight);
            
            Button _SinglePlayerButton = new Button(_SinglePlayerButtonRectangle, "SinglePlayer", _Font, _GameModeScreenManager);
            Button _MultiPlayerButton = new Button(_MultiPlayerButtonRectangle, "MultiPlayer", _Font, _GameModeScreenManager);

            _SinglePlayerButton.OnButtonPressedEvent = _SinglePlayerButtonEvent;
            _MultiPlayerButton.OnButtonPressedEvent = _MultiPlayerButtonEvent; 
        }

        public GameModeSelectionScreen(Point WindowSize)
        {
            _WindowSize = WindowSize; 
            _GameModeScreenManager = new UIManager();    
        }


        public override void Update() 
        {
            _GameModeScreenManager.Update();
        }

        public override void Draw() 
        {
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);
            _GameModeScreenManager.Draw();

        }

        private UIManager _GameModeScreenManager;
        private SpriteFont _Font;
    }
}
