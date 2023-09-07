using Microsoft.Xna.Framework;

namespace MathTricks 
{
    struct Transform2D
    {
        // i fucking hate c# so so so much
        public Transform2D(bool temp = false) 
        {
            _IsDirty = true;
            _Position = Vector2.Zero;
            _Size = Vector2.One;
            _Rotation = 0.0f;
        }

        public Vector2 Position 
        {
            get => _Position;
            set 
            {
                _Position = value;
                _IsDirty = true;
            }
        }

        public Vector2 Size 
        {
            get => _Size;
            set 
            {
                _Size = value;
                _IsDirty = true;
            }
        }

        public float Rotation 
        {
            get => _Rotation;
            set 
            {
                _Rotation = value;
                _IsDirty = true;
            }
        }

        public bool IsDirty => _IsDirty;
        public void ResetDirty() => _IsDirty = false;

        private bool _IsDirty;
        private Vector2 _Position;
        private Vector2 _Size;
        private float _Rotation;
    }
}