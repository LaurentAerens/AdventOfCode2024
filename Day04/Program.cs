namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Challenge 1
            string[] lines = File.ReadAllLines("input.txt");
            // put the input in a 2D array
            char[,] input = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    input[i, j] = lines[i][j];
                }
            }
            int counter = 0;
            // check for word "xmas" in all directions
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 'X')
                    {
                        counter += CheckXmas(input, i, j);
                    }
                }
            }
            Console.WriteLine(counter);

            // Challenge 2
            counter = 0;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 'A')
                    {
                        counter += Checkxmas(input, i, j);
                    }
                }
            }
            Console.WriteLine(counter);
        }
        private static int CheckXmas(char[,] input, int i, int j)
        {
            int counter = 0;
            // check for "xmas" in all directions
            if (i + 3 < input.GetLength(0) && input[i + 1, j] == 'M' && input[i + 2, j] == 'A' && input[i + 3, j] == 'S')
            {
                counter++;
            }
            if (i - 3 >= 0 && input[i - 1, j] == 'M' && input[i - 2, j] == 'A' && input[i - 3, j] == 'S')
            {
                counter++;
            }
            if (j + 3 < input.GetLength(1) && input[i, j + 1] == 'M' && input[i, j + 2] == 'A' && input[i, j + 3] == 'S')
            {
                counter++;
            }
            if (j - 3 >= 0 && input[i, j - 1] == 'M' && input[i, j - 2] == 'A' && input[i, j - 3] == 'S')
            {
                counter++;
            }
            if (i + 3 < input.GetLength(0) && j + 3 < input.GetLength(1) && input[i + 1, j + 1] == 'M' && input[i + 2, j + 2] == 'A' && input[i + 3, j + 3] == 'S')
            {
                counter++;
            }
            if (i - 3 >= 0 && j - 3 >= 0 && input[i - 1, j - 1] == 'M' && input[i - 2, j - 2] == 'A' && input[i - 3, j - 3] == 'S')
            {
                counter++;
            }
            if (i + 3 < input.GetLength(0) && j - 3 >= 0 && input[i + 1, j - 1] == 'M' && input[i + 2, j - 2] == 'A' && input[i + 3, j - 3] == 'S')
            {
                counter++;
            }
            if (i - 3 >= 0 && j + 3 < input.GetLength(1) && input[i - 1, j + 1] == 'M' && input[i - 2, j + 2] == 'A' && input[i - 3, j + 3] == 'S')
            {
                counter++;
            }
            return counter;
        }
        private static int Checkxmas(char[,] input, int i, int j)
        {
            // check if the diagonals are 2 times the word "mas" if i,j is a A
            if (i + 1 < input.GetLength(0) && j + 1 < input.GetLength(1) && i - 1 >= 0 && j - 1 >= 0)
            {
                if (input[i + 1, j + 1] == 'M' && input[i - 1, j - 1] == 'S' && input[i + 1, j - 1] == 'M' && input[i - 1, j + 1] == 'S')
                {
                    return 1;
                }
                if (input[i + 1, j + 1] == 'S' && input[i - 1, j - 1] == 'M' && input[i + 1, j - 1] == 'M' && input[i - 1, j + 1] == 'S')
                {
                    return 1;
                }
                if (input[i + 1, j + 1] == 'M' && input[i - 1, j - 1] == 'S' && input[i + 1, j - 1] == 'S' && input[i - 1, j + 1] == 'M') 
                { 
                    return 1; 
                }
                if (input[i + 1, j + 1] == 'S' && input[i - 1, j - 1] == 'M' && input[i + 1, j - 1] == 'S' && input[i - 1, j + 1] == 'M') 
                { 
                    return 1;
                }
            }
            return 0;
        }
    }
}
