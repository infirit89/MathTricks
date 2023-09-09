using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MathTricks
{
    enum Anchor 
    {
        None = 0,
        TopRight
    }

    class UIComponent
    {
        public UIComponent(Vector2 position, Vector2 size) 
        {
            Transform = new Transform2D
            {
                Position = position,
                Size = size
            };
            _Children = new List<UIComponent>();
            _Anchor = Anchor.None;
        }

        public UIComponent(Transform2D transform, Anchor anchor = Anchor.None) 
        {
            Transform = transform;
            _Children = new List<UIComponent>();
            _Anchor = anchor;
        }

        public UIComponent(Vector2 offset, Vector2 size, Anchor anchor = Anchor.None) 
        {
            Transform = new Transform2D() 
            {
                Position = Vector2.Zero,
                Size = size
            };

            _Offset = offset;
            _Children = new List<UIComponent>();
            _Anchor = anchor;
        }

        public virtual void Draw() 
        {
            // draw children:
            foreach(var child in _Children)
                child.Draw();
        }

        public virtual void Update() 
        {
            foreach(var child in _Children)
                child.Update();
        }

        // public List<UIComponent> Children => _Children;

        public void AddChild(UIComponent child) 
        {
            _Children.Add(child);
            child._Parent = this;
        }

        public void RemoveChild(UIComponent child) 
        {
            child._Parent = null;
            _Children.Remove(child);
        }

        public void ClearChildren() 
        {
            foreach(var child in _Children)
                child._Parent = null;

            _Children.Clear();
        }

        public T GetChild<T>(int index) where T : UIComponent 
                    => (T)_Children[index];

        public virtual Rectangle GetBoundingBox() 
        {
            Rectangle boundingBox;

            Rectangle parentRect = _Parent.GetBoundingBox();
            Point position = Transform.Position.ToPoint();
            Point size = Transform.Size.ToPoint();

            switch(_Anchor) 
            {
                case Anchor.TopRight: 
                {
                    position.X = (int)(parentRect.Width - (size.X + _Offset.X));
                    position.Y = (int)_Offset.Y;
                    break;
                }
            }

            boundingBox = new Rectangle(position, size);

            return boundingBox;
        }

        public Transform2D Transform;
        protected UIComponent _Parent = null;

        private List<UIComponent> _Children;
        private Anchor _Anchor;
        private Vector2 _Offset;
    }
}
