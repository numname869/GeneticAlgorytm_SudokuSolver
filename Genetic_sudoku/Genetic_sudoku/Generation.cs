using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genetic_sudoku;



namespace Genetic_sudoku
{

   
    internal class Generation 
    {
        //lets say one population is 12 paths
        //one path is  always lesser than 12 x 81 digits 
        //path ends when algorythm puts number in incorrect place 
        //when the generation is done it sorts it by the highest score 
        // 6 best paths have children - crossover and maybe ? mutation
       

        
        string[] _population;
        private Board _board;


     

        //will need probably 2 contructors
        public Generation(Board board)
        {
            _board = board;
        }

        public (string, bool) CreateStep()
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

               number = random.Next(1, 9);

            } while (_board.CheckIfAvaliableNumber(_board._sudoku, number) == false);
            step = step + number;
            int col = 0;
            int row = 0;

            Tuple<int, int>[] variations = new Tuple<int, int>[81];
            int i = 0;
            do
            {
                
                    col = random.Next(0, 9);
                    row = random.Next(1, 9);


    

            } while (_board.CheckCords(_board._sudoku, number, row,col) == false);

            logic = _board.CheckCords(_board._sudoku, number, row, col);

            step = step + col + row;

            return (step, logic);

        }

        
        public string CreatePath()
        {
            string path = "";
            var result = ("", true);
            do
            {
                result = CreateStep();

                path = path + result.Item1;

            } while (result.Item2 != false || ); //we also need to check if it found the solution or not

            if (result.Item2 == false)
            {

               
                string newpath = path.Remove(path.Length - 3); // Remove last 3 chars

                return newpath;
            }

            return path;

        }



        //now we need to seperate orginal board with solution boards for every path

    }
}
