using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Game
    {
        private GameState state;
        private List<Rule> gameRules;
        private int numberOfRules;
        private Renderer renderer;
        private string gameTitle;
        private bool renderTitle;

        //Empty Board
        public Game(string title, bool renderTitle,int sizeX, int sizeY,char filledCellSymbol, char emptyCellSymbol, params Rule[] rules)
        {
            state = new GameState(sizeX, sizeY);
            renderer = new Renderer(filledCellSymbol,emptyCellSymbol);
            gameRules = new List<Rule>();
            this.renderTitle = renderTitle;

            if (this.renderTitle)
            {
                gameTitle = title;
            }
            else
            {
                gameTitle = string.Empty;
            }

            if (rules != null)
            {
                numberOfRules = rules.Length;

                for (int i = 0; i < numberOfRules; ++i)
                {
                    gameRules.Add(rules[i]);
                }
            }
        }

        //For PreLoaded states
        public Game(GameState state, string title, bool renderTitle, char filledCellSymbol, char emptyCellSymbol, params Rule[] rules)
        {
            this.state = state;
            renderer = new Renderer(filledCellSymbol, emptyCellSymbol);
            gameRules = new List<Rule>();
            this.renderTitle = renderTitle;

            if (this.renderTitle)
            {
                gameTitle = title;
            }
            else
            {
                gameTitle = string.Empty;
            }

            if (rules != null)
            {
                numberOfRules = rules.Length;

                for (int i = 0; i < numberOfRules; ++i)
                {
                    gameRules.Add(rules[i]);
                }
            }
        }

        public void Update()
        {
            int sizeX = state.BoardSizeX;
            int sizeY = state.BoardSizeY;
            renderer.PreRenderBegin(gameTitle);

            for (int x = 0; x < sizeX; ++x)
            {
                for (int y = 0; y < sizeY; ++y)
                {
                    //Set up to render the current state
                    bool currentSpaceState = state.GetBoardSpaceState(x, y);
                    bool endOfRow = y == (sizeY - 1);
                    renderer.PreRenderUpdate(currentSpaceState, endOfRow);

                    //Apply the Rules to generate the next state
                    for(int i =0; i < numberOfRules; ++i)
                    {
                        //Does not mutate current state
                        gameRules[i].Execute(state, x, y);
                    }
                }
            }

            //Switch current state to the newly generated one
            state.EndFrame();
            //Render the previous state we set up
            renderer.Render();
            Console.WriteLine();
        }
    }
}
