using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameOfLife.Rules.GameRules;

namespace GameOfLife
{
    class Program
    {
        const string Title = "Game Of Life";

        static void Main(string[] args)
        {
           
            RuleUnderpopulation underPopulation = new RuleUnderpopulation();
            RuleOvercrowding overcrowding = new RuleOvercrowding();
            RuleCreationOfLife creationOfLife = new RuleCreationOfLife();

            GameState state = new GameState(3, 3);
            bool[,] initialState = new bool[3, 3];

            bool successfullLoad = true;

            if (args.Length > 0)
            {
                successfullLoad = ParseArgs(args, out initialState);
            }
            else
            {
                initialState[1, 0] = true;
                initialState[1, 1] = true;
                initialState[1, 2] = true;
            }

            if (successfullLoad)
            {
                state.LoadState(initialState);

                Game game = new Game(state,Title,true, '*', '-', underPopulation, overcrowding, creationOfLife);

                do
                {
                    game.Update();
                    Thread.Sleep(1000);
                } while (!Console.KeyAvailable);

                Console.ReadKey(); //Stops Execution of while loop
            }
            else
            {
                PrintArgsError();
            }
            
            Console.ReadKey(); //Stops from exiting
        }

        //Format required is sizeX:int sizeY:int states:int..(sizeX * sizeY)
        static bool ParseArgs(string[] args, out bool[,] result)
        {
            if (args.Length < 1)
            {
                result = null;
                return false;
            }

            int x;
            int y;

            if (!int.TryParse(args[0], out x))
            {
                result = null;
                return false;
            }

            if (!int.TryParse(args[1], out y))
            {
                result = null;
                return false;
            }

            //Check length is the one specified
            int argsLength = args.Length;

            if ((argsLength - 2) != (x * y))
            {
                result = null;
                return false;
            }

            result = new bool[x, y];
            int xCount = 0;
            int yCount = 0;

            for (int i = 2; i < argsLength; ++i)
            {
                byte temp;
                if (!byte.TryParse(args[i], out temp))
                {
                    result = null;
                    return false;
                }

                result[xCount, yCount] = temp >= 1 ? true : false;

                if ((yCount + 1) >= y)
                {
                    yCount = 0;
                    ++xCount;
                }
                else
                {
                    ++yCount;
                }
            }

            return true;
        }

        static void PrintArgsError()
        {
            Console.WriteLine("Invalid Command Line Arguments. Arguments should correspond to a 2D grid of length X and Y.");
            Console.WriteLine("Format for Arguments, X (int)  Y (int) Values (int)");
            Console.WriteLine("Values are the states of the grid, 0 = Empty , 1 = Live Cell");
        }
    }
}
