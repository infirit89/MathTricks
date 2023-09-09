using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MathTricks
{
    class EndScreen : Screen
    {
        public EndScreen(Point windowSize)
        {
            _WindowSize = windowSize;
            _Manager = new UIManager();
        }

        public override void OnLoad()
        {
            _Text.Value = $"{Globals.WinningPlayerName} has won!";

            _Text.Transform.Position = new Vector2(
                                                _Text.Transform.Position.X,
                                                _Text.Transform.Position.Y - 50);

            base.OnLoad();
        }

        public override void LoadContent(ContentManager manager)
        {
            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            _Font = manager.Load<SpriteFont>("Salvar");
            _Text = new Text("Player 1 has won!", _Font, _WindowSize)
            {
                Color = Color.WhiteSmoke
            };

            _Manager.AddComponent(_Text);

            Transform2D buttonTransform = new Transform2D
            {
                Position = new Vector2(
                                    _WindowSize.X / 2 - _ButtonWidth / 2,
                                    _WindowSize.Y / 2),

                Size = new Vector2(_ButtonWidth, _ButtonHeight)
            };

            Button mainMenuButton = new Button(
                                    buttonTransform,
                                    "Main Menu",
                                    _Font,
                                    _ButtonTexture)
            {
                OnButtonPressedEvent =
                                () =>
                                    { ScreenManager.CurrentScreen = ScreenState.MainMenu; }
            };
            mainMenuButton.Text.Color = Color.WhiteSmoke;

            _Manager.AddComponent(mainMenuButton);

            Background = manager.Load<Texture2D>("bg");
            BackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }
        public override void Draw()
        {
            Renderer.AddQuad(BackgroundTransform, Color.White, Background);
            _Manager.Draw();
        }
        
        public override void Update() 
        {
            _Manager.Update();
        }

        private Text _Text;
        private SpriteFont _Font;
        private UIManager _Manager;
        private Point _WindowSize;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
    }
}
