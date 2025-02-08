using System;
using System.Collections.Generic;
using System.Linq;
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


        public bool CheckCords(Tuple<int, bool>[,] sudoku, int number, int row, int col)
        {
            if (CheckRow(sudoku, row, number) && CheckCol(sudoku, col, number))
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

        public bool CheckIfAvaliable(Tuple<int, bool>[,] sudoku , int number)
        {
            int numbers = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j].Item1 == Convert.ToInt32(number))
                    {
                        numbers++;
                    }
                }
            }

            if (numbers == 9)
            {
                return false;
            }
            else
            {
                return true;
            }

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

