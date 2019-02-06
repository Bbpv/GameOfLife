using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public abstract class Rule
    {
        public abstract void Execute(GameState state, int x, int y);
    }
}
