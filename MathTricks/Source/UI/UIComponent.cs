using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MathTricks
{
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
        }

        public UIComponent(Transform2D transform) 
        {
            Transform = transform;
            _Children = new List<UIComponent>();
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

        public List<UIComponent> Children => _Children;

        public void AddChild(UIComponent child) => _Children.Add(child);

        public virtual Rectangle BoundingBox
                                    => new Rectangle(
                                                Transform.Position.ToPoint(),
                                                Transform.Size.ToPoint());

        public Transform2D Transform;
        private List<UIComponent> _Children;
    }
}
