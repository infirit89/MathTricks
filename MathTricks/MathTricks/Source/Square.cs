using Microsoft.Xna.Framework;

namespace MathTricks
{
    class Square
    {
        public Square(Vector2 squarePos, int squareSize)
        {
            Transform = new Rectangle(squarePos.ToPoint(), new Point(squareSize, squareSize));
            CanPlaceQueen = true;
        }

        public Rectangle Transform { get; private set; }
        public bool CanPlaceQueen;
    }

}
