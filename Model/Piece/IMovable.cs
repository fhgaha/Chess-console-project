using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IMovable
    {
        List<Location> GetValidMoves(Board board);
        List<Location> GetValidMoves(Board board, Square square);
        void MakeMove(Square square);
    }
}
