using _8pazzle_bfs_rbfs.Game;
using System.Text;

namespace _8pazzle_bfs_rbfs.Solvers
{
    public abstract class Solver
    {
        protected int[,] GoalState { get; set; }
        protected int NodesInMemory { get; set; }
        protected int MaxRecursiveDepth { get; set; }
        protected int NodesExpanded { get; set; }
        protected int Iterations { get; set; }

        public Solver()
        {
            this.GoalState = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0, } };
        }

        public abstract void Solve(State state);

        protected List<State> GenerateChildrenStates(State currentState, int x, int y)
        {
            var children = new List<State>();

            var rightState = currentState.RightMove(x, y);
            if (rightState != null)
            {
                rightState.LastMove = "right";
                rightState.Parent = currentState;
                rightState.SearchDepth++;
                rightState.PathCost = currentState.PathCost + 1;
                rightState.FEvalute = rightState.PathCost + rightState.HeuristicCost();
                children.Add(rightState);
            }

            var leftState = currentState.LeftMove(x, y);
            if (leftState != null)
            {
                leftState.LastMove = "left";
                leftState.Parent = currentState;
                leftState.SearchDepth++;
                leftState.PathCost = currentState.PathCost + 1;
                leftState.FEvalute = leftState.PathCost + leftState.HeuristicCost();
                children.Add(leftState);
            }

            var downState = currentState.DownMove(x, y);
            if (downState != null)
            {
                downState.LastMove = "down";
                downState.Parent = currentState;
                downState.SearchDepth++;
                downState.PathCost = currentState.PathCost + 1;
                downState.FEvalute = downState.PathCost + downState.HeuristicCost();
                children.Add(downState);
            }

            var upState = currentState.UpMove(x, y);
            if (upState != null)
            {
                upState.LastMove = "up";
                upState.Parent = currentState;
                upState.SearchDepth++;
                upState.PathCost = currentState.PathCost + 1;
                upState.FEvalute = upState.PathCost + upState.HeuristicCost();
                children.Add(upState);
            }

            return children;
        }

        public void PrintResults(State finalState, bool isIterativeMethod)
        {
            var stringPath = FindPath(finalState);
            var boardPath = GetGoalBoards(finalState);
            var pathToGoal = GetPathAsString(stringPath);
            var costOfPath = stringPath.Count;

            foreach (var b in boardPath)
            {
                b.Print();
            }

            Console.WriteLine($"Path to goal: {pathToGoal}");
            if (isIterativeMethod)
            {
                Console.WriteLine($"Iterations: {Iterations}");
            }
            else
            {
                Console.WriteLine($"Search depth: {MaxRecursiveDepth}");
            }
            Console.WriteLine($"Cost of path: {costOfPath}");
            Console.WriteLine($"Nodes expanded: {this.NodesExpanded}");
            Console.WriteLine($"Max nodes in memory: {this.NodesInMemory}");
        }

        private List<Board> GetGoalBoards(State state)
        {
            var goalBoards = new List<Board>();
            while (state.Parent != null)
            {
                goalBoards.Add(state.CurrentBoard);
                state = state.Parent;
            }
            
            goalBoards.Reverse();
            
            return goalBoards;
        }

        private List<string> FindPath(State state)
        {
            var path = new List<string>();
            while (state.Parent != null)
            {
                path.Add(state.LastMove);
                state = state.Parent;
            }

            return path;
        }

        private string GetPathAsString(List<string> path)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                sb.Append("'");
                sb.Append(path[i]);
                sb.Append("'");
                sb.Append(", ");
            }

            var pathToGoal = sb.ToString().TrimEnd(new[] { ',', ' ' });
            pathToGoal += "]";

            return pathToGoal;
        }

        protected bool IsGoalState(State state)
        {
            return state.CurrentBoard.IsEqual(this.GoalState);
        }
    }
}
