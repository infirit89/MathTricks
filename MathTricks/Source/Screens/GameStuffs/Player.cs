using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class Player
    {
        public Player(Point position, int playerSize, Color color, Texture2D texture = null)
        {
            _Transform = new Rectangle(position, new Point(playerSize, playerSize));
            _PlayerTexture = texture;
            _PlayerColor = color;
            Score = 0;
        }

        public void Draw()
        {
            Renderer.AddQuad(_Transform, _PlayerColor, _PlayerTexture, 0.2f);
        }

        public Color GetPlayerColor() => _PlayerColor;

        public Rectangle Transform { get => _Transform; set => _Transform = value; }
        public int Score;

        private Rectangle _Transform;
        private Texture2D _PlayerTexture;
        private Color _PlayerColor;
    }
}
