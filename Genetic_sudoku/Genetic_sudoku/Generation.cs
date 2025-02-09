using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genetic_sudoku;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace Genetic_sudoku
{

   
    internal class Generation 
    {
        //lets say one population is 12 paths
        //one path is  always lesser than 12 x 81 digits 
        //path ends when algorythm puts number in incorrect place 
        //when the generation is done it sorts it by the highest score 
        // 6 best paths have children - crossover and maybe ? mutation
       

        
       
        private Board _board;


     

        //will need probably 2 contructors
        public Generation(Board board)
        {
            _board = board;
        }

        public (string, bool) CreateStep(Tuple<int, bool>[,] board)
        {
            bool logic = true;
            //it cant choose occupied place
            //it cant choose a number that is already in the row or column
            //it cant choose a number that is already in the 3x3 square
            string step = "";
            Random random = new Random();
            int number = 0;
            do
            {

               number = random.Next(1, 10);

            } while (_board.CheckIfAvaliableNumber( board, number) == false);
            step = step + number;
            int col = 0;
            int row = 0;

            Tuple<int, int>[] variations = new Tuple<int, int>[81];

            //every step can adjust 10 times max

            for (int i = 0; i < 10; i++)
            {
                col = random.Next(0, 9);
                row = random.Next(0, 9);

                if (_board.CheckCords(board, number, row, col) == true) break;
            }
                


           

            logic = _board.CheckCords(board, number, row, col);

            step = step + col + row;

            return (step, logic);

        }

        public  bool TakeStep(Tuple<int, bool>[,] board , string step)
        {
            int number = int.Parse(step[0].ToString()); 
            string row = step[1].ToString();
            string col = step[2].ToString();
           if( _board.CheckIfAvaliableNumber(board, number) == false ) return  false;
            if (_board.CheckCords(board, number, int.Parse(row), int.Parse(col)) == false) return  false;
            return  true;


        }

       



        public (string , Tuple<int, bool>[,]) CreatePath()
        {
            Tuple<int, bool>[,] board = _board.CopyBoard();
            string path = "";
            var result = ("", true);
            do
            {
                result = CreateStep(board);

                path = path + result.Item1;
                
             
               if(result.Item2 == true)
                {
                    int row = int.Parse(result.Item1[2].ToString());
                    int col = int.Parse(result.Item1[1].ToString());
                    int number = int.Parse(result.Item1[0].ToString());

                    board[row, col] = new Tuple<int, bool>(number, true);

                 
                }
                
                    

            } while (result.Item2 != false || _board.CheckSolution(board) ); 

            if (result.Item2 == false)
            {

               
                string newpath = path.Remove(path.Length - 3); // Remove last 3 chars

                return (newpath, board);
            }

            return (path, board);

        }



        //now we need to seperate orginal board with solution boards for every path

    }
}
