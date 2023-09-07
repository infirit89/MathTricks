using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    // centered text
    class Text : UIComponent
    {
        public Text(
                string text,
                SpriteFont font,
                Rectangle bounds,
                bool centerTransform = true)
            : base(new Transform2D())
        {
            _Text = text;
            _Font = font;
            _Bounds = bounds;
            _IsCenterTransform = centerTransform;

            if(centerTransform)
                CenterTransform(_Bounds);
            Color = Color.Black;
        }

        public Text(string text, SpriteFont font, Point bounds)
            : this(text, font, new Rectangle(new Point(0, 0), bounds)) { }

        public override void Update()
        { }

        public override void Draw()
        {
            Renderer.AddText(Transform.Position, _Text, _Font, Color);
        }
        public void CenterTransform(Rectangle bounds) 
        {
            Vector2 textSize = _Font.MeasureString(_Text);
            Vector2 textPos = new Vector2(((bounds.Width / 2) - (textSize.X / 2)) + bounds.X, 
                                          ((bounds.Height / 2) - (textSize.Y / 2)) + bounds.Y);
            Transform.Position = textPos;
            Transform.Size = textSize;
        }

        public void CenterTransform(Point size) 
                                => CenterTransform(new Rectangle(new Point(0, 0), size));

        public Color Color { get; set; }
        
        private string _Text;
        private Rectangle _Bounds;
        private SpriteFont _Font;
        private bool _IsCenterTransform;
        public string Value 
        {
            get => _Text;
            set 
            {
                _Text = value;
                if(_IsCenterTransform)
                    CenterTransform(_Bounds);
            }
        }
    }
}
