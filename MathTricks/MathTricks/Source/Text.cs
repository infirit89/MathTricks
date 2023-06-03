using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    // centered text
    class Text : UIComponent
    {
        public Text(string text, SpriteFont font, Rectangle bounds, UIManager manager)
            : base(new Rectangle(0, 0, 0, 0), manager)
        {
            _Text = text;
            _Font = font;
            _Bounds = bounds;
            CenterTransform(_Bounds);
            Color = Color.Black;
        }

        public Text(string text, SpriteFont font, Point bounds, UIManager manager)
            : this(text, font, new Rectangle(new Point(0, 0), bounds), manager) { }

        public override void Update()
        { }

        public override void Draw()
        {
            GraphicsManager.AddText(Transform.Location.ToVector2(), _Text, _Font, Color);
        }
        public void CenterTransform(Rectangle bounds) 
        {
            Vector2 textSize = _Font.MeasureString(_Text);
            Vector2 textPos = new Vector2(((bounds.Width / 2) - (textSize.X / 2)) + bounds.X, 
                                          ((bounds.Height / 2) - (textSize.Y / 2)) + bounds.Y);
            Transform = new Rectangle(textPos.ToPoint(), textSize.ToPoint());
        }

        public void CenterTransform(Point size) => CenterTransform(new Rectangle(new Point(0, 0), size));

        public Color Color { get; set; }
        
        private string _Text;
        private Rectangle _Bounds;
        private SpriteFont _Font;
        public string TextStr 
        {
            get => _Text;
            set 
            {
                _Text = value;
                CenterTransform(_Bounds);
            }
        }
    }
}
