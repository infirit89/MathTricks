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

        private void SinglePlayerButtonEvent()
        {
            Globals.IsSinglePlayer = true;
            ApplicationManager.CurrentState = ApplicationState.Settings;
        }
        private void MultiPlayerButtonEvent()
        {
            Globals.IsSinglePlayer = false;
            ApplicationManager.CurrentState = ApplicationState.Settings;
        }
        
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Arial");
            
            const int buttonWidth = 200;
            const int buttonHeight = 70;

            _SinglePlayerButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 - buttonHeight * 2, buttonWidth, buttonHeight);
            _MultiPlayerButtonRectangle = new Rectangle(_WindowSize.X / 2 - buttonWidth/2, _WindowSize.Y / 2 , buttonWidth, buttonHeight);
            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            Button _SinglePlayerButton = new Button(_SinglePlayerButtonRectangle, "SinglePlayer", _Font, _GameModeScreenManager, _ButtonTexture);
            Button _MultiPlayerButton = new Button(_MultiPlayerButtonRectangle, "MultiPlayer", _Font, _GameModeScreenManager, _ButtonTexture);

            _SinglePlayerButton.OnButtonPressedEvent = SinglePlayerButtonEvent;
            _MultiPlayerButton.OnButtonPressedEvent = MultiPlayerButtonEvent; 
            
            _pBackground = manager.Load<Texture2D>("bg");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
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
