using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8pazzle_bfs_rbfs.Game
{
    public class State
    {
        private static int[,] goalMatrix = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 }
            };

        public State(Board currentBoard, State parent, string lastMove, int searchDepth)
        {
            this.CurrentBoard = currentBoard;
            this.Parent = parent;
            this.LastMove = lastMove;
            this.SearchDepth = searchDepth;
        }

        public Board CurrentBoard { get; set; }
        public State Parent { get; set; }
        public string LastMove { get; set; }
        public int SearchDepth { get; set; }
        public int PathCost { get; set; }
        public int FEvalute { get; set; }

        public State LeftMove(int x, int y)
        {
            if (y == 2)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y + 1];
            clonedState.CurrentBoard.Matrix[x, y + 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State RightMove(int x, int y)
        {
            if (y == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y - 1];
            clonedState.CurrentBoard.Matrix[x, y - 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State UpMove(int x, int y)
        {
            if (x == 2)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x + 1, y];
            clonedState.CurrentBoard.Matrix[x + 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State DownMove(int x, int y)
        {
            if (x == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x - 1, y];
            clonedState.CurrentBoard.Matrix[x - 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State Clone()
        {
            var newMatrix = new int[3, 3];

            for (int i = 0; i < this.CurrentBoard.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.CurrentBoard.Matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = this.CurrentBoard.Matrix[i, j];
                }
            }

            var clonedBoard = new Board(newMatrix);

            return new State(clonedBoard, null, this.LastMove, this.SearchDepth);
        }

        public int HeuristicCost()
        {
            /*var matrix = this.CurrentBoard.Matrix;
            int result = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int p = 0; p < 3; p++)
                        {
                            if (matrix[i, j] == 0 || goalMatrix[k, p] == 0) continue;
                            if (goalMatrix[k, p] == matrix[i, j])
                            {
                                result += Math.Abs(i - k) + Math.Abs(j - p);
                            }
                        }
                    }
                }
            }

            return result;*/

            int misplacedTiles = 0;

            for (int i = 0; i < this.CurrentBoard.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.CurrentBoard.Matrix.GetLength(1); j++)
                {
                    if (this.CurrentBoard.Matrix[i, j] != 0 && this.CurrentBoard.Matrix[i, j] != goalMatrix[i, j])
                    {
                        misplacedTiles++;
                    }
                }
            }

            return misplacedTiles;
        }

        public override int GetHashCode()
        {
            return this.CurrentBoard.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherState = (State)obj;

            return this.CurrentBoard.Equals(otherState.CurrentBoard);
        }
    }
}
