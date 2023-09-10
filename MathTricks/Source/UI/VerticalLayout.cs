using Microsoft.Xna.Framework;

namespace MathTricks
{
    class VerticalLayout : UIComponent
    {
        public VerticalLayout(
                            Vector2 offset,
                            Vector2 size,
                            Anchor anchor = Anchor.None,
                            Sizing sizing = Sizing.None)
            : base(offset, size, anchor, sizing)
        { }

        public override Rectangle GetBoundingBox() 
        {
            Rectangle rect = base.GetBoundingBox();

            float line = 0.0f;
            for(int i = 0; i < _Children.Count; i++) 
            {
                UIComponent child = _Children[i];
                Vector2 position = child.Transform.Position;
                Vector2 size = child.Transform.Size;
                position.X = rect.X - (size.X / 2);
                position.Y = rect.Height / 2 - 
                                ((size.Y / 2) * 
                                (_Children.Count - (i * 2))) +
                                line;

                line += Spacing;
                child.Transform.Position = position;
            }

            return rect;
        }

        public float Spacing = 0.0f;
        public Anchor ChildAlignment = Anchor.None;
    }
}
