using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MathTricks
{
    enum MouseButtons 
    {
        Left = 0,
        Middle,
        Right
    }

    // NOTE: wrapper with helper functions for the mouse
    class MTMouse
    {
        public static void Update() 
        {
            _PrevMouseState = _CurrMouseState;
            _CurrMouseState = Mouse.GetState();
        }

        public static Point GetPosition() => _CurrMouseState.Position;

        public static Rectangle GetMouseRect() => new Rectangle(GetPosition(), new Point(_CursorSize, _CursorSize));

        public static bool IsButtonPressed(MouseButtons button) 
        {
            switch (button)
            {
                case MouseButtons.Left:     return _CurrMouseState.LeftButton == ButtonState.Pressed &&
                                                    _PrevMouseState.LeftButton == ButtonState.Released;
                case MouseButtons.Middle:   return _CurrMouseState.MiddleButton == ButtonState.Pressed &&
                                                    _PrevMouseState.MiddleButton == ButtonState.Released;
                case MouseButtons.Right:    return _CurrMouseState.RightButton == ButtonState.Pressed &&
                                                    _PrevMouseState.RightButton == ButtonState.Released;
            }

            return false;
        }

        public static bool IsButtonHeld(MouseButtons button) 
        {
            switch (button)
            {
                case MouseButtons.Left:     return _CurrMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Middle:   return _CurrMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.Right:    return _CurrMouseState.RightButton == ButtonState.Pressed;
            }

            return false;
        }

        public static bool IsButtonReleased(MouseButtons button) 
        {
            switch (button)
            {
                case MouseButtons.Left:     return _CurrMouseState.LeftButton == ButtonState.Released &&
                                            _PrevMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Middle:   return _CurrMouseState.MiddleButton == ButtonState.Released &&
                                            _PrevMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.Right:    return _CurrMouseState.RightButton == ButtonState.Released &&
                                            _PrevMouseState.RightButton == ButtonState.Pressed;
            }

            return false;
        }

        private static MouseState _CurrMouseState, _PrevMouseState;
        private const int _CursorSize = 2;
    }
}
