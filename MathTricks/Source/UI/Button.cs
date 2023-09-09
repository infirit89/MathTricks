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
            Text = new Text(text, font, Rectangle.Empty);
            AddChild(Text);
            _Texture = buttonTexture;
        }

        public Button(
                    Vector2 offset,
                    Vector2 size,
                    string text,
                    SpriteFont font,
                    Anchor anchor = Anchor.None,
                    Texture2D buttonTexture = null)
            : base(offset, size, anchor)
        {
            Text = new Text(text, font, Rectangle.Empty);
            AddChild(Text);
            _Texture = buttonTexture;
        }

        public override void Update() 
        {
            _Hovered = Input.GetMouseRect().Intersects(GetBoundingBox());

            if (_Hovered && Input.IsButtonPressed(MouseButtons.Left) && OnButtonPressedEvent != null)
                OnButtonPressedEvent();

            base.Update();
        }

        public override void Draw() 
        {
            Renderer.AddQuad(GetBoundingBox(), _Hovered ? HoveredButtonColor : ButtonColor, _Texture);

            base.Draw();
        }

        public OnButtonPressed OnButtonPressedEvent { private get; set; } = null;
        public Color ButtonColor = Color.White;
        public Color HoveredButtonColor = Color.Gray;
        public Text Text;
        private Texture2D _Texture;
        private bool _Hovered;
    }
}
