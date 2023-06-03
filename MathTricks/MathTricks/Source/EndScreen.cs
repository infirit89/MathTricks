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
        public override void LoadContent(ContentManager manager)
        {
            _Font = manager.Load<SpriteFont>("Salvar");
            _Text = new Text("Player 1 has won!", _Font, _WindowSize, _Manager);
            _Text.Color = Color.White;
            
            _ButtonTransform = new Rectangle(_WindowSize.X / 2 - _ButtonWidth / 2, _WindowSize.Y / 2, 
                                            _ButtonWidth, _ButtonHeight);
            _Button = new Button(_ButtonTransform, "Main Menu", _Font, _Manager);
            _Button.OnButtonPressedEvent = PlayButtonActionEvent;

            _pBackground = manager.Load<Texture2D>("bg");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }
        public override void Draw()
        {
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);
            _Manager.Draw();
        }
        public void PlayButtonActionEvent()
        {
            ApplicationManager.CurrentState = ApplicationState.MainMenu;
        }
        public override void Update() 
        {
            _Manager.Update();
        }

        public void Lost(string winningPlayer)
        {
            _Text.Value = $"{winningPlayer} has won!";

            _Text.Transform = new Rectangle(_Text.Transform.X, _Text.Transform.Y - 50,
                                            _Text.Transform.Width, _Text.Transform.Height);
        }

        private Button _Button;
        private Text _Text;
        private SpriteFont _Font;
        private UIManager _Manager;
        private Point _WindowSize;
        private Rectangle _ButtonTransform;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
    }
}
