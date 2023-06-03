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
        }

        public void Draw()
        {
            GraphicsManager.AddQuad(_Transform, _PlayerColor, _PlayerTexture);
        }

        public Color GetPlayerColor() => _PlayerColor;

        public Rectangle Transform { get => _Transform; set => _Transform = value; }
        
        private Rectangle _Transform;
        private Texture2D _PlayerTexture;
        private Color _PlayerColor;
    }
}
