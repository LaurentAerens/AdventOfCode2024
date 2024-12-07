using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string[,] playfield = new string[input.Length, input[0].Length];
            int playerX = 0;
            int playerY = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    playfield[i, j] = input[i][j].ToString();
                    if (playfield[i, j] == "^")
                    {
                        playerX = i;
                        playerY = j;
                    }
                }
            }
            string[,] playFieldWithEdge = new string[playfield.GetLength(0) + 2, playfield.GetLength(1) + 2];
            // add and edge of the character "E" to the playfield
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == playFieldWithEdge.GetLength(0) - 1 || j == playFieldWithEdge.GetLength(1) - 1)
                    {
                        playFieldWithEdge[i, j] = "E";
                    }
                    else
                    {
                        playFieldWithEdge[i, j] = playfield[i - 1, j - 1];
                    }
                }
            }
            // update the player coordinates now that the playfield has an edge
            playerX++;
            playerY++;
            // print the coordinates for part 2
            Console.WriteLine($"playerX: {playerX} playerY: {playerY}");
            // print the playfield with the edge for debugging
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    Console.Write(playFieldWithEdge[i, j]);
                }
                Console.WriteLine();
            }
            string currentSquare = "";
            string[] directions = new string[] { "Upp", "Right", "Down", "Left" };
            string currentDirection = "Upp";
            string nextSquare = "";

            while (currentSquare != "E")
            {
                // check the current direction
                switch (currentDirection)
                {
                    case "Upp":
                        Console.Write("Upp ");
                        Console.WriteLine($"playerX: {playerX} playerY: {playerY}");
                        nextSquare = playFieldWithEdge[playerX - 1, playerY];
                        if (nextSquare == "." || nextSquare == "X")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            playerX--;
                        }
                        else if (nextSquare == "#")
                        {
                            // change direction to right
                            currentDirection = "Right";
                        }
                        else if (nextSquare == "E")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            currentSquare = "E";
                            break;
                        }
                        break;
                    case "Right":
                        Console.Write("Right ");
                        Console.WriteLine($"playerX: {playerX} playerY: {playerY}");
                        nextSquare = playFieldWithEdge[playerX, playerY + 1];
                        if (nextSquare == "." || nextSquare == "X")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            playerY++;
                        }
                        else if (nextSquare == "#")
                        {
                            // change direction to down
                            currentDirection = "Down";
                        }
                        else if (nextSquare == "E")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            currentSquare = "E";
                            break;
                        }
                        break;
                    case "Down":
                        Console.Write("Down ");
                        Console.WriteLine($"playerX: {playerX} playerY: {playerY}");
                        nextSquare = playFieldWithEdge[playerX + 1, playerY];
                        if (nextSquare == "." || nextSquare == "X")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            playerX++;
                        }
                        else if (nextSquare == "#")
                        {
                            // change direction to left
                            currentDirection = "Left";
                        }
                        else if (nextSquare == "E")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            currentSquare = "E";
                            break;
                        }
                        break;
                    case "Left":
                        Console.Write("Left ");
                        Console.WriteLine($"playerX: {playerX} playerY: {playerY}");
                        nextSquare = playFieldWithEdge[playerX, playerY - 1];
                        if (nextSquare == "." || nextSquare == "X")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            playerY--;
                        }
                        else if (nextSquare == "#")
                        {
                            // change direction to up
                            currentDirection = "Upp";
                        }
                        else if (nextSquare == "E")
                        {
                            // mark the current square as visited (X)
                            playFieldWithEdge[playerX, playerY] = "X";
                            currentSquare = "E";
                            break;
                        }
                        break;
                }
            }
            // print the playfield with the edge for debugging
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    Console.Write(playFieldWithEdge[i, j]);
                }
                Console.WriteLine();
            }
            // count the X's in the playfield
            int count = 0;
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    if (playFieldWithEdge[i, j] == "X")
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
            // part 2
            part2();

        }
        static void part2()
        {
            string[] input = File.ReadAllLines("input.txt");
            string[,] playfield = new string[input.Length, input[0].Length];
            int playerX = 0;
            int playerY = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    playfield[i, j] = input[i][j].ToString();
                    if (playfield[i, j] == "^")
                    {
                        playerX = i;
                        playerY = j;
                    }
                }
            }
            string[,] playFieldWithEdge = new string[playfield.GetLength(0) + 2, playfield.GetLength(1) + 2];
            // add and edge of the character "E" to the playfield
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == playFieldWithEdge.GetLength(0) - 1 || j == playFieldWithEdge.GetLength(1) - 1)
                    {
                        playFieldWithEdge[i, j] = "E";
                    }
                    else
                    {
                        playFieldWithEdge[i, j] = playfield[i - 1, j - 1];
                    }
                }
            }
            // get the orginal playfield with the edge
            string[,] OriginalPlayFieldWithEdge = new string[playFieldWithEdge.GetLength(0), playFieldWithEdge.GetLength(1)];
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    OriginalPlayFieldWithEdge[i, j] = playFieldWithEdge[i, j];
                }
            }


            int count = 0;
            for (int i = 0; i < playFieldWithEdge.GetLength(0); i++)
            {
                for (int j = 0; j < playFieldWithEdge.GetLength(1); j++)
                {
                    
                    if (playFieldWithEdge[i, j] != "#"  && playFieldWithEdge[i, j] != "^")
                    {
                        playFieldWithEdge[i, j] = "#";
                        if (CheckIfLoop(playFieldWithEdge))
                        {
                            count++;
                        }   
                        Console.WriteLine(count);
                        // reset the playfield
                        for (int k = 0; k < playFieldWithEdge.GetLength(0); k++)
                        {
                            for (int l = 0; l < playFieldWithEdge.GetLength(1); l++)
                            {
                                playFieldWithEdge[k, l] = OriginalPlayFieldWithEdge[k, l];
                            }
                        }
                    }

                }
            }
            Console.WriteLine(count);
        }
        static bool CheckIfLoop(string[,] playFieldWithEdge)
        {
            string currentSquare = "";
            string[] directions = new string[] { "Upp", "Right", "Down", "Left" };
            string currentDirection = "Upp";
            string nextSquare = "";
            int playerX = 38;
            int playerY = 66;
            int counter = 0;
            while (currentSquare != "E")
            {
                if (counter == 100000)
                {
                    PlayfielToCsv(playFieldWithEdge);
                    Console.WriteLine("counter");
                }
                counter++;
                // get the currentsquare
                currentSquare = playFieldWithEdge[playerX, playerY];
                // check the current direction
                switch (currentDirection)
                {
                    case "Upp":
                        nextSquare = playFieldWithEdge[playerX - 1, playerY];
                        //Console.WriteLine($"up: playerX: {playerX} playerY: {playerY}");
                        //Console.WriteLine(nextSquare);
                        if (currentSquare.Contains("U"))
                        {
                            return true;
                        }
                        else if (nextSquare == "#")
                        {
                            // change direction to right
                            currentDirection = "Right";
                        }
                        else if (nextSquare == "E")
                        {
                            currentSquare = "E";
                            break;
                        }
                        else
                        {
                            if (nextSquare.Contains("U"))
                            {
                                return true;
                            }
                            // mark the current square as a sqaure we went up by adding U to the current square
                            playFieldWithEdge[playerX, playerY] += "U";
                            playerX--;

                        }
                        break;
                    case "Right":
                        nextSquare = playFieldWithEdge[playerX, playerY + 1];
                        //Console.WriteLine($"right: playerX: {playerX} playerY: {playerY}");
                        //Console.WriteLine(nextSquare);
                        if (currentSquare.Contains("R"))
                        {
                            return true;
                        }
                        else
                        if (nextSquare == "#")
                        {
                            // change direction to down
                            currentDirection = "Down";
                        }
                        else if (nextSquare == "E")
                        {
                            currentSquare = "E";
                            break;
                        }
                        else
                        {
                            if (nextSquare.Contains("R"))
                            {
                                return true;
                            }
                            // mark the current square as a sqaure we went up by adding U to the current square
                            playFieldWithEdge[playerX, playerY] += "R";
                            playerY++;
                        }
                        break;
                    case "Down":
                        nextSquare = playFieldWithEdge[playerX + 1, playerY];
                        //Console.WriteLine($"down: playerX: {playerX} playerY: {playerY}");
                        //Console.WriteLine(nextSquare);
                        if (currentSquare.Contains("d"))
                        {
                            return true;
                        }
                        else
                        if (nextSquare == "#")
                        {
                            // change direction to left
                            currentDirection = "Left";
                        }
                        else if (nextSquare == "E")
                        {
                            currentSquare = "E";
                            break;
                        }
                        else
                        {
                            if (nextSquare.Contains("D"))
                            {
                                return true;
                            }
                            // mark the current square as a sqaure we went up by adding U to the current square
                            playFieldWithEdge[playerX, playerY] += "D";
                            playerX++;
                        }
                        break;
                    case "Left":
                        nextSquare = playFieldWithEdge[playerX, playerY - 1];
                        //Console.WriteLine($"left: playerX: {playerX} playerY: {playerY}");
                        //Console.WriteLine(nextSquare);
                        if (currentSquare.Contains("L"))
                        {
                            return true;
                        }
                        else
                        if (nextSquare == "#")
                        {
                            // change direction to up
                            currentDirection = "Upp";
                        }
                        else if (nextSquare == "E")
                        {
                            currentSquare = "E";
                            break;
                        }
                        else
                        {
                            if (nextSquare.Contains("L"))
                            {
                                return true;
                            }
                            // mark the current square as a sqaure we went up by adding U to the current square
                            playFieldWithEdge[playerX, playerY] += "L";
                            playerY--;
                        }
                        break;
                }
            }

            return false;
        }
        static void PlayfielToCsv(string[,] playfield)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < playfield.GetLength(0); i++)
            {
                for (int j = 0; j < playfield.GetLength(1); j++)
                {
                    sb.Append(playfield[i, j]);
                    sb.Append(",");
                }
                sb.AppendLine();
            }
            File.WriteAllText("playfield.csv", sb.ToString());
        }
    }
}
