namespace _8pazzle_bfs_rbfs.Game
{
    public class StateGenerator
    {
        private static Random _random = new Random();
        private const int _empty = 0;

        public static int[,] GenerateSolvablePuzzle()
        {
            int[] values;

            do
            {
                values = GenerateRandomValues();
            }
            while (!IsSolvable(values));

            return ConvertTo2DArray(values);
        }

        private static int[] GenerateRandomValues()
        {
            int[] values = Enumerable.Range(0, 9).ToArray();
            values = values.OrderBy(x => _random.Next()).ToArray();
            return values;
        }

        private static bool IsSolvable(int[] values)
        {
            int countOfInversions = 0;

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (values[i] != _empty && values[j] != _empty && values[i] > values[j])
                    {
                        countOfInversions++;
                    }
                }
            }

            return countOfInversions % 2 == 0;
        }

        private static int[,] ConvertTo2DArray(int[] values)
        {
            int[,] puzzle = new int[3, 3];
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    puzzle[i, j] = values[index];
                    index++;
                }
            }
            return puzzle;
        }
    }
}
