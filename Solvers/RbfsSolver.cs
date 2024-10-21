using _8pazzle_bfs_rbfs.Game;

namespace _8pazzle_bfs_rbfs.Solvers
{
    public class RbfsSolver : Solver
    {
        private int currentFringeSize = 0;
        private int currentRecursiveDepth = 0;

        public RbfsSolver()
        {
            NodesInMemory = 0;
            MaxRecursiveDepth = 0;
        }

        public override void Solve(State initialState)
        {
            initialState.SearchDepth = 0;
            currentRecursiveDepth = 0;
            var result = RBFSRecursive(initialState, int.MaxValue);
            PrintResults(result.Item1, false);
        }

        public Tuple<State, int> RBFSRecursive(State currentState, double fLimit)
        {
            currentRecursiveDepth++;
            MaxRecursiveDepth = Math.Max(MaxRecursiveDepth, currentRecursiveDepth);

            if (IsGoalState(currentState))
            {
                return Tuple.Create(currentState, 0);
            }

            var successors = GenerateChildrenStates(currentState, currentState.CurrentBoard.IndexOfZero().Item1,
                currentState.CurrentBoard.IndexOfZero().Item2);

            NodesExpanded += successors.Count;

            if (successors.Count == 0)
            {
                return Tuple.Create<State, int>(null, int.MaxValue);
            }

            foreach (var s in successors)
            {
                s.FEvalute = Math.Max(s.PathCost + s.HeuristicCost(), currentState.FEvalute);
            }

            currentFringeSize += successors.Count;
            NodesInMemory = Math.Max(NodesInMemory, currentFringeSize);

            while (true)
            {
                successors = successors.OrderBy(s => s.FEvalute).ToList();
                var best = successors[0];
                var alternative = successors.Count > 1 ? successors[1].FEvalute : double.PositiveInfinity;

                if (best.FEvalute > fLimit)
                {
                    currentFringeSize--;
                    return Tuple.Create<State, int>(null, best.FEvalute);
                }

                var result = RBFSRecursive(best, Math.Min(fLimit, alternative));

                if (result.Item1 != null)
                {
                    return result;
                }

                best.FEvalute = result.Item2;
            }
        }
    }
}
