using System.Collections.Generic;

namespace MathTricks
{
    class UIManager
    {
        public UIManager() 
        {
            _UIComponents = new List<UIComponent>();
        }

        public void AddComponent(UIComponent component) => _UIComponents.Add(component);
        public void ClearComponents() => _UIComponents.Clear();

        public void Update() 
        {
            foreach (var component in _UIComponents)
                component.Update();
        }

        public void Draw() 
        {
            foreach (var component in _UIComponents)
                component.Draw();
        }

        private List<UIComponent> _UIComponents;
    }
}
