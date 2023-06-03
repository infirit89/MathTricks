using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class Button : UIComponent
    {
        public delegate void OnButtonPressed();

        public Button(Rectangle buttonTransform, string text, SpriteFont font, UIManager manager, Texture2D buttonTexture = null)
            : base(buttonTransform, manager)
        {
            _Text = new Text(text, font, Transform, manager);            
            ButtonColor = Color.White;
            HoveredButtonColor = Color.Gray;
            _Hovered = false;
            _Texture = buttonTexture;
        }

        public override void Update() 
        {
            _Hovered = MTMouse.GetMouseRect().Intersects(Transform);

            if (_Hovered && MTMouse.IsButtonPressed(MouseButtons.Left) && OnButtonPressedEvent != null)
                OnButtonPressedEvent();

        }

        public override void Draw() 
        {
            GraphicsManager.AddQuad(Transform, _Hovered ? HoveredButtonColor : ButtonColor, _Texture);
        }

        public override Rectangle Transform 
        {
            get => _pTransform;

            set 
            {
                _pTransform = value;
                _Text.CenterTransform(_pTransform);
            } 
        }
        public OnButtonPressed OnButtonPressedEvent { private get; set; } = null;
        public Color ButtonColor, HoveredButtonColor;
        private Texture2D _Texture;
        private Text _Text;
        private bool _Hovered;
    }
}
