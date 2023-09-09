using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class GameModeSelectionScreen : Screen
    {
        private Point _WindowSize;
        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Salvar");
            
            const int buttonWidth = 200;
            const int buttonHeight = 70;

            Transform2D singlePlayerButtonTransform = new Transform2D
            {
                Position = new Vector2(
                                    _WindowSize.X / 2 - buttonWidth / 2,
                                    _WindowSize.Y / 2 - buttonHeight * 2),

                Size = new Vector2(buttonWidth, buttonHeight)
            };

            Transform2D multiPlayerButtonTransform = new Transform2D
            {
                Position = new Vector2(
                                        _WindowSize.X / 2 - buttonWidth / 2,
                                        _WindowSize.Y / 2),

                Size = new Vector2(buttonWidth, buttonHeight)
            };

            Texture2D buttonTexture = manager.Load<Texture2D>("niggaButton");

            Vector2 buttonOffset = new Vector2(10.0f, 10.0f);
            Vector2 buttonSize = new Vector2(70.0f, 40.0f);
            Button returnToMainMenuButton = new Button(
                                                    buttonOffset,
                                                    buttonSize,
                                                    "Back",
                                                    _Font,
                                                    Anchor.TopLeft,
                                                    buttonTexture) 
            {
                OnButtonPressedEvent = () => 
                {
                    ScreenManager.CurrentScreen = ScreenState.MainMenu;
                }    
            };
            returnToMainMenuButton.Text.Color = Color.WhiteSmoke;
            
            _GameModeScreenManager.AddComponent(returnToMainMenuButton);

            Button singlePlayerButton = new Button(
                                                singlePlayerButtonTransform,
                                                "SinglePlayer",
                                                _Font,
                                                buttonTexture);

            singlePlayerButton.Text.Color = Color.WhiteSmoke;

            _GameModeScreenManager.AddComponent(singlePlayerButton);

            Button multiPlayerButton = new Button(
                                                multiPlayerButtonTransform,
                                                "MultiPlayer",
                                                _Font,
                                                buttonTexture);

            multiPlayerButton.Text.Color = Color.WhiteSmoke;

            _GameModeScreenManager.AddComponent(multiPlayerButton);

            singlePlayerButton.OnButtonPressedEvent = () => 
            {
                Globals.IsSinglePlayer = true;
                ScreenManager.CurrentScreen = ScreenState.Settings;
            };

            multiPlayerButton.OnButtonPressedEvent = () =>
            {
                Globals.IsSinglePlayer = false;
                ScreenManager.CurrentScreen = ScreenState.Settings;
            };
            
            Background = manager.Load<Texture2D>("bg");
            BackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
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
            Renderer.AddQuad(BackgroundTransform, Color.White, Background);
            _GameModeScreenManager.Draw();

        }

        private UIManager _GameModeScreenManager;
        private SpriteFont _Font;
    }
}
