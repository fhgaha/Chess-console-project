using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var board = new Board();
            board.PrintBoard();

            //board.LightPieces.ForEach(p => Console.WriteLine(p));

            while (true)
            {
                var line = Console.ReadLine();
                var fromTo = line.Split(new[] { "->" }, StringSplitOptions.None);
                var files = (File[])Enum.GetValues(typeof(File));

                var fromFile = files.Single(f => f.ToString() == Char.ToUpper(fromTo[0][0]).ToString());
                var fromRank = int.Parse(fromTo[0][1].ToString());

                var toFile = files.Single(f => f.ToString() == Char.ToUpper(fromTo[1][0]).ToString());
                var toRank = int.Parse(fromTo[1][1].ToString());

                var fromSquare = board.LocationSquareMap[new Location(fromFile, fromRank)];
                var toSquare = board.LocationSquareMap[new Location(toFile, toRank)];

                fromSquare.CurrentPiece.MakeMove(toSquare);
                fromSquare.Reset();

                board.PrintBoard();
            }


            #region MyRegion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            #endregion
        }

        private static void PrintPiece(IMovable piece)
        {
            piece.GetValidMoves(null);
        }
    }
}
