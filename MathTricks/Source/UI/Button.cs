using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class Button : UIComponent
    {
        public delegate void OnButtonPressed();

        public Button(
                    Transform2D transform,
                    string text,
                    SpriteFont font,
                    Texture2D buttonTexture = null)
            : base(transform)
        {
            // _Text = new Text(text, font, Transform);  
            ButtonColor = Color.White;
            HoveredButtonColor = Color.Gray;
            _Hovered = false;
            _Texture = buttonTexture;
        }

        public override void Update() 
        {
            _Hovered = Input.GetMouseRect().Intersects(BoundingBox);

            if (_Hovered && Input.IsButtonPressed(MouseButtons.Left) && OnButtonPressedEvent != null)
                OnButtonPressedEvent();

        }

        public override void Draw() 
        {
            Renderer.AddQuad(Transform, _Hovered ? HoveredButtonColor : ButtonColor, _Texture);
        }

        public OnButtonPressed OnButtonPressedEvent { private get; set; } = null;
        public Color ButtonColor, HoveredButtonColor;
        private Texture2D _Texture;
        // private Text _Text;
        private bool _Hovered;
    }
}
