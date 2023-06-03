using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    static class GraphicsManager
    {
        public static void Init(Game game) 
        {
            _Graphics = new GraphicsDeviceManager(game);
        }

        public static void CreateRenderer() 
        {
            _SpriteBatch = new SpriteBatch(_Graphics.GraphicsDevice);
            _WhiteTexture = new Texture2D(_Graphics.GraphicsDevice, 1, 1);
            uint[] whiteTexData = new uint[] { 0xffffffff };
            _WhiteTexture.SetData(whiteTexData);
        }

        public static void Begin() => _SpriteBatch.Begin();
        public static void End() => _SpriteBatch.End();

        public static void AddQuad(Rectangle transform, Color color, Texture2D texture = null)
            => _SpriteBatch.Draw(texture == null ? _WhiteTexture : texture, transform, color);
        public static void AddText(Vector2 position, string text, SpriteFont font, Color color)
            => _SpriteBatch.DrawString(font, text, position, color);

        private static GraphicsDeviceManager _Graphics;
        private static SpriteBatch _SpriteBatch;
        private static Texture2D _WhiteTexture;
    }
}
