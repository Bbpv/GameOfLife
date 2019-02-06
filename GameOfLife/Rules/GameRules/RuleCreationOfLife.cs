using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules.GameRules
{
    //When an empty position has exactly three neighbouring cells
    //Then a cell is created in this position
    public class RuleCreationOfLife : Rule
    {
        public override void Execute(GameState state, int x, int y)
        {
            bool isEmpty = !state.GetBoardSpaceState(x, y);
            if(isEmpty)
            {
                if(state.GetNumberOfNeighbours(x, y) == 3)
                {
                    state.SetBoardSpaceState(x, y, true);
                }
            }
        }
    }
}
