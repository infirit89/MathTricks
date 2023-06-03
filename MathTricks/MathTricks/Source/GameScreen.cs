using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Queens
{
    class GameScreen : Screen
    {
        struct SquareIndex 
        {
            public int X, Y;
        }

        public delegate void OnGameLost(string winningPlayer);

        public GameScreen(Point windowSize) 
        {
            // NOTE: max field size: 9x15
            _FieldWidth = 8;
            _FieldHeight = 8;
            
            _WindowSize = windowSize;

            _SettingsManager = new UIManager();
            _Pieces = new List<Piece>();
        }

        public void BeginGame() 
        {
            _Field = new Square[_FieldHeight, _FieldWidth];
            Vector2 fieldOffset = new Vector2(_WindowSize.X * 0.5f - (((_SquareSize + 2) * _Field.GetLength(1)) * 0.5f),
                                              _WindowSize.Y * 0.5f - (((_SquareSize + 2) * _Field.GetLength(0)) * 0.5f));

            for (int y = 0; y < _Field.GetLength(0); y++)
            {
                for (int x = 0; x < _Field.GetLength(1); x++)
                {
                    Vector2 squarePos = new Vector2((x * (_SquareSize + 2)) + fieldOffset.X, 
                                                    (y * (_SquareSize + 2)) + fieldOffset.Y);
                    _Field[y, x] = new Square(squarePos, _SquareSize);
                }
            }

            _PlayerTurnCounter = 0;
            _Pieces.Clear();

            _HasGameBegun = true;
        }

        public override void LoadContent(ContentManager manager) 
        {
            _QueenTexture = manager.Load<Texture2D>("WhiteQueenIcon");
            _BlackQueenTexture = manager.Load<Texture2D>("BlackQueenIcon");

            _ArialFont = manager.Load<SpriteFont>("Arial");

            SetupSettingUI();

            _pBackground = manager.Load<Texture2D>("GameBackground");
            _pBackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public override void Update() 
        {
            if (!_HasGameBegun)
                _SettingsManager.Update();
            else 
            {
                Square firstAvailSquare = _Field.Cast<Square>().ToArray().FirstOrDefault(x => x.CanPlaceQueen);
                if (firstAvailSquare == null) 
                {
                    OnGameLostEvent(_PlayerTurnCounter % 2 == 0 ? "Player 2" : "Player 1");
                    _HasGameBegun = false;
                    ApplicationManager.CurrentState = ApplicationState.EndScreen;
                }

                if (OSMouse.IsButtonPressed(MouseButtons.Left)) 
                {
                    Square square = _Field.Cast<Square>().ToArray().FirstOrDefault(x => x.Transform.Intersects(OSMouse.GetMouseRect()));                    
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
                    for (int x = 0; x < _Field.GetLength(1); x++)
                        GraphicsManager.AddQuad(_Field[y, x].Transform, _Field[y, x].CanPlaceQueen ? Color.White : Color.Gray);

                foreach (var piece in _Pieces)
                    piece.Draw();
            } 
        }

        private Texture2D GetCurrentPieceTexture() => _PlayerTurnCounter % 2 == 0 ? _QueenTexture : _BlackQueenTexture;


        private void PlacePiece(Square square)
        {
            if (square != null)
            {
                SquareIndex ind = GetSquareIndexInMatrix(square);

                if (IsMoveValid(ind))
                {
                    _Pieces.Add(new Piece(square.Transform.Location.ToVector2(), 50, GetCurrentPieceTexture()));
                    UpdateBoard(ind);
                    _PlayerTurnCounter++;
                }
            }
        }

        private bool IsMoveValid(SquareIndex index)
        {
            if ((!_Field[index.Y, index.X].CanPlaceQueen) || !_Field[index.Y, index.X].CanPlaceQueen)
                return false;

            return true;
        }

        private void UpdateBoard(SquareIndex index)
        {
            for (int i = 0; i < _Field.GetLength(0); i++)
                _Field[i, index.X].CanPlaceQueen = false;

            for (int i = 0; i < _Field.GetLength(1); i++)
                _Field[index.Y, i].CanPlaceQueen = false;

            for (int i = index.Y - 1, j = index.X - 1;
                i >= 0 && j >= 0; i--, j--)
                _Field[i, j].CanPlaceQueen = false;

            for (int i = index.Y + 1, j = index.X + 1;
                i < _Field.GetLength(0) && j < _Field.GetLength(1); i++, j++)
                _Field[i, j].CanPlaceQueen = false;

            for (int i = index.Y - 1, j = index.X + 1;
                 i >= 0 && j < _Field.GetLength(1); i--, j++)
                _Field[i, j].CanPlaceQueen = false;

            for (int i = index.Y + 1, j = index.X - 1;
                i < _Field.GetLength(0) && j >= 0; i++, j--)
                _Field[i, j].CanPlaceQueen = false;

        }

        private SquareIndex GetSquareIndexInMatrix(Square square)
        {
            for (int y = 0; y < _Field.GetLength(0); y++)
            {
                for (int x = 0; x < _Field.GetLength(1); x++)
                {
                    if (_Field[y, x] == square)
                        return new SquareIndex() { X = x, Y = y };
                }
            }

            return new SquareIndex() { X = -1, Y = -1 };
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
        private Texture2D _QueenTexture;
        private Texture2D _BlackQueenTexture;
        private List<Piece> _Pieces;
        private int _PlayerTurnCounter;
        private Point _WindowSize;
        private int _FieldWidth, _FieldHeight;
        private const int _ButtonWidth = 150, _ButtonHeight = 50;
        private const int _ModifierButtonOffset = 10;
        private Point _MinFieldSize = new Point(2, 2), _MaxFieldSize = new Point(15, 9);
    }
}
