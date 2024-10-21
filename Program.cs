using _8pazzle_bfs_rbfs.Game;
using _8pazzle_bfs_rbfs.Solvers;
using _8pazzle_bfs_rbfs.Diagnostic;

namespace _8pazzle_bfs_rbfs.Main;

class Program
{
    static void Main()
    {
        int[,] initialState = StateGenerator.GenerateSolvablePuzzle();

        var board = new Board(initialState);

        Console.WriteLine("Initial state:\n");
        board.Print();

        var startingState = new State(board, null, null, 0);

        var bfsSolver = new BfsSolver();
        //var rbfsSolver = new RbfsSolver();
        var measurer = new PerformanceMeasurer();
        measurer.StartMeasuring();
        bfsSolver.Solve(startingState);
        //rbfsSolver.Solve(startingState);
        measurer.StopMeasuring();
        measurer.PrintResults();
    }
}