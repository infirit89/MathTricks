using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class SettingsScreen : Screen
    {
        public SettingsScreen(Point windowSize) 
        {
            _WindowSize = windowSize;
            _SettingsManager = new UIManager();
        }

        public override void Draw()
        {
            Renderer.AddQuad(BackgroundTransform, Color.White, Background);
            _SettingsManager.Draw();
        }

        public override void LoadContent(ContentManager manager)
        {
            _Font = manager.Load<SpriteFont>("Salvar");
            SetupSettingUI(manager);
            Background = manager.Load<Texture2D>("bg");
            BackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public override void Update()
        {
            _SettingsManager.Update();
        }

        private void SetupSettingUI(ContentManager manager)
        {
            Point center = new Point(_WindowSize.X / 2, _WindowSize.Y / 2);
            Vector2 buttonSize = new Vector2(_ButtonWidth, _ButtonHeight);

            #region ConfirmButton
            Transform2D confirmButtonTransform = new Transform2D() 
            {
                Position = new Vector2(center.X - buttonSize.X / 2, _WindowSize.Y - 80),
                Size = buttonSize

            };

            Texture2D buttonTexture = manager.Load<Texture2D>("niggaButton");
            Button confirmButton = new Button(
                                            confirmButtonTransform,
                                            "Confirm",
                                            _Font,
                                            buttonTexture);
            confirmButton.Text.Color = Color.WhiteSmoke;
            confirmButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.GameScreen;
            };

            _SettingsManager.AddComponent(confirmButton);
            #endregion

            #region  ModifierUI
            Vector2 modifierButtonSize = new Vector2(40, 40);

            Transform2D modifierUITransform = new Transform2D() 
            {
                Position = new Vector2(
                                    (center.X - buttonSize.X / 2) -
                                        _ModifierButtonOffset,
                                    center.Y - buttonSize.Y / 2),
                Size = modifierButtonSize
            };

            #region SizeValueTexts
            Text widthValueText = new Text(
                                        Globals.FieldWidth.ToString(),
                                        _Font,
                                        _WindowSize);
            widthValueText.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(widthValueText);            

            Text heightValueText = new Text(
                                        Globals.FieldHeight.ToString(),
                                        _Font,
                                        _WindowSize);
            heightValueText.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(heightValueText);
            #endregion

            #region SizeDescriptionTexts
            Text widthText = new Text(
                                    "Width:",
                                    _Font,
                                    _WindowSize);
            widthText.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(widthText);

            Text heightText = new Text(
                                "Height:",
                                _Font,
                                _WindowSize);
            heightText.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(heightText);
            #endregion

            #region MinusWidthButton
            Button minusWidthButton = new Button(
                                                modifierUITransform,
                                                "-",
                                                _Font,
                                                buttonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    Globals.FieldWidth--;
                    Globals.FieldWidth = Math.Min(Globals.FieldWidth, _MaxFieldSize.X);
                    widthValueText.Value = Globals.FieldWidth.ToString();
                }
            };

            minusWidthButton.Text.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(minusWidthButton);
            #endregion

            widthText.Transform.Position = new Vector2(
                                                    widthText.Transform.Position.X - 
                                                        minusWidthButton.Transform.Size.X * 3,
                                                    widthText.Transform.Position.Y 
                                                        - _ModifierButtonOffset);


            widthValueText.Color = Color.White;

            modifierUITransform.Position += 
                                        new Vector2(0, modifierButtonSize.Y + _ModifierButtonOffset);

            #region MinusHeightButton
            Button minusHeightButton = new Button(
                                                modifierUITransform,
                                                "-",
                                                _Font,
                                                buttonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    Globals.FieldHeight--;
                    Globals.FieldHeight = Math.Min(Globals.FieldHeight, _MaxFieldSize.Y);
                    heightValueText.Value = Globals.FieldHeight.ToString();
                    heightValueText.Transform.Position = new Vector2(
                                                            heightValueText.Transform.Position.X,
                                                            heightValueText.Transform.Position.Y +
                                                                heightValueText.Transform.Size.X +
                                                                _ModifierButtonOffset * 3);

                }
            };
            minusHeightButton.Text.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(minusHeightButton);
            #endregion


            heightText.Transform.Position = new Vector2(
                                                heightText.Transform.Position.X -
                                                    minusHeightButton.Transform.Size.X * 3,
                                                heightText.Transform.Position.Y +
                                                    heightText.Transform.Size.Y +
                                                    _ModifierButtonOffset * 3);


            modifierUITransform.Position -=
                                    new Vector2(0, modifierButtonSize.Y + _ModifierButtonOffset);

            modifierUITransform.Position +=
                                    new Vector2(modifierButtonSize.X * 3 + _ModifierButtonOffset, 0);

            #region PlusWidthButton
            Button plusWidthButton = new Button(
                                            modifierUITransform,
                                            "+",
                                            _Font,
                                            buttonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    Globals.FieldWidth++;
                    Globals.FieldWidth = Math.Max(Globals.FieldWidth, _MinFieldSize.Y);

                    widthValueText.Value = Globals.FieldWidth.ToString();
                }
            };
            plusWidthButton.Text.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(plusWidthButton);
            #endregion

            heightValueText.Transform.Position = new Vector2(
                                                    heightValueText.Transform.Position.X,
                                                    heightValueText.Transform.Position.Y +
                                                        heightValueText.Transform.Size.Y +
                                                        _ModifierButtonOffset * 3);

            modifierUITransform.Position +=
                                    new Vector2(0, modifierButtonSize.Y + _ModifierButtonOffset);

            #region PlusHeightButton
            Button plusHeightButton = new Button(
                                                modifierUITransform,
                                                "+",
                                                _Font,
                                                buttonTexture)
            {
                OnButtonPressedEvent = () =>
                {
                    Globals.FieldHeight++;
                    Globals.FieldHeight = Math.Max(Globals.FieldHeight, _MinFieldSize.X);

                    heightValueText.Value = Globals.FieldHeight.ToString();
                    heightValueText.Transform.Position = new Vector2(
                                                            heightValueText.Transform.Position.X,
                                                            heightValueText.Transform.Position.Y +
                                                                heightValueText.Transform.Size.Y +
                                                                _ModifierButtonOffset * 3);

                }
            };
            plusHeightButton.Text.Color = Color.WhiteSmoke;

            _SettingsManager.AddComponent(plusHeightButton);
            #endregion
            
            #endregion
        }

        private Point _MinFieldSize = new Point(4, 4), _MaxFieldSize = new Point(15, 9);
        private UIManager _SettingsManager;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
        private const int _ModifierButtonOffset = 10;
        private SpriteFont _Font;
        private Point _WindowSize;
    }
}
