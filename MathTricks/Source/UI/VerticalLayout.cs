using Microsoft.Xna.Framework;

/*
    TODO:
    Center anchor
    Test
    Vertical layout where children are positioned based on an alignment
*/

namespace MathTricks
{
    class VerticalLayout : UIComponent
    {
        public VerticalLayout(
                            Vector2 offset,
                            Anchor anchor = Anchor.None)
            : base(offset, Vector2.Zero, anchor)
        {
        }

        public override void Draw() 
        {
            Renderer.AddQuad(GetBoundingBox(), Color.White);
        }

        public float Spacing = 0.0f;
        public Anchor ChildAlignment = Anchor.None;
    }
}
