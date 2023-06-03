using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    class GameScreen : Screen
    {
        struct SquareIndex 
        {
            public int X, Y;
        }

        struct ValueRange 
        {
            public int Lower, Upper;
        }

        public delegate void OnGameLost(string winningPlayer);

        public GameScreen(Point windowSize) 
        {
            // NOTE: max field size: 9x15
            _FieldWidth = 8;
            _FieldHeight = 8;
            
            _WindowSize = windowSize;
            _SettingsManager = new UIManager();
            _GameScreenUIManager = new UIManager();
            _Random = new Random();

            _PlayerColor = new Color[2];
            _PlayerColor[0] = Color.Cyan;
            _PlayerColor[1] = Color.Orange;
        }

        private void GenerateBoard() 
        {
            _Field = new Square[_FieldHeight, _FieldWidth];

            _FieldOffset = new Vector2(_WindowSize.X * 0.5f - (((_SquareSize + 2) * _Field.GetLength(1)) * 0.5f),
                                       _WindowSize.Y * 0.5f - (((_SquareSize + 2) * _Field.GetLength(0)) * 0.5f));

            List<string> actions = new List<string> { "", "-", "*", "/" };

            for (int y = 0; y < _FieldHeight; y++)
            {
                for (int x = 0; x < _FieldWidth; x++)
                {
                    Vector2 squarePos = new Vector2((x * (_SquareSize + 2)) + _FieldOffset.X,
                                                    (y * (_SquareSize + 2)) + _FieldOffset.Y);

                    string squareValue = "";

                    if (x == 0 && y == 0 || x == _FieldWidth - 1 && y == _FieldHeight - 1)
                    {
                        squareValue = "0";
                    }
                    else 
                    {
                        string action = actions[_Random.Next(actions.Count)];

                        ValueRange range = GetValueRange(action);
                        int number = _Random.Next(range.Lower, range.Upper);

                        squareValue = $"{action} {number}";
                    }
                    _Field[y, x] = new Square(squarePos, _SquareSize);
                    _Field[y, x].Text = new Text(squareValue, _ArialFont, _Field[y, x].Transform, _GameScreenUIManager);
                }
            }
        }

        private ValueRange GetValueRange(string action) 
        {
            ValueRange range = new ValueRange { Lower = 0, Upper = 9 };
            switch (action)
            {
                case "":
                case "-":
                    break;
                case "*":
                    range.Upper = 2;
                    break;
                case "/":
                    range.Lower = 1;
                    break;
            }

            return range;
        }

        public void BeginGame() 
        {
            GenerateBoard();

            _Player1 = new Player(GetCeneteredPlayerPosition(_PlayerSize, _Field[0, 0].Transform.Location, _SquareSize),
                                  _PlayerSize, Color.Blue);

            _Player2 = new Player(GetCeneteredPlayerPosition(_PlayerSize, _Field[_FieldHeight - 1, _FieldWidth - 1].Transform.Location, _SquareSize),
                                  _PlayerSize, Color.Yellow);

            _Field[0, 0].PlayerIndex = 0;
            _Field[_FieldHeight - 1, _FieldWidth - 1].PlayerIndex = 1;

            _PlayerTurnCounter = 0;

            _HasGameBegun = true;
        }

        private Point GetCeneteredPlayerPosition(int playerSize, Point squarePosition, int squareSize) 
        {
            return new Point((squareSize / 2) - (playerSize / 2) + squarePosition.X,
                               (squareSize / 2) - (playerSize / 2) + squarePosition.Y);
        }

        public override void LoadContent(ContentManager manager) 
        {
            _ArialFont = manager.Load<SpriteFont>("Arial");

            SetupSettingUI();

            _pBackground = manager.Load<Texture2D>("bg");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public override void Update() 
        {
            if (!_HasGameBegun)
                _SettingsManager.Update();
            else 
            {
                if (MTMouse.IsButtonPressed(MouseButtons.Left)) 
                {
                    Square square = _Field.Cast<Square>().ToArray().FirstOrDefault(x => x.Transform.Intersects(MTMouse.GetMouseRect()));                    
                    PlacePiece(square);
                }
            }
        }
        
        public override void Draw()
        {
            GraphicsManager.AddQuad(_pBackgroundTransform, Color.White, _pBackground);

            if (!_HasGameBegun)
                _SettingsManager.Draw();
            else 
            {
                for (int y = 0; y < _Field.GetLength(0); y++) 
                {
                    for (int x = 0; x < _Field.GetLength(1); x++) 
                    {
                        Color currentSquareColor = Color.White;


                        if (_Field[y, x].PlayerIndex != -1)
                            currentSquareColor = _PlayerColor[_Field[y, x].PlayerIndex];

                        if (y == 0 && x == 0)
                            currentSquareColor = _PlayerColor[0];
                        else if (y == _FieldHeight - 1 && x == _FieldWidth - 1)
                            currentSquareColor = _PlayerColor[1];


                        GraphicsManager.AddQuad(_Field[y, x].Transform, currentSquareColor);
                    } 
                }

                _Player1.Draw();
                _Player2.Draw();

                _GameScreenUIManager.Draw();
            } 
        }

        private Color GetCurrentPlayerColor()
        {
            if (_PlayerTurnCounter == 0)
                return Color.White;

            return _PlayerColor[_PlayerTurnCounter % 2 != 0 ? 0 : 1];
        }

        private void PlacePiece(Square square)
        {
            if (square != null)
            {
                SquareIndex ind = GetSquareIndexInMatrix(square);

                if (IsMoveValid(ind))
                {
                    Point newPlayerPosition = GetCeneteredPlayerPosition(_PlayerSize, square.Transform.Location, _SquareSize);
                    if (_PlayerTurnCounter % 2 == 0)
                        _Player1.Transform = new Rectangle(newPlayerPosition, _Player1.Transform.Size);
                    else
                        _Player2.Transform = new Rectangle(newPlayerPosition, _Player2.Transform.Size);
                    _Field[ind.Y, ind.X].PlayerIndex = _PlayerTurnCounter % 2;

                    if (!ValidMoveExists())
                    {
                        OnGameLostEvent(_PlayerTurnCounter % 2 == 0 ? "Player 2" : "Player 1");
                        _HasGameBegun = false;
                        ApplicationManager.CurrentState = ApplicationState.EndScreen;
                    }

                    _PlayerTurnCounter++;
                }
            }
        }

        private bool IsMoveValid(SquareIndex index)
        {
            Player currentPlayer = _PlayerTurnCounter % 2 == 0 ? _Player1 : _Player2;
            Square playerSquare = _Field.Cast<Square>().ToArray().FirstOrDefault(square => square.Transform.Intersects(currentPlayer.Transform));
            SquareIndex playerSquareIndex = GetSquareIndexInMatrix(playerSquare);

            int xDirection = Math.Abs(playerSquareIndex.X - index.X), yDirection = Math.Abs(playerSquareIndex.Y - index.Y);
            if (xDirection > 1 || yDirection > 1)
                return false;

            if (_Field[index.Y, index.X].PlayerIndex != -1)
                return false;

            return true;
        }

        private bool ValidMoveExists() 
        {
            Player currentPlayer = _PlayerTurnCounter % 2 == 0 ? _Player1 : _Player2;
            Square playerSquare = _Field.Cast<Square>().ToArray().FirstOrDefault(square => square.Transform.Intersects(currentPlayer.Transform));
            SquareIndex playerSquareIndex = GetSquareIndexInMatrix(playerSquare);

            bool validMoveExists = false;

            if (playerSquareIndex.X + 1 < _FieldWidth && _Field[playerSquareIndex.Y, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 && _Field[playerSquareIndex.Y, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.Y + 1 > _FieldHeight && _Field[playerSquareIndex.Y + 1, playerSquareIndex.X].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.Y - 1 >= 0 && _Field[playerSquareIndex.Y - 1, playerSquareIndex.X].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X + 1 < _FieldWidth && playerSquareIndex.Y + 1 < _FieldHeight &&
                    _Field[playerSquareIndex.Y + 1, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 && playerSquareIndex.Y + 1 < _FieldHeight &&
                    _Field[playerSquareIndex.Y + 1, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X + 1 < _FieldWidth && playerSquareIndex.Y - 1 >= 0 &&
                    _Field[playerSquareIndex.Y - 1, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 && playerSquareIndex.Y - 1 >= 0 &&
                    _Field[playerSquareIndex.Y - 1, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;

            return validMoveExists;
        }

        private SquareIndex GetSquareIndexInMatrix(Square square)
        {
            // convert from square location to the square index in the field matrix
            Vector2 squarePosition = square.Transform.Location.ToVector2();
            return new SquareIndex() { X = (int)((squarePosition.X - _FieldOffset.X) / (_SquareSize + 2)), Y = (int)((squarePosition.Y - _FieldOffset.Y) / (_SquareSize + 2)) };
        }

        private void SetupSettingUI() 
        {
            Point center = new Point(_WindowSize.X / 2, _WindowSize.Y / 2);
            Point buttonSize = new Point(_ButtonWidth, _ButtonHeight);

            Rectangle confirmButtonTransform = new Rectangle(new Point(center.X - buttonSize.X / 2,
                                                            _WindowSize.Y - 80), buttonSize);
            Button confirmButton = new Button(confirmButtonTransform, "Confirm",
                                                _ArialFont, _SettingsManager);

            Point modifierButtonSize = new Point(40, 40);

            Rectangle modifierUITransform = new Rectangle(new Point((center.X - buttonSize.X / 2) - _ModifierButtonOffset,
                                                     center.Y - buttonSize.Y / 2), modifierButtonSize);
            Text columnNumText = new Text(_FieldWidth.ToString(), _ArialFont, _WindowSize, _SettingsManager);
            Text rowNumText = new Text(_FieldWidth.ToString(), _ArialFont, _WindowSize, _SettingsManager);

            Text columnText = new Text("Column:", _ArialFont, _WindowSize, _SettingsManager);
            columnText.Color = Color.White;
            Text rowText = new Text("Row:", _ArialFont, _WindowSize, _SettingsManager);
            rowText.Color = Color.White;

            Button plusColumnButton = new Button(modifierUITransform, "+", _ArialFont, _SettingsManager);
            plusColumnButton.OnButtonPressedEvent = () =>
            {
                _FieldHeight++;
                _FieldHeight = Math.Min(_FieldHeight, _MaxFieldSize.Y);
                columnNumText.TextStr = _FieldHeight.ToString();
            };

            columnText.Transform = new Rectangle(new Point(columnText.Transform.X - plusColumnButton.Transform.Width * 3,
                                                columnText.Transform.Y - _ModifierButtonOffset),
                                                 columnText.Transform.Size);


            columnNumText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;

            Button plusRowButton = new Button(modifierUITransform, "+", _ArialFont, _SettingsManager);

            rowText.Transform = new Rectangle(new Point(rowText.Transform.X - plusRowButton.Transform.Width * 3,
                                                rowText.Transform.Y + rowText.Transform.Height + _ModifierButtonOffset * 3),
                                                rowText.Transform.Size);

            plusRowButton.OnButtonPressedEvent = () =>
            {
                _FieldWidth++;
                _FieldWidth = Math.Min(_FieldWidth, _MaxFieldSize.X);
                rowNumText.TextStr = _FieldWidth.ToString();
                rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                              rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                              rowNumText.Transform.Width, rowNumText.Transform.Height);

            };

            modifierUITransform.Y -= modifierButtonSize.Y + _ModifierButtonOffset;

            modifierUITransform.X += modifierButtonSize.X * 3 + _ModifierButtonOffset;
            Button minusColumnButton = new Button(modifierUITransform, "-", _ArialFont, _SettingsManager);
            minusColumnButton.OnButtonPressedEvent = () =>
            {
                _FieldHeight--;
                _FieldHeight = Math.Max(_FieldHeight, _MinFieldSize.Y);

                columnNumText.TextStr = _FieldHeight.ToString();
            };

            rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                                                          rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                                                          rowNumText.Transform.Width, rowNumText.Transform.Height);

            columnNumText.Color = Color.White;
            rowNumText.Color = Color.White;

            modifierUITransform.Y += modifierButtonSize.Y + _ModifierButtonOffset;
            Button minusRowButton = new Button(modifierUITransform, "-", _ArialFont, _SettingsManager);
            minusRowButton.OnButtonPressedEvent = () =>
            {
                _FieldWidth--;
                _FieldWidth = Math.Max(_FieldWidth, _MinFieldSize.X);

                rowNumText.TextStr = _FieldWidth.ToString();
                rowNumText.Transform = new Rectangle(rowNumText.Transform.X,
                                              rowNumText.Transform.Y + rowNumText.Transform.Height + _ModifierButtonOffset * 3,
                                              rowNumText.Transform.Width, rowNumText.Transform.Height);

            };

            confirmButton.OnButtonPressedEvent = BeginGame;

        }

        public OnGameLost OnGameLostEvent { private get; set; } = null;

        private Square[,] _Field;
        private const int _SquareSize = 50;
        private bool _HasGameBegun = false;
        private UIManager _SettingsManager;
        private SpriteFont _ArialFont;
        private Color[] _PlayerColor;
        private const int _PlayerSize = 20;
        private Player _Player1, _Player2;
        private int _PlayerTurnCounter;
        private Point _WindowSize;
        private int _FieldWidth, _FieldHeight;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
        private const int _ModifierButtonOffset = 10;
        private Point _MinFieldSize = new Point(4, 4), _MaxFieldSize = new Point(15, 9);
        private UIManager _GameScreenUIManager;
        private Random _Random;
        private Vector2 _FieldOffset;
    }
}
