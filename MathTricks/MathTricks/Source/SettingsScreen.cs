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
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);
            _SettingsManager.Draw();
        }

        public override void LoadContent(ContentManager manager)
        {
            _ArialFont = manager.Load<SpriteFont>("Arial");
            SetupSettingUI(manager);
            _pBackground = manager.Load<Texture2D>("bg");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
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

            Rectangle modifierUITransform = new Rectangle(new Point((center.X - buttonSize.X / 2) - _ModifierButtonOffset,
                                                     center.Y - buttonSize.Y / 2), modifierButtonSize);
            Text columnNumText = new Text(Globals.FieldHeight.ToString(), _ArialFont, _WindowSize, _SettingsManager);
            Text rowNumText = new Text(Globals.FieldWidth.ToString(), _ArialFont, _WindowSize, _SettingsManager);

            Text columnText = new Text("Column:", _ArialFont, _WindowSize, _SettingsManager);
            columnText.Color = Color.White;
            Text rowText = new Text("Row:", _ArialFont, _WindowSize, _SettingsManager);
            rowText.Color = Color.White;


            Button minusColumnButton = new Button(modifierUITransform, "-", _ArialFont, _SettingsManager, _ButtonTexture);
            minusColumnButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldHeight--;
                Globals.FieldHeight = Math.Min(Globals.FieldHeight, _MaxFieldSize.Y);
                columnNumText.Value = Globals.FieldHeight.ToString();
            };

            columnText.Transform = new Rectangle(new Point(columnText.Transform.X - minusColumnButton.Transform.Width * 3,
                                                columnText.Transform.Y - _ModifierButtonOffset),
                                                 columnText.Transform.Size);


            columnNumText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;

            Button minusRowButton = new Button(modifierUITransform, "-", _ArialFont, _SettingsManager, _ButtonTexture);

            rowText.Transform = new Rectangle(new Point(rowText.Transform.X - minusRowButton.Transform.Width * 3,
                                                rowText.Transform.Y + rowText.Transform.Height + _ModifierButtonOffset * 3),
                                                rowText.Transform.Size);

            minusRowButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldWidth--;
                Globals.FieldWidth = Math.Min(Globals.FieldWidth, _MaxFieldSize.X);
                rowNumText.Value = Globals.FieldWidth.ToString();
                rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                              rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                              rowNumText.Transform.Width, rowNumText.Transform.Height);

            };

            modifierUITransform.Y -= modifierButtonSize.Y + _ModifierButtonOffset;

            modifierUITransform.X += modifierButtonSize.X * 3 + _ModifierButtonOffset;
            Button plusColumnButton = new Button(modifierUITransform, "+", _ArialFont, _SettingsManager, _ButtonTexture);
            plusColumnButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldHeight++;
                Globals.FieldHeight = Math.Max(Globals.FieldHeight, _MinFieldSize.Y);

                columnNumText.Value = Globals.FieldHeight.ToString();
            };

            rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                                                          rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                                                          rowNumText.Transform.Width, rowNumText.Transform.Height);

            columnNumText.Color = Color.White;
            rowNumText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;
            Button plusRowButton = new Button(modifierUITransform, "+", _ArialFont, _SettingsManager, _ButtonTexture);
            plusRowButton.OnButtonPressedEvent = () =>
            {
                Globals.FieldWidth++;
                Globals.FieldWidth = Math.Max(Globals.FieldWidth, _MinFieldSize.X);

                rowNumText.Value = Globals.FieldWidth.ToString();
                rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                                              rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                                              rowNumText.Transform.Width, rowNumText.Transform.Height);

            };

            confirmButton.OnButtonPressedEvent = OnConfirmButtonPressed;
        }

        public Button.OnButtonPressed OnConfirmButtonPressed;
        private Point _MinFieldSize = new Point(4, 4), _MaxFieldSize = new Point(15, 9);
        private UIManager _SettingsManager;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
        private const int _ModifierButtonOffset = 10;
        private SpriteFont _ArialFont;
        private Point _WindowSize;
    }
}
