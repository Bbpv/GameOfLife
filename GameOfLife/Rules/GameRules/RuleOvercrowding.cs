using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules.GameRules
{
    // When a live cell has more than three neighbours
    // Then this cell dies
    public class RuleOvercrowding : Rule
    {
        public override void Execute(GameState state, int x, int y)
        {
            if (state.GetBoardSpaceState(x, y))
            {
                if (state.GetNumberOfNeighbours(x, y) > 3)
                {
                    state.SetBoardSpaceState(x, y, false);
                }
            }
        }
    }
}
