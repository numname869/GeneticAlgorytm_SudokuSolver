using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Genetic_sudoku
{
    internal class GeneticAlgorithm
    {
        Random random = new Random();
        //every generation consists of 12 paths with string and a array

        private Board _board;
        private Generation _generation;

        //it will need a generation

        public GeneticAlgorithm(Generation generation, Board board)
        {
            _board = board;
            _generation = generation;
        }

        public Tuple<string, Tuple<int, bool>[,]>[] CreateFirstGeneration()
        {
            Tuple<string, Tuple<int, bool>[,]>[] gen = new Tuple<string, Tuple<int, bool>[,]>[12];
            for (int i = 0; i < 12; i++)
            {
                var result = _generation.CreatePath();
                gen[i] = new Tuple<string, Tuple<int, bool>[,]>(result.Item1, result.Item2);
               
            }

            return gen;
        }

        public Tuple<string, Tuple<int, bool>[,]>[] GenerationInOrder(Tuple<string, Tuple<int, bool>[,]>[] gen)
        {
            

            

            //var sorted = gen.OrderBy(item => item.Item1.Length).ToArray();
            //desc
            var sorted = gen.OrderByDescending(item => item.Item1.Length).ToArray();

            return sorted;

        }


        // public static void DisplaySudoku(Tuple<int, bool>[,] sudoku)


        public void PrintGeneration(Tuple<string, Tuple<int, bool>[,]>[] gen)
        {
           
           
            for (int i = 0; i < 12; i++)
            {

                Board.DisplaySudoku(gen[i].Item2);


                Console.WriteLine(gen[i].Item1);

                Console.WriteLine();

            }
            
           
        }

        
        public void Mutation()
        {

        }

       

        public bool CheckIfParentsExists(Tuple<int, int>[] parents, int parent1, int parent2)
        {
            if(parents.Contains(new Tuple<int, int>(parent1, parent2)) || parents.Contains(new Tuple<int, int>(parent2, parent1)))
            {
                return true;
            }

            return false;
        }
        
        public Tuple<int, int>[] ParentSelection()
        {
            Tuple<int, int>[] parents = new Tuple<int, int>[6];
            
            for (int i = 0; i < 6; i++)
            {
                
                do
                {
                    parents[i] = new Tuple<int, int>(random.Next(0, 6), random.Next(0, 6));
                } while (parents[i].Item1 == parents[i].Item2 ||CheckIfParentsExists(parents, parents[i].Item1, parents[i].Item2) ); //tu jakas funkcja sprawdzająca czy istnieje juz taka para rodziców

               
            }
            return parents;
         }
        

        public (int , int ) PickRandomStringPart(string path)
        {
            int min = 0;
            int max = 0;
            do {
                 min = GetRandomDivisibleByThree(0, path.Length - 3);
                 max = GetRandomDivisibleByThree(min, path.Length - 3);
            } while (min >= max);


            return (min, max);
        }


        static int GetRandomDivisibleByThree(int min, int max)
        {
            Random rand = new Random();

          
            int firstMultiple = (min % 3 == 0) ? min : min + (3 - min % 3);

            
            int lastMultiple = (max % 3 == 0) ? max : max - (max % 3);

            if (firstMultiple > max)
            {
                throw new ArgumentException("No multiples of 3 in the given range");
            }

            
            int count = (lastMultiple - firstMultiple) / 3 + 1;
            int randomIndex = rand.Next(0, count);

            return firstMultiple + (randomIndex * 3);
        }

        public string EditString(string path, int min, int max)
        {
            string newpath = "";
            for (int i = min; i <= max; i++)
            {
                newpath = newpath + path[i];
            }

            return newpath;

        }


        public string Crossover(string parent1, string parent2)
        {
            string child = "";
            int HeadOrTails = random.Next(0, 2);
            if (HeadOrTails == 0)
            {
                child = parent1 + parent2;
            }
            else
            {
                child = parent2 + parent1;
            }

            return child;
        }

        public (string,string) CreateChild(Tuple<string, Tuple<int, bool>[,]>[] pastgen, Tuple<int, int>[] parents,  int i)
        {

           
            var parent1minmax_1 = PickRandomStringPart(pastgen[parents[i].Item1].Item1);
            var parent2minmax_1 = PickRandomStringPart(pastgen[parents[i].Item2].Item1);

            string parent1string_1 =  EditString(pastgen[parents[i].Item1].Item1 , parent1minmax_1.Item1, parent1minmax_1.Item2);
            string parent2string_1 =  EditString(pastgen[parents[i].Item2].Item1, parent2minmax_1.Item1, parent2minmax_1.Item2);


            var parent1minmax_2 = PickRandomStringPart(pastgen[parents[i].Item1].Item1);
            var parent2minmax_2 = PickRandomStringPart(pastgen[parents[i].Item2].Item1);

            string parent1string_2 = EditString(pastgen[parents[i].Item1].Item1, parent1minmax_2.Item1, parent1minmax_2.Item2);
            string parent2string_2 = EditString(pastgen[parents[i].Item2].Item1, parent2minmax_2.Item1, parent2minmax_2.Item2);



            string child1 = Crossover(parent1string_1, parent2string_1); 
            string child2 = Crossover(parent1string_2, parent2string_2);

            //need to iplement mutation later


            return (child1, child2);


        }

        public (string, Tuple<int, bool>[,]) CheckNewString(string path)
        {
            Tuple<int, bool>[,] board = _board.CopyBoard();
            string step = "";
            string possiblepath = "";
            for (int i = 0; i < path.Length; i = i+ 3) {
                
                step = EditString(path, i, i + 2);
                
                if (_generation.TakeStep(board, step))
                {
                  _board.EditBoard(board, step);
                    possiblepath = possiblepath + step;
                }
                else
                {
                    return (possiblepath, board);
                }
                
            }
            return (possiblepath, board);
        }

        public  Tuple<string, Tuple<int, bool>[,]>[] CreateChildren(Tuple<string, Tuple<int, bool>[,]>[] pastgen)
        {
            Tuple<int, int>[] parents = ParentSelection();
            Tuple<string, Tuple<int, bool>[,]>[] newgen = new Tuple<string, Tuple<int, bool>[,]>[12];
            for (int i = 1; i <= 6; i = i+2)
            {
               // new Tuple<int, bool>(9, false);
                var children = CreateChild(pastgen, parents, i);

                var value1 = CheckNewString(children.Item1);
                var value2 = CheckNewString(children.Item2);

                newgen[i] =   new Tuple< string, Tuple<int, bool>[,] >(value1.Item1, value1.Item2); //tu trzeba stworzyc nową tablice
                newgen[i + 1] = new Tuple<string, Tuple<int, bool>[,]>(value2.Item1, value2.Item2);

            }

            return newgen;


        }


       public void Run()
        {
            Tuple<string, Tuple<int, bool>[,]>[] firstgen = CreateFirstGeneration();
            Tuple<string, Tuple<int, bool>[,]>[] sortedgen = GenerationInOrder(firstgen);
            PrintGeneration(sortedgen);
            Tuple<string, Tuple<int, bool>[,]>[] newgen = CreateChildren(sortedgen);
            Tuple<string, Tuple<int, bool>[,]>[] sortednewgen = GenerationInOrder(newgen);
            PrintGeneration(sortednewgen);
        }


    }
}
