using System.Collections.Generic;

namespace MathTricks
{
    class UIManager
    {
        public UIManager() 
        {
            _Canvas = new Canvas();
        }

        public void AddComponent(UIComponent component) => _Canvas.AddChild(component);
        public void ClearComponents() => _Canvas.Children.Clear();

        public void Update()
        {
            _Canvas.Update();
        }


        public void Draw() 
        {
            _Canvas.Draw();
        }

        private Canvas _Canvas;
        public static float GlobalScale = 1.0f;
    }
}
