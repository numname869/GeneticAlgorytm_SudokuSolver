using System;
using System.Collections.Generic;
using Genetic_sudoku;


class Program
{
   
    static void Main()
    {
        
        int[,] sudoku = new int[9, 9];

        // Ustawiamy stałe wartości na planszy (np. przykładowe liczby w sudoku)
        sudoku[0, 0] = 5;
        sudoku[0, 1] = 3;
        sudoku[0, 4] = 7;
        sudoku[1, 0] = 6;
        sudoku[1, 3] = 1;
        sudoku[1, 4] = 9;
        sudoku[1, 5] = 5;
        sudoku[2, 1] = 9;
        sudoku[2, 2] = 8;
        sudoku[2, 7] = 6;
        sudoku[3, 0] = 8;
        sudoku[3, 4] = 6;
        sudoku[3, 8] = 3;
        sudoku[4, 0] = 4;
        sudoku[4, 3] = 8;
        sudoku[4, 5] = 3;
        sudoku[4, 8] = 1;
        sudoku[5, 0] = 7;
        sudoku[5, 4] = 2;
        sudoku[5, 8] = 6;
        sudoku[6, 1] = 6;
        sudoku[6, 6] = 2;
        sudoku[6, 7] = 8;
        sudoku[7, 3] = 4;
        sudoku[7, 4] = 1;
        sudoku[7, 5] = 9;
        sudoku[8, 4] = 8;
        sudoku[8, 7] = 7;
        sudoku[8, 8] = 9;

      Board board = new Board(sudoku);
        
        Board.DisplaySudoku(sudoku);

    }

   
}
