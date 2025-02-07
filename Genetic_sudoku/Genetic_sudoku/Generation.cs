using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genetic_sudoku;

Random random = new Random();

namespace Genetic_sudoku
{

   
    internal class Generation
    {
        //lets say one population is 12 paths
        //one path is  always lesser than 12 x 81 digits 
        //path ends when algorythm puts number in incorrect place 
        //when the generation is done it sorts it by the highest score 
        // 6 best paths have children - crossover and maybe ? mutation


        string _path;
        string[] _population;
        

        //will need probably 2 contructors
        public Generation()
        {
            _path = "";
            _population = new string[10];
        }

        public void CreatePath(string path)
        {
            byte [] digit = new byte [4];
            random.NextBytes(digit, 0, 1001);
        }



    }
}
