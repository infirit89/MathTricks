using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MathTricks
{
    abstract class UIComponent
    {
        public UIComponent(Rectangle transform, UIManager manager) 
        {
            _pTransform = transform;
            manager.AddComponent(this);
        }

        public abstract void Draw();
        public abstract void Update();

        public virtual Rectangle Transform { get => _pTransform; set => _pTransform = value; }
        protected Rectangle _pTransform;
    }
}
