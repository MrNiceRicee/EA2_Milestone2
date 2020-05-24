using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Board
    {
        public int Size { get; set; }

        public Cell[,] Grid { get; set; }

        public double Difficulty { get; set; }

        Random random = new Random();

        public Board(int size)
        {
            Size = size;
            Grid = new Cell[size, size];
            Difficulty = .5;
            setupLiveNeighbors(Grid);
        }

        public Board(int size, double difficulty)
        {
            Size = size;
            Grid = new Cell[size, size];
            Difficulty = difficulty;
            setupLiveNeighbors(Grid);
        }


        public void setupLiveNeighbors(Cell[,] cells)
        {
            int LiveNumbers = 0;
            int count = 0;
            for (int col = 0; col < cells.GetLength(0); col++)
            {
                for (int row = 0; row < cells.GetLength(1); row++)
                {
                    cells[col, row] = new Cell();
                    cells[col, row].Column = col;
                    cells[col, row].Row = row;
                    count++;
                    if (LiveNumbers < ((Size*Size)*Difficulty)) {
                        //hard limit the amount of cells to be live to the difficulty
                        if (random.Next(0, (Size*Size)) < ((Size*Size)*Difficulty))
                        {
                            //get total max and multiplied by difficulty, to find the point to calculate the percentage
                            cells[col, row].Live = true;
                            LiveNumbers++;
                            //Console.WriteLine("Position:"+count+". Live:"+LiveNumbers+".\t"+col +","+row);
                        }
                    }
                }
                
            }
        }

        public int checkNeighbor(Cell[,] cells, Cell current, int distance)
        {
            List<Cell> listofcells = cells.Cast<Cell>().ToList();
            int x = current.Row;
            int y = current.Column;
            //convert to list
            var nearbycells = listofcells.Where(cell => cell.Row >= (x - distance) && cell.Row <= (x + distance)
                                                 && cell.Column >= (y - distance) && cell.Column <= (y + distance)
                                                 && cell.Live == true);
            //lambda code to see which cells are live around the current cell
            var currentcell = listofcells.Where(cell => cell.Row == x && cell.Row == y);

            nearbycells.Except(currentcell).ToList();
            return nearbycells.Count();
        }

    }
}
