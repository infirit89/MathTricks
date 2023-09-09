using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class HelpScreen : Screen
    {
        
        public HelpScreen()
        {
            _HelpScreenManager = new UIManager();    
        }

        public override void LoadContent(ContentManager manager) 
        {
            _Font = manager.Load<SpriteFont>("Salvar");

            Texture2D buttonTexture = manager.Load<Texture2D>("niggaButton");

            Vector2 buttonOffset = new Vector2(10.0f, 10.0f);
            Vector2 buttonSize = new Vector2(70.0f, 40.0f);
            Button escapeFromHelpScreenButton = new Button(
                                                        buttonOffset,
                                                        buttonSize,
                                                        "Back",
                                                        _Font,
                                                        Anchor.TopRight,
                                                        buttonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    ScreenManager.CurrentScreen = ScreenState.MainMenu;
                }
            };

            escapeFromHelpScreenButton.Text.Color = Color.WhiteSmoke;

            _HelpScreenManager.AddComponent(escapeFromHelpScreenButton);

            string helpText;
            using (StreamReader streamReader = new StreamReader(@"Content/Help.txt"))
                helpText = streamReader.ReadToEnd();

            Text text = new Text(
                            helpText,
                            _Font,
                            Rectangle.Empty)
            {
                Color = Color.WhiteSmoke
            };

            _HelpScreenManager.AddComponent(text);
        }

        public override void Update() 
        {
            _HelpScreenManager.Update();
        }

        public override void Draw() 
        {
            _HelpScreenManager.Draw();
        }

        private UIManager _HelpScreenManager;
        private SpriteFont _Font;
    }
}
