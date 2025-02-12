using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Mail;
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


        public void GerRidOfNULL(Tuple<string, Tuple<int, bool>[,]>[] gen)
        {
            for (int i = 0; i < 12; i++)
            {
                if (gen[i] == null)
                {
                    gen[i] = new Tuple<string, Tuple<int, bool>[,]>("0", _board.ReturnEmptyBoard()); // tu cos bedzie trzeba wlozyc
                }
            }

        }

public Tuple<string, Tuple<int, bool>[,]>[] GenerationInOrder(Tuple<string, Tuple<int, bool>[,]>[] gen)
        {
            //z jakiegos powodu dostaje null i nie ma zadnej tablicy z true?? tak jakby wgl nic nie wyłapywało, co jest nie tak, string jest pusty
            if (gen == null || gen.Any(item => item == null))
            {
                throw new ArgumentNullException(nameof(gen), "The generation array contains null elements.");
            }

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
            List<int> parentsid = new List<int> { 0, 1, 2, 3, 4, 5 };

         

            for(int i = 0; i < 3; i++)
            {
                do
                {
                    parents[i] = new Tuple<int, int>(parentsid[random.Next(0, parentsid.Count)], parentsid[random.Next(0, parentsid.Count)]);
                } while (parents[i].Item1 == parents[i].Item2 );

                parentsid.Remove(parents[i].Item1);
                parentsid.Remove(parents[i].Item2);
                Console.WriteLine($"{parents[i].Item1} {parents[i].Item2}");
            }






            return parents;
        }
        

        public (int , int ) PickRandomStringPart(string path)
        {
            int min = 0;
            int max = 0;


            if(path.Length == 3) return (0, 2);

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
            if (min < 0 || max >= path.Length || min > max)
            {
                throw new ArgumentOutOfRangeException("min or max is out of range of the string.");
            }

            
            if (path.Length == 3)
            {
                return path;
            }

          
            StringBuilder newpath = new StringBuilder();
            for (int i = min; i <= max; i++)
            {
                newpath.Append(path[i]);
            }

         
            _generation.CheckIfPathNull(newpath.ToString());

            return newpath.ToString();

        }


        public string Crossover(string parent1, string parent2)
        {
            // Check for null or empty parents
            if (string.IsNullOrEmpty(parent1) || string.IsNullOrEmpty(parent2))
            {
                throw new ArgumentException("Both parent strings must be non-null and non-empty.");
            }

            // Randomly choose which parent will be first
            int HeadOrTails = random.Next(0, 2);
            string child = HeadOrTails == 0 ? parent1 + parent2 : parent2 + parent1;

            return child;
        }

        public (string,string) CreateChild(Tuple<string, Tuple<int, bool>[,]>[] pastgen, Tuple<int, int>[] parents,  int i , int parentindex)
        {


          //od nowa

         



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


            int parentindex = 0;
            for (int i = 0; i < 6; i = i+2)
            {
               // new Tuple<int, bool>(9, false);
                var children = CreateChild(pastgen, parents, i , parentindex);

                var value1 = CheckNewString(children.Item1);
                var value2 = CheckNewString(children.Item2);

                newgen[i] =   new Tuple< string, Tuple<int, bool>[,] >(value1.Item1, value1.Item2); //tu trzeba stworzyc nową tablice
                newgen[i + 1] = new Tuple<string, Tuple<int, bool>[,]>(value2.Item1, value2.Item2);
                parentindex++;

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
