using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    class Piece
    {
        public Piece(Vector2 position, int playerSize, Texture2D texture)
        {
            _Transform = new Rectangle(position.ToPoint(), new Point(playerSize, playerSize));
            _PlayerTexture = texture;
        }

        public void Draw()
        {
            GraphicsManager.AddQuad(_Transform, Color.White, _PlayerTexture);
        }

        public Rectangle Transform { get => _Transform; set => _Transform = value; }
        
        private Rectangle _Transform;
        private Texture2D _PlayerTexture;
    }
}
