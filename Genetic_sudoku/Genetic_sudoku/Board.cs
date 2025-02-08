using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Genetic_sudoku;

namespace Genetic_sudoku
{
    internal class Board
    {
        internal Tuple<int, bool>[,] _sudoku;





        public Board(Tuple<int, bool>[,] sudoku)
        {
            _sudoku = sudoku;
        }

        public  Tuple<int, bool>[,] CopyBoard()
        {
            Tuple<int, bool>[,] copy = new Tuple<int, bool>[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    copy[i, j] = _sudoku[i, j];
                }
            }
            return copy;
        }

        public bool CheckSolution(Tuple<int, bool>[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!CheckRow(board, i, 9))
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (!CheckCol(board, i, 9))
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    if (!CheckSquare(board, i, j, 9))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void EditBoard(Tuple<int, bool>[,] sudoku, string step)
        {
            char[] options  = step.ToCharArray();

            int number = Convert.ToInt32(options[0].ToString());
            int row = Convert.ToInt32(options[1].ToString());
            int col = Convert.ToInt32(options[2].ToString());

           sudoku[row, col] = new Tuple<int, bool>(number, true);

        }


        public bool CheckRow(Tuple<int, bool>[,] sudoku, int row, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[row, i].Item1 == number)
                {
                    return false;
                }
            }
            return true;

        }

        public bool CheckSquare(Tuple<int, bool>[,] sudoku, int row, int col, int number)
        {
            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j].Item1 == number)
                    {
                        return false; // Number already exists in the 3x3 square
                    }
                }
            }
            return true; // Number is valid
        }

        public bool IsSolved(Tuple<int, bool>[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j].Item1 == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckCords(Tuple<int, bool>[,] sudoku, int number, int row, int col)
        {
            if (CheckRow(sudoku, row, number) && CheckCol(sudoku, col, number) && CheckSquare(sudoku,row,  col, number))
            {
                if (sudoku[row, col].Item1 == 0)
                
                    return true;

                }

            return false;
        }
        
        public bool CheckCol(Tuple<int, bool>[,] sudoku, int col, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i, col].Item1 == number)
                {
                    return false;
                }
            }
            return true;

        }

        public bool CheckPossibleVariations(Tuple<int, int>[] variations, int rows, int cols)
        {
            for(int i = 0; i < variations.Length; i++)
            {
                if (variations[i].Item1 == rows && variations[i].Item2 == cols)
                {
                    return false;
                }
            }

            return true;
        }
        public bool CheckIfAvaliableCords(Tuple<int, bool>[,] sudoku , int row, int col)
        {
            return sudoku[row, col].Item1 != 0 ? false : true;
                


        }


        public bool CheckIfAvaliableNumber(Tuple<int, bool>[,] sudoku, int number)
        {
            int numbers = 0;

            for (int i = 0; i < 9; i++)
            {
               for (int j = 0; j < 9; j++)
                {
                    if(sudoku[i,j].Item1 == number)
                    {
                        numbers++;
                    }
                }
            }


            return numbers == 9 ? false : true;


        }
        public static void DisplaySudoku(Tuple<int, bool>[,] sudoku)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (j % 3 == 0 && j != 0)
                        Console.Write("| ");

                    if (sudoku[i, j].Item1 == 0)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        if (sudoku[i, j].Item2 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(sudoku[i, j].Item1 + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write(sudoku[i, j].Item1 + " ");
                        }
                    }


                        if (j == 8)
                            Console.WriteLine();
                    }


                    if ((i + 1) % 3 == 0 && i != 8)
                        Console.WriteLine("------+-------+------");
                }
            }

        }
    }

