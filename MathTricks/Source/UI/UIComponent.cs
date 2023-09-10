﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MathTricks
{
    enum Anchor 
    {
        None = 0,
        TopRight,
        TopLeft,
        Center,
        CenterTop,
        CenterBottom
    }

    enum Sizing 
    {
        None = 0,
        ParentWidth,
        ParentHeight,
        ParentSize
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

        public UIComponent(
                        Vector2 offset,
                        Vector2 size,
                        Anchor anchor = Anchor.None,
                        Sizing sizing = Sizing.None) 
        {
            Transform = new Transform2D() 
            {
                Position = Vector2.Zero,
                Size = size
            };

            _Offset = offset;
            _Children = new List<UIComponent>();
            _Anchor = anchor;
            _Sizing = sizing;
        }

        public virtual void Draw() 
        {
            DrawChildren();
        }

        protected virtual void DrawChildren() 
        {
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

            switch(_Sizing) 
            {
                case Sizing.ParentWidth:
                {
                    size.X = parentRect.Width;
                    break;
                }
                case Sizing.ParentHeight:
                {
                    size.Y = parentRect.Height;
                    break;
                }
                case Sizing.ParentSize:
                {
                    size.X = parentRect.Width;
                    size.Y = parentRect.Height;
                    break;
                }
            }

            switch(_Anchor) 
            {
                case Anchor.TopRight: 
                {
                    position.X = (int)(parentRect.Width - (size.X + _Offset.X));
                    position.Y = (int)_Offset.Y;
                    break;
                }
                case Anchor.TopLeft: 
                {
                    position.X = (int)_Offset.X;
                    position.Y = (int)_Offset.Y;
                    break;
                }
                case Anchor.Center: 
                {
                    position.X = (int)(parentRect.Width / 2 - ((size.X / 2) + _Offset.X));
                    position.Y = (int)(parentRect.Height / 2 - ((size.Y / 2) + _Offset.Y));
                    break;
                }
                case Anchor.CenterTop: 
                {
                    position.X = (int)(parentRect.Width / 2 - ((size.X / 2) + _Offset.X));
                    position.Y = (int)_Offset.Y;
                    break;
                }
                case Anchor.CenterBottom: 
                {
                    position.X = (int)(parentRect.Width / 2 - ((size.X / 2) + _Offset.X));
                    position.Y = (int)(parentRect.Height - (size.Y + _Offset.Y));
                    break;
                }
            }

            boundingBox = new Rectangle(position, size);

            return boundingBox;
        }

        public Transform2D Transform;

        protected UIComponent _Parent = null;

        protected List<UIComponent> _Children;

        private Anchor _Anchor;
        private Sizing _Sizing;
        private Vector2 _Offset;
    }
}