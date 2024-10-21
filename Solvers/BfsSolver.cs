using _8pazzle_bfs_rbfs.Game;

namespace _8pazzle_bfs_rbfs.Solvers
{
    public class BfsSolver : Solver
    {
        public override void Solve(State state)
        {
            var visited = new HashSet<Board>();
            var queue = new Queue<State>();
            Iterations = 0;

            queue.Enqueue(state);
            visited.Add(state.CurrentBoard);

            while (queue.Count > 0)
            {
                Iterations++;

                if (queue.Count > this.NodesInMemory)
                {
                    this.NodesInMemory = queue.Count;
                }

                state = queue.Dequeue();

                if (IsGoalState(state))
                {
                    this.PrintResults(state, true);
                    break;
                }

                this.NodesExpanded++;

                var zeroXAndY = state.CurrentBoard.IndexOfZero();
                var zeroX = zeroXAndY.Item1;
                var zeroY = zeroXAndY.Item2;

                var children = this.GenerateChildrenStates(state, zeroX, zeroY);

                for (var i = children.Count - 1; i >= 0; i--)
                {
                    var currentChild = children[i];
                    if (!visited.Contains(currentChild.CurrentBoard))
                    {
                        queue.Enqueue(currentChild);
                        visited.Add(currentChild.CurrentBoard);

                        if (currentChild.SearchDepth > this.MaxRecursiveDepth)
                        {
                            this.MaxRecursiveDepth = currentChild.SearchDepth;
                        }
                    }
                }
            }
        }
    }
}
