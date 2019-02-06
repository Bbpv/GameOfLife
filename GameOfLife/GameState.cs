using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameState
    {
        private bool[,] board;
        private bool[,] nextBoardState;

        public int BoardSizeX
        {
            get { return board.GetLength(0); }
        }

        public int BoardSizeY
        {
            get
            {
                if (BoardSizeX == 0)
                {
                    return 0;
                }
                return board.GetLength(1);
            }
        }

        public GameState(int sizeX, int sizeY)
        {
            board = new bool[sizeX, sizeY];
            nextBoardState = new bool[sizeX, sizeY];
        }

        public GameState(bool[,] state)
        {
            LoadState(state);
        }

        public void LoadState(bool[,] state)
        {
            if (state == null)
            {
                return;
            }

            if (state.Length < 1)
            {
                return;
            }

            int sizeX = state.GetLength(0);
            int sizeY = state.GetLength(1);
            int totalGridSize = sizeX * sizeY;

            board = new bool[sizeX, sizeY];
            nextBoardState = new bool[sizeX, sizeY];

            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < totalGridSize; ++i)
            {
                board[currentX, currentY] = state[currentX, currentY];
                nextBoardState[currentX, currentY] = state[currentX, currentY];

                if ((currentY + 1) >= sizeY)
                {
                    currentY = 0;
                    ++currentX;
                }
                else
                {
                    ++currentY;
                }
            }
        }

        public int GetNumberOfNeighbours(int x, int y)
        {
            int count = 0;
            //X goes up and down
            //Y goes left and right
            
            //Bottom
            int bottomNeighbourX = x + 1;
            bool notAtBottom = (bottomNeighbourX < BoardSizeY);
            if (notAtBottom)
            {
                count += board[bottomNeighbourX, y] ? 1 : 0;
            }

            //Top
            int topNeighbourX = x - 1;
            bool notAtTop = (topNeighbourX >= 0);
            if (notAtTop)
            {
                count += board[topNeighbourX, y] ? 1 : 0;
            }

            //Left
            int leftNeighbourY = y - 1;
            bool notAtLeft = (leftNeighbourY >= 0);
            if (notAtLeft)
            {
                count += board[x, leftNeighbourY] ? 1 : 0;
            }

            //Right
            int rightNeighbourY = y + 1;
            bool notAtRight = (rightNeighbourY < BoardSizeX);
            if (notAtRight)
            {
                count += board[x, rightNeighbourY] ? 1 : 0;
            }

            //TopLeft
            if (notAtTop && notAtLeft)
            {
                count += board[topNeighbourX, leftNeighbourY] ? 1 : 0;
            }

            //TopRight
            if (notAtTop && notAtRight)
            {
                count += board[topNeighbourX, rightNeighbourY] ? 1 : 0;
            }

            //BottomLeft
            if (notAtBottom && notAtLeft)
            {
                count += board[bottomNeighbourX, leftNeighbourY] ? 1 : 0;
            }

            //BottomRight
            if (notAtBottom && notAtRight)
            {
                count += board[bottomNeighbourX, rightNeighbourY] ? 1 : 0;
            }

            return count;
        }


        public void SetBoardSpaceState(int x, int y, bool state)
        {
            //Any changes are for the next state so we should do that
            nextBoardState[x, y] = state;
        }

        public bool GetBoardSpaceState(int x, int y)
        {
            //Any value look ups must be from the current state
            return board[x, y];
        }

        public void EndFrame()
        {
            //Have to copy array manually
            //Because no deep cpy exists and assignments just links the two variables to the same mem
            int sizeX = BoardSizeX;
            int sizeY = BoardSizeY;
            int totalGridSize = sizeX * sizeY;
            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < totalGridSize; ++i)
            {
                board[currentX, currentY] = nextBoardState[currentX, currentY];

                if ((currentY + 1) >= sizeY)
                {
                    currentY = 0;
                    ++currentX;
                }
                else
                {
                    ++currentY;
                }
            }
        }
    }
}
