using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_sudoku
{
    internal class Board
    {
        private int[,] _sudoku;



        public Board(int[,] sudoku)
        {
            _sudoku = sudoku;
        }


        public static void DisplaySudoku(int[,] sudoku)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (j % 3 == 0 && j != 0)
                        Console.Write("| ");

                    Console.Write(sudoku[i, j] == 0 ? ". " : sudoku[i, j] + " ");


                    if (j == 8)
                        Console.WriteLine();
                }


                if ((i + 1) % 3 == 0 && i != 8)
                    Console.WriteLine("------+-------+------");
            }
        }

    }
}
