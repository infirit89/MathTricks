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

        public GameScreen(Point windowSize) 
        {
            // NOTE: max field size: 9x15
            _WindowSize = windowSize;
            _GameScreenUIManager = new UIManager();
            _Random = new Random();

            _PlayerColor = new Color[2];
            _PlayerColor[0] = Color.Cyan;
            _PlayerColor[1] = Color.Orange;

        }

        private void GenerateBoard() 
        {
            _Field = new Square[Globals.FieldHeight, Globals.FieldWidth];

            _FieldOffset = new Vector2(
                                        _WindowSize.X * 0.5f -
                                            ((_SquareSize + 2) * _Field.GetLength(1) * 0.5f),
                                       _WindowSize.Y * 0.5f -
                                            ((_SquareSize + 2) * _Field.GetLength(0) * 0.5f));

            List<string> actions = new List<string> { "", "-", "*", "/" };

            for (int y = 0; y < Globals.FieldHeight; y++)
            {
                for (int x = 0; x < Globals.FieldWidth; x++)
                {
                    Vector2 squarePos = new Vector2((x * (_SquareSize + 2)) + _FieldOffset.X,
                                                    (y * (_SquareSize + 2)) + _FieldOffset.Y);

                    string squareValue;

                    if (x == 0 &&
                        y == 0 ||
                        x == Globals.FieldWidth - 1 &&
                        y == Globals.FieldHeight - 1)
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
                    _GameScreenUIManager.AddComponent(_Field[y, x].Image);

                    if(y == 0 && x == 0)
                        _Field[y, x].Image.Color = _PlayerColor[0];
                    else if(y == Globals.FieldHeight - 1 && x == Globals.FieldWidth - 1)
                        _Field[y, x].Image.Color = _PlayerColor[1];

                    _Field[y, x].Text = new Text(
                                                squareValue,
                                                _ArialFont, Rectangle.Empty);
                    
                    _Field[y, x].Image.AddChild(_Field[y, x].Text);
                }
            }
        }

        private void EnsureAllActionsIncluded(List<string> actions)
        {
            var includedActions = new List<string>();

            foreach (var cell in _Field)
            {
                includedActions.Add(cell.Text.Value[0].ToString());
            }

            foreach (var action in actions)
            {
                if (!includedActions.Contains(action))
                {
                    var cell = GetRandomCellWithoutAction(action);
                    int numberIndex = 2;
                    if (cell.Text.Value.Length == 2)
                        numberIndex = 1;

                    cell.Text.Value = $"{action} {cell.Text.Value[numberIndex]}";
                }
            }
        }

        private Square GetRandomCellWithoutAction(string action)
        {
            var random = new Random();

            while (true)
            {
                var row = random.Next(_Field.GetLength(0));
                var col = random.Next(_Field.GetLength(1));

                if (_Field[row, col].Text.Value[0].ToString() != action)
                    return _Field[row, col];
            }
        }

        private ValueRange GetValueRange(string action) 
        {
            ValueRange range = new ValueRange { Lower = 0, Upper = 9 };
            switch (action)
            {
                case "":
                    break;
                case "-":
                    range.Lower = 1;
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

        public override void OnLoad()
        {
            BeginGame();
            base.OnLoad();
        }

        public void BeginGame() 
        {
            _GameScreenUIManager.ClearComponents();
            GenerateBoard();

            _Player1 = new Player(
                                GetCeneteredPlayerPosition(
                                            _PlayerSize, 
                                            _Field[0, 0].Transform.Location, 
                                            _SquareSize),
                                _PlayerSize, Color.Blue);

            _Player2 = new Player(
                                GetCeneteredPlayerPosition(
                                            _PlayerSize,
                                            _Field[
                                                Globals.FieldHeight - 1,
                                                Globals.FieldWidth - 1].Transform.Location,
                                            _SquareSize),
                                _PlayerSize, Color.Yellow);

            _Field[0, 0].PlayerIndex = 0;
            _Field[Globals.FieldHeight - 1, Globals.FieldWidth - 1].PlayerIndex = 1;

            _PlayerTurnCounter = 0;

            const int scoreTextOffset = 5;
            _Player1ScoreText = new Text(
                                        "Player 1: 0",
                                        _ArialFont,
                                        Rectangle.Empty,
                                        false)
            {
                Color = Color.WhiteSmoke
            };

            _GameScreenUIManager.AddComponent(_Player1ScoreText);

            _Player1ScoreText.Transform.Position = new Vector2(scoreTextOffset, scoreTextOffset);

            Vector2 textSize = _ArialFont.MeasureString(_Player1ScoreText.Value);
            _Player2ScoreText = new Text(
                                        "Player 2: 0",
                                        _ArialFont,
                                        Rectangle.Empty,
                                        false)
            {
                Color = Color.WhiteSmoke
            };

            _GameScreenUIManager.AddComponent(_Player2ScoreText);

            _Player2ScoreText.Transform.Position = new Vector2(
                                                        scoreTextOffset, 
                                                        _Player1ScoreText.Transform.Position.Y +
                                                            (scoreTextOffset * 3) +
                                                            (int)textSize.Y);
        }

        private Point GetCeneteredPlayerPosition(int playerSize, Point squarePosition, int squareSize) 
        {
            return new Point((squareSize / 2) - (playerSize / 2) + squarePosition.X,
                               (squareSize / 2) - (playerSize / 2) + squarePosition.Y);
        }

        public override void LoadContent(ContentManager manager) 
        {
            _ArialFont = manager.Load<SpriteFont>("Salvar");
            Background = manager.Load<Texture2D>("bg");
            BackgroundTransform = new Rectangle(new Point(0, 0), _WindowSize);
        }

        public override void Update() 
        {
            if (!ValidMoveExists())
            {
                Globals.WinningPlayerName = 
                            _PlayerTurnCounter % 2 == 0 ? "Player 2" : "Player 1";
                ScreenManager.CurrentScreen = ScreenState.EndScreen;
            }

            if (Globals.IsSinglePlayer && _PlayerTurnCounter % 2 == 1)
            {
                foreach (var square in _Field.Cast<Square>().ToArray())
                {
                    if(PlacePiece(square)) 
                        break;
                }
            }
            else if (Input.IsButtonPressed(MouseButtons.Left))
            {
                Square square = _Field.Cast<Square>()
                                        .ToArray()
                                        .FirstOrDefault(
                                                    x => x
                                                        .Transform
                                                        .Intersects(
                                                            Input.GetMouseRect()));
                PlacePiece(square);
            }
        }
        
        public override void Draw()
        {
            Renderer.AddQuad(BackgroundTransform, Color.White, Background);

            _GameScreenUIManager.Draw();

            _Player1.Draw();
            _Player2.Draw();
        }

        private Color GetCurrentPlayerColor()
        {
            if (_PlayerTurnCounter == 0)
                return Color.White;

            return _PlayerColor[_PlayerTurnCounter % 2 != 0 ? 0 : 1];
        }

        private int CalculatePlayerScore(int playerScore, string squareValue) 
        {
            switch (squareValue[0])
            {
                case ' ': return playerScore += int.Parse(squareValue[1].ToString());
                case '-': return playerScore -= int.Parse(squareValue[2].ToString());
                case '*': return playerScore *= int.Parse(squareValue[2].ToString());
                case '/': return playerScore /= int.Parse(squareValue[2].ToString());
            }

            return 0;
        }

        private bool PlacePiece(Square square)
        {
            if (square != null)
            {
                if(IsMoveValid(square)) 
                {
                    SquareIndex ind = GetSquareIndexInMatrix(square);

                    Point newPlayerPosition = GetCeneteredPlayerPosition(
                                                                    _PlayerSize,
                                                                    square.Transform.Location,
                                                                    _SquareSize);
                    if (_PlayerTurnCounter % 2 == 0)
                    {
                        _Player1.Transform = new Rectangle(
                                                        newPlayerPosition,
                                                        _Player1.Transform.Size);

                        _Player1.Score = CalculatePlayerScore(_Player1.Score, square.Text.Value);
                        _Player1ScoreText.Value = $"Player 1: {_Player1.Score}";
                    }
                    else
                    {
                        _Player2.Transform = new Rectangle(
                                                        newPlayerPosition,
                                                        _Player2.Transform.Size);
                        _Player2.Score = CalculatePlayerScore(_Player2.Score, square.Text.Value);
                        _Player2ScoreText.Value = $"Player 2: {_Player2.Score}";
                    }
                    _Field[ind.Y, ind.X].PlayerIndex = _PlayerTurnCounter % 2;
                    _Field[ind.Y, ind.X].Image.Color = _PlayerColor[_PlayerTurnCounter % 2];
                    _PlayerTurnCounter++;
                    return true;
                }

            }

            return false;
        }

        private bool IsMoveValid(Square square)
        {
            SquareIndex index = GetSquareIndexInMatrix(square);
            
            Square playerSquare = GetCurrentPlayerSquare();
            SquareIndex playerSquareIndex = GetSquareIndexInMatrix(playerSquare);

            int xDirection = Math.Abs(playerSquareIndex.X - index.X);
            int yDirection = Math.Abs(playerSquareIndex.Y - index.Y);

            if (xDirection > 1 || yDirection > 1)
                return false;

            if (_Field[index.Y, index.X].PlayerIndex != -1)
                return false;

            return true;
        }

        private Square GetPlayerSquare(Player player)
        {
            return _Field
                        .Cast<Square>()
                        .ToArray()
                        .FirstOrDefault(
                            square => square
                                        .Transform
                                        .Intersects(player.Transform));
        }

        private Square GetCurrentPlayerSquare() 
        {
            Player currentPlayer = _PlayerTurnCounter % 2 == 0 ? _Player1 : _Player2;
            return GetPlayerSquare(currentPlayer);
        }

        private bool ValidMoveExists() 
        {
            Square playerSquare = GetCurrentPlayerSquare();
            SquareIndex playerSquareIndex = GetSquareIndexInMatrix(playerSquare);

            bool validMoveExists = false;

            if (playerSquareIndex.X + 1 < Globals.FieldWidth &&
                _Field[playerSquareIndex.Y, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 &&
                    _Field[playerSquareIndex.Y, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.Y + 1 > Globals.FieldHeight &&
                    _Field[playerSquareIndex.Y + 1, playerSquareIndex.X].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.Y - 1 >= 0 &&
                    _Field[playerSquareIndex.Y - 1, playerSquareIndex.X].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X + 1 < Globals.FieldWidth &&
                    playerSquareIndex.Y + 1 < Globals.FieldHeight &&
                    _Field[playerSquareIndex.Y + 1, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 &&
                    playerSquareIndex.Y + 1 < Globals.FieldHeight &&
                    _Field[playerSquareIndex.Y + 1, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X + 1 < Globals.FieldWidth &&
                    playerSquareIndex.Y - 1 >= 0 &&
                    _Field[playerSquareIndex.Y - 1, playerSquareIndex.X + 1].PlayerIndex == -1)
                validMoveExists = true;
            else if (playerSquareIndex.X - 1 >= 0 &&
                    playerSquareIndex.Y - 1 >= 0 &&
                    _Field[playerSquareIndex.Y - 1, playerSquareIndex.X - 1].PlayerIndex == -1)
                validMoveExists = true;

            return validMoveExists;
        }

        private SquareIndex GetSquareIndexInMatrix(Square square)
        {
            // convert from square location to the square index in the field matrix
            Vector2 squarePosition = square.Transform.Location.ToVector2();
            return new SquareIndex() 
            { 
                X = (int)((squarePosition.X - _FieldOffset.X) / (_SquareSize + 2)), 
                Y = (int)((squarePosition.Y - _FieldOffset.Y) / (_SquareSize + 2)) 
            };
        }

        private Square[,] _Field;
        private const int _SquareSize = 50;
        private SpriteFont _ArialFont;
        private Color[] _PlayerColor;
        private const int _PlayerSize = 20;
        private Player _Player1, _Player2;
        private int _PlayerTurnCounter;
        private Point _WindowSize;
        private UIManager _GameScreenUIManager;
        private Random _Random;
        private Vector2 _FieldOffset;
        private Text _Player1ScoreText, _Player2ScoreText;
    }
}
