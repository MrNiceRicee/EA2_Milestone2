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

            //Board x = new Board(10,.1);
            //Console.WriteLine(printBoard(x));
            //Console.WriteLine(printBoard(x,1));
            int gamesWon = 0;
            int gamesLost = 0;
            string lastboard = "";
            do
            {
                if (gamesWon+gamesLost==0)
                {
                    Console.WriteLine("Play game? Yes or No");

                }
                else
                {
                    Console.WriteLine("Play another game? Yes or No" +
                        "\nGames won: "+gamesWon + ". Games lost: "+gamesLost);

                }
                String answer = Console.ReadLine();
                if (stringCheck(answer,"yes","y","1"))
                {
                    //create board
                    Write("Create Board Size: 0-50");
                    int BoardSize = getIntNumber(2,50);
                    Write("Difficulty: 0.0-.6 (Based on percentage)");
                    double BoardDifficulty = getDoubleNumber(.01,.6);
                    Board board = new Board(BoardSize,BoardDifficulty);

                    Write("Generated Board");

                    Write(printBoard(board));
                    do
                    {
                        //Ask player to select cell in game
                        Write("Select X-Axis coordinate: ");
                        int xAxis = getIntNumber(0, BoardSize);
                        Write("Select Y-Axis coordinate: ");
                        int yAxis = getIntNumber(0, BoardSize);


                        //print board
                        board.Grid[yAxis, xAxis].Visited = true;
                        board.Grid[yAxis, xAxis].LiveNeighbors = board.checkNeighbor(board.Grid[yAxis,xAxis],1);
                        board.revealNearbyZero(board.Grid[yAxis, xAxis]);

                        //check if player is still in game
                        if (board.Grid[xAxis,yAxis].Live)
                        {
                            Console.WriteLine("You Lost!");
                            gamesLost++;
                            foreach (var item in board.Grid)
                            {
                                item.Visited = true;
                            }
                            lastboard = printBoard(board) +
                                "\nSorry on the lost! Here are some stats:" +
                                "\nSafe Cells: " + board.getCells().Count(cells => !cells.Live) +
                                "\nBomb Cells: " + board.getCells().Count(cells => cells.Live);
                            break;
                        }
                        else
                        {
                            if (board.getCells().Count(cells=> !cells.Live)==board.getCells().Count(cell=> !cell.Live && cell.Visited))
                            {
                                Console.WriteLine("You won!");
                                gamesWon++;
                                foreach (var item in board.Grid)
                                {
                                    item.Visited = true;
                                }
                                lastboard = printBoard(board) +
                                    "\nCongratulations on winning! Here are some stats:" +
                                "\nSafe Cells: " + board.getCells().Count(cells => !cells.Live) +
                                "\nBomb Cells: " + board.getCells().Count(cells => cells.Live);
                                break;
                            }
                            else
                            {
                                Write(printBoard(board));
                            }
                        }

                    } while (true);
                    Write(lastboard);
                }
                else if (stringCheck(answer, "no", "n", "2"))
                {
                    break; // end loop
                }
            } while (true);
            Console.Read();
        }





        //just got tired of writing consolewrite, no other reason
        public static void Write(String say)
        {
            Console.WriteLine(say);
        }

        //check if said name was within range
        public static int getIntNumber(int min,int max)
        {
            do
            {
                if (int.TryParse(Console.ReadLine(), out int x))
                {
                    if (x < max && x >= min)
                    {
                        return x;
                    }
                    else if (x < min)
                    {
                        Console.WriteLine("Please enter number greater than: " + min);
                    }
                    else
                    {
                        Console.WriteLine("Please enter number less than: " + max);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            } while (true);
        }

        public static double getDoubleNumber(double min,double max)
        {
            do
            {
                if (Double.TryParse(Console.ReadLine(), out double x))
                {
                    if (x < max && x >= min)
                    {
                        return x;
                    }
                    else if (x < min)
                    {
                        Console.WriteLine("Please enter number greater than: " + min);
                    }
                    else
                    {
                        Console.WriteLine("Please enter number less than: " + max);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            } while (true);
        }

        //just checking if string equals something
        public static bool stringCheck(String check,String first, String second, String third)
        {
            if (second.Length>0)
            {
                if (third.Length>0)
                {
                    if ((check.Equals(first, StringComparison.InvariantCultureIgnoreCase))|| (check.Equals(second, StringComparison.InvariantCultureIgnoreCase))
                        || (check.Equals(third, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if ((check.Equals(first, StringComparison.InvariantCultureIgnoreCase)) || (check.Equals(second, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (check.Equals(first,StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
                    if (cell.Live && cell.Visited)
                    {
                        r += "| X ";
                    }
                    else if (cell.Visited && !cell.Live)
                    {
                        r += "| " + cell.LiveNeighbors + " ";
                    }
                    else
                    {
                        r += "|   ";
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
