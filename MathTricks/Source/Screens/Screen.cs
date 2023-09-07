using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathTricks
{
    abstract class Screen
    {
        public abstract void LoadContent(ContentManager manager);
        public abstract void Draw();
        public abstract void Update();
        public virtual void OnLoad() { }
        public virtual void OnUnload() { }

        public virtual void OnResize(Viewport viewport) { }

        protected Texture2D Background;
        protected Rectangle BackgroundTransform;
    }

    static class ScreenManager 
    {
        public static void Init() 
        {
            s_Screens = new Dictionary<string, Screen>();
        }

        public static void AddScreen(string name, Screen screen) 
        {
            s_Screens.Add(name, screen);
        }

        public static void UpdateCurrent() 
        {
            s_Screens[s_CurrentScreen].Update();
        }

        public static void DrawCurrent() 
        {
            s_Screens[s_CurrentScreen].Draw();
        }

        public static void LoadContent(ContentManager contentManager) 
        {
            foreach (var item in s_Screens)
                item.Value.LoadContent(contentManager);
        }

        public static void OnResize(Viewport viewport)
        {
            foreach (var item in s_Screens)
                item.Value.OnResize(viewport);
        }

        public static string CurrentScreen 
        {
            get => s_CurrentScreen;
            set 
            {
                if(s_CurrentScreen != null)
                    s_Screens[s_CurrentScreen].OnUnload();
                    
                s_CurrentScreen = value;
                s_Screens[s_CurrentScreen].OnLoad();
            }
        }

        public static T GetScreen<T>(string name) where T : Screen 
                    => (T)s_Screens[name];

        private static string s_CurrentScreen;
        private static Dictionary<string, Screen> s_Screens;
    }
}
