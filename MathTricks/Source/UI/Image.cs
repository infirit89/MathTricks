using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks 
{
    class Image : UIComponent
    {
        public Image(
                Vector2 position,
                Vector2 size,
                Texture2D texture = null)
            : base(position, size)
        {
            _Texture = texture;
        }

        public Image(
                Vector2 offset,
                Vector2 size,
                Anchor anchor = Anchor.None,
                Texture2D texture = null)
            : base(offset, size, anchor)
        {
            _Texture = texture;
        }

        public override void Draw() 
        {
            Renderer.AddQuad(GetBoundingBox(), Color, _Texture);
            base.Draw();
        }

        public Color Color = Color.White;
        private Texture2D _Texture;
    }
}