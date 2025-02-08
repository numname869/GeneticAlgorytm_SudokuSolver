using System;
using System.Collections.Generic;
using Genetic_sudoku;

namespace Genetic_sudoku
{
    class Program
    {

        static void Main()
        {
            

            Tuple<int,bool >[,] sudoku = new Tuple<int, bool>[9, 9];


            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j] = new Tuple<int, bool>(0, false);
                }
            }



            sudoku[0, 0] = new Tuple<int, bool>(5, false);
            sudoku[0, 1] = new Tuple<int, bool>(3, false);
            sudoku[0, 4] = new Tuple<int, bool>(7, false);
            sudoku[1, 0] = new Tuple<int, bool>(6, false);
            sudoku[1, 3] = new Tuple<int, bool>(1, false);
            sudoku[1, 4] = new Tuple<int, bool>(9, false);
            sudoku[1, 5] = new Tuple<int, bool>(5, false);
            sudoku[2, 1] = new Tuple<int, bool>(9, false);
            sudoku[2, 2] = new Tuple<int, bool>(8, false);
            sudoku[2, 7] = new Tuple<int, bool>(6, false);
            sudoku[3, 0] = new Tuple<int, bool>(8, false);
            sudoku[3, 4] = new Tuple<int, bool>(6, false);
            sudoku[3, 8] = new Tuple<int, bool>(3, false);
            sudoku[4, 0] = new Tuple<int, bool>(4, false);
            sudoku[4, 3] = new Tuple<int, bool>(8, false);
            sudoku[4, 5] = new Tuple<int, bool>(3, false);
            sudoku[4, 8] = new Tuple<int, bool>(1, false);
            sudoku[5, 0] = new Tuple<int, bool>(7, false);
            sudoku[5, 4] = new Tuple<int, bool>(2, false);
            sudoku[5, 8] = new Tuple<int, bool>(6, false);
            sudoku[6, 1] = new Tuple<int, bool>(6, false);
            sudoku[6, 6] = new Tuple<int, bool>(2, false);
            sudoku[6, 7] = new Tuple<int, bool>(8, false);
            sudoku[7, 3] = new Tuple<int, bool>(4, false);
            sudoku[7, 4] = new Tuple<int, bool>(1, false);
            sudoku[7, 5] = new Tuple<int, bool>(9, false);
            sudoku[8, 4] = new Tuple<int, bool>(8, false);
            sudoku[8, 7] = new Tuple<int, bool>(7, false);
            sudoku[8, 8] = new Tuple<int, bool>(9, false);


            Board board = new Board(sudoku);

            Board.DisplaySudoku(sudoku);
            Console.WriteLine();

             Generation generation = new Generation(board);

             generation.CreatePath();

              var result  = generation.CreatePath();

             Board.DisplaySudoku(result.Item2);
             Console.WriteLine(result.Item1);







        }


    }

}