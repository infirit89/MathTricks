using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    static class Renderer
    {
        public static void Init()
        {
            _SpriteBatch = new SpriteBatch(Application.Instance.GraphicsDevice);
            _WhiteTexture = new Texture2D(
                                        Application.Instance.GraphicsDevice,
                                        1, 1);

            uint[] whiteTexData = new uint[] { 0xffffffff };
            _WhiteTexture.SetData(whiteTexData);
        }

        public static void Shutdown() 
        {
            _SpriteBatch.Dispose();
        }

        public static void Begin() 
                => _SpriteBatch.Begin(
                                    SpriteSortMode.Deferred,
                                    null,
                                    SamplerState.PointClamp);
        public static void End() => _SpriteBatch.End();

        public static void AddQuad(
                                Rectangle transform,
                                Color color,
                                Texture2D texture = null)
            => _SpriteBatch.Draw(
                            texture == null ? _WhiteTexture : texture,
                            transform,
                            color);

        public static void AddQuad(
                                Transform2D transform,
                                Color color,
                                Texture2D texture = null) 
        {
            _SpriteBatch.Draw(
                            texture ??_WhiteTexture,
                            transform.Position,
                            null,
                            color, Rotation, default, transform.Size, SpriteEffects.None, 0.0f);
        }

        public static void AddText(
                                Vector2 position,
                                string text,
                                SpriteFont font,
                                Color color)
            => _SpriteBatch.DrawString(font, text, position, color);

        private static SpriteBatch _SpriteBatch;
        static float Rotation;
        private static Texture2D _WhiteTexture;
    }
}
