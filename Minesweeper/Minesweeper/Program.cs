using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {

            Board x = new Board(10,.1);
            //Console.WriteLine(printBoard(x));
            //Console.WriteLine(printBoard(x,1));

            Console.Read();
        }


        public static String printBoard(Board board, int x)
        {
            String r = "";
            for (int row = 0; row < board.Grid.GetLength(0); row++)
            {
                for (int col = 0; col < board.Grid.GetLength(1); col++)
                {
                    r += board.checkNeighbor(board.Grid, board.Grid[row, col],1)+"\n";
                }
            }
            return r;
        }

        public static String printBoard(Board board)
        {
            String r = "";
            for (int i = 0; i < board.Grid.GetLength(1); i++)
            {
                r += "| "+i+" ";
            }
            for (int row = 0; row < board.Grid.GetLength(0); row++)
            {

                //do the numbers
                r += "|\n";
                for (int i = 0; i < board.Grid.GetLength(1); i++)
                {
                         
                    r += "----";
                }
                //do pretty lines on the board
                r += "\n";
                for (int col = 0; col < board.Grid.GetLength(1); col++)
                {
                    var cell = board.Grid[row, col];
                    if (cell.Live)
                    {
                        r += "| X ";
                    }else
                    {
                        //r += "| - ";
                        r += "| "+board.checkNeighbor(board.Grid,cell,1) +" ";
                    }
                }
                
                r += "| "+ row+"";        // the Columns 
                //started a new line
            }
            r += "\n";
            for (int i = 0; i < board.Grid.GetLength(1); i++)
            {
                r += "----";
            }
            // last separation line on the board
            return r;
        }
    }
}
