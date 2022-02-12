using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Board
    {
        private const int BoardLength = 8;
        private Square[,] boardSquares = new Square[BoardLength, BoardLength];
        public Dictionary<Location, Square> LocationSquareMap { get; }
        public List<AbstractPiece> LightPieces { get; private set; }
        public List<AbstractPiece> DarkPieces { get; private set; }

        public Board()
        {
            LightPieces = new List<AbstractPiece>();
            DarkPieces = new List<AbstractPiece>();

            LocationSquareMap = new Dictionary<Location, Square>();
            var pieces = PieceFactory.GetPieces();

            for (int i = 0; i < boardSquares.GetLength(0); i++)
            {
                var column = 0;
                var currentColor = i % 2 == 0 ? SquareColor.Light : SquareColor.Dark;

                foreach (File file in Enum.GetValues(typeof(File)))
                {
                    var newSquare = new Square(currentColor, new Location(file, BoardLength - i));
                    if (pieces.ContainsKey(newSquare.Location))
                    {
                        var piece = pieces[newSquare.Location];
                        newSquare.CurrentPiece = piece;
                        newSquare.IsOccupied = true;
                        piece.CurrentSquare = newSquare;

                        if (piece.PieceColor == PieceColor.Light)
                            LightPieces.Add(piece);
                        else DarkPieces.Add(piece);
                    }
                    boardSquares[i, column] = newSquare;
                    currentColor = currentColor == SquareColor.Light ? SquareColor.Dark : SquareColor.Light;
                    column++;

                    LocationSquareMap.Add(newSquare.Location, newSquare);
                }
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < boardSquares.GetLength(0); i++)
            {
                Console.Write(BoardLength - i + " ");
                for (int j = 0; j < boardSquares.GetLength(1); j++)
                {
                    if (boardSquares[i, j].IsOccupied)
                    {
                        var piece = boardSquares[i, j].CurrentPiece;
                        if (piece is Knight) Console.Write("N ");
                        else Console.Write(piece.Name[0] + " ");
                    }
                    else Console.Write("- ");
                }
                Console.WriteLine();
            }

            Console.Write("  ");
            foreach (var file in Enum.GetValues(typeof(File)))
                Console.Write(file + " ");
            Console.WriteLine("\n");
        }
    }
}
