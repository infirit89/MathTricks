using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    class Button : UIComponent
    {
        public delegate void OnButtonPressed();

        public Button(Rectangle buttonTransform, string text, SpriteFont font, UIManager manager)
            : base(buttonTransform, manager)
        {
            _Text = new Text(text, font, Transform, manager);            
            ButtonColor = Color.White;
            HoveredButtonColor = Color.Gray;
            _Hovered = false;
        }

        public override void Update() 
        {
            _Hovered = OSMouse.GetMouseRect().Intersects(Transform);

            if (_Hovered && OSMouse.IsButtonPressed(MouseButtons.Left) && OnButtonPressedEvent != null)
                OnButtonPressedEvent();

        }

        public override void Draw() 
        {
            GraphicsManager.AddQuad(Transform, _Hovered ? HoveredButtonColor : ButtonColor);
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
        private Text _Text;
        private bool _Hovered;
    }
}
