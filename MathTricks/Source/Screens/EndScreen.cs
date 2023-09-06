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

            _Text.Transform = new Rectangle(_Text.Transform.X, _Text.Transform.Y - 50,
                                            _Text.Transform.Width, _Text.Transform.Height);
            base.OnLoad();
        }

        public override void LoadContent(ContentManager manager)
        {
            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            _Font = manager.Load<SpriteFont>("Salvar");
            _Text = new Text("Player 1 has won!", _Font, _WindowSize, _Manager)
            {
                Color = Color.White
            };

            _ButtonTransform = new Rectangle(
                                            _WindowSize.X / 2 - _ButtonWidth / 2,
                                            _WindowSize.Y / 2,
                                            _ButtonWidth,
                                            _ButtonHeight);

            Button MainMenu = new Button(
                                    _ButtonTransform,
                                    "Main Menu",
                                    _Font,
                                    _Manager,
                                    _ButtonTexture)
            {
                OnButtonPressedEvent =
                                () =>
                                    { ScreenManager.CurrentScreen = ScreenState.MainMenu; }
            };

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
        private Rectangle _ButtonTransform;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
    }
}
