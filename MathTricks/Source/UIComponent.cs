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
            _Transform = transform;
            manager.AddComponent(this);
        }

        public abstract void Draw();
        public abstract void Update();

        public virtual Rectangle Transform { get => _Transform; set => _Transform = value; }
        protected Rectangle _Transform;
    }
}
