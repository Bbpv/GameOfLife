using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules.GameRules
{
    // When a live cell has fewer than two neighbours
    // Then this cell dies
    public class RuleUnderpopulation : Rule
    {
        public override void Execute(GameState state, int x, int y)
        {
            if (state.GetBoardSpaceState(x, y))
            {
                if (state.GetNumberOfNeighbours(x, y) < 2)
                {
                    state.SetBoardSpaceState(x, y, false);
                }
            }
        }
    }
}
