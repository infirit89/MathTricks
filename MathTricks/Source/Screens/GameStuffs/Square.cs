using Microsoft.Xna.Framework;

namespace MathTricks
{
    class Square
    {
        public Square(Vector2 squarePos, int squareSize)
        {
            Image = new Image(squarePos, new Vector2(squareSize), null);
            PlayerIndex = -1;
        }

        public Rectangle Transform => Image.GetBoundingBox();

        public Image Image;
        public Text Text;
        public int PlayerIndex;
    }
}
