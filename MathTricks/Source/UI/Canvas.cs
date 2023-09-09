
using Microsoft.Xna.Framework;

namespace MathTricks 
{
    class Canvas : UIComponent
    {
        public Canvas() 
            : base(Vector2.Zero, Vector2.Zero)
        { }

        public override Rectangle GetBoundingBox() 
        {
            return new Rectangle(0, 0,
                                Application.Instance.WindowWidth,
                                Application.Instance.WindowHeight);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
