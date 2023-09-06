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
            _ArialFont = manager.Load<SpriteFont>("Arial");
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
            Point buttonSize = new Point(_ButtonWidth, _ButtonHeight);

            Rectangle confirmButtonTransform = new Rectangle(new Point(center.X - buttonSize.X / 2,
                                                            _WindowSize.Y - 80), buttonSize);
            Texture2D _ButtonTexture = manager.Load<Texture2D>("niggaButton");
            Button confirmButton = new Button(confirmButtonTransform, "Confirm",
                                                _ArialFont, _SettingsManager, _ButtonTexture);

            Point modifierButtonSize = new Point(40, 40);

            Rectangle modifierUITransform = new Rectangle(
                                                    new Point(
                                                            (center.X - buttonSize.X / 2) -
                                                            _ModifierButtonOffset,
                                                    center.Y - buttonSize.Y / 2), 
                                                    modifierButtonSize);

            Text widthValueText = new Text(
                                        Globals.FieldWidth.ToString(),
                                        _ArialFont,
                                        _WindowSize, _SettingsManager);

            Text heightValueText = new Text(
                                        Globals.FieldHeight.ToString(),
                                        _ArialFont,
                                        _WindowSize,
                                        _SettingsManager);

            Text widthText = new Text("Width:", _ArialFont, _WindowSize, _SettingsManager);
            widthText.Color = Color.White;
            Text height = new Text("Height:", _ArialFont, _WindowSize, _SettingsManager);
            height.Color = Color.White;

            Button minusWidthButton = new Button(
                                                modifierUITransform,
                                                "-",
                                                _ArialFont,
                                                _SettingsManager,
                                                _ButtonTexture);
            minusWidthButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldWidth--;
                Globals.FieldWidth = Math.Min(Globals.FieldWidth, _MaxFieldSize.X);
                widthValueText.Value = Globals.FieldWidth.ToString();
            };

            widthText.Transform = new Rectangle(
                                            new Point(
                                                    widthText.Transform.X - 
                                                    minusWidthButton.Transform.Width * 3,
                                                    widthText.Transform.Y - _ModifierButtonOffset),
                                            widthText.Transform.Size);


            widthValueText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;

            Button minusHeightButton = new Button(
                                                modifierUITransform,
                                                "-",
                                                _ArialFont,
                                                _SettingsManager,
                                                _ButtonTexture);

            height.Transform = new Rectangle(
                                        new Point(
                                                height.Transform.X -
                                                minusHeightButton.Transform.Width * 3,
                                                height.Transform.Y +
                                                height.Transform.Height +
                                                _ModifierButtonOffset * 3),
                                        height.Transform.Size);

            minusHeightButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldHeight--;
                Globals.FieldHeight = Math.Min(Globals.FieldHeight, _MaxFieldSize.Y);
                heightValueText.Value = Globals.FieldHeight.ToString();
                heightValueText.Transform = new Rectangle(
                                                        heightValueText.Transform.X,
                                                        heightValueText.Transform.Y + 
                                                        heightValueText.Transform.Height + 
                                                        _ModifierButtonOffset * 3,
                                                        heightValueText.Transform.Width,
                                                        heightValueText.Transform.Height);

            };

            modifierUITransform.Y -= modifierButtonSize.Y + _ModifierButtonOffset;

            modifierUITransform.X += modifierButtonSize.X * 3 + _ModifierButtonOffset;
            Button plusWidthButton = new Button(
                                            modifierUITransform,
                                            "+",
                                            _ArialFont,
                                            _SettingsManager,
                                            _ButtonTexture);
            plusWidthButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldWidth++;
                Globals.FieldWidth = Math.Max(Globals.FieldWidth, _MinFieldSize.Y);

                widthValueText.Value = Globals.FieldWidth.ToString();
            };

            heightValueText.Transform = new Rectangle(
                                                    heightValueText.Transform.X,
                                                    heightValueText.Transform.Y +
                                                    heightValueText.Transform.Height +
                                                    _ModifierButtonOffset * 3,
                                                    heightValueText.Transform.Width,
                                                    heightValueText.Transform.Height);

            widthValueText.Color = Color.White;
            heightValueText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;

            Button plusHeightButton = new Button(
                                                modifierUITransform,
                                                "+",
                                                _ArialFont,
                                                _SettingsManager,
                                                _ButtonTexture);
            plusHeightButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldHeight++;
                Globals.FieldHeight = Math.Max(Globals.FieldHeight, _MinFieldSize.X);

                heightValueText.Value = Globals.FieldHeight.ToString();
                heightValueText.Transform = new Rectangle(
                                                        heightValueText.Transform.X,
                                                        heightValueText.Transform.Y +
                                                        heightValueText.Transform.Height +
                                                        _ModifierButtonOffset * 3,
                                                        heightValueText.Transform.Width,
                                                        heightValueText.Transform.Height);

            };

            confirmButton.OnButtonPressedEvent = () => 
            {
                ScreenManager.CurrentScreen = ScreenState.GameScreen;
            };
        }

        private Point _MinFieldSize = new Point(4, 4), _MaxFieldSize = new Point(15, 9);
        private UIManager _SettingsManager;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
        private const int _ModifierButtonOffset = 10;
        private SpriteFont _ArialFont;
        private Point _WindowSize;
    }
}
