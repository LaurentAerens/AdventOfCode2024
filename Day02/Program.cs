namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //challenge 1
            string[] lines = File.ReadAllLines("input.txt");
            int safe = 0;
            
            foreach (string line in lines)
            {
                bool valid = false;
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                // convert parts to integers
                int[] numbers = parts.Select(int.Parse).ToArray();
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] + 1 == numbers[i + 1] || numbers[i] + 2 == numbers[i + 1] || numbers[i] + 3 == numbers[i + 1])
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    for (int i = 0; i < numbers.Length - 1; i++)
                    {
                        if (numbers[i] - 1 == numbers[i + 1] || numbers[i] - 2 == numbers[i + 1] || numbers[i] - 3 == numbers[i + 1])
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid)
                {
                    safe++;
                }
            }
            Console.WriteLine(safe);
            // challenge 2
            safe = 0;
            // the same as above but if 1 number is wrong we can remove it from the row and check again
            foreach (string line in lines)
            {
                bool valid = false;
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                // convert parts to integers
                int[] numbers = parts.Select(int.Parse).ToArray();
              
                if (IsValid(numbers))
                {
                    safe++;
                }
               
            }
            Console.WriteLine(safe);
        }
        public static bool IsValid(int[] numbers)
        {
            bool valid = true;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] + 1 == numbers[i + 1] || numbers[i] + 2 == numbers[i + 1] || numbers[i] + 3 == numbers[i + 1])
                {
                    valid = true;
                }
                else
                {
                    int[] newNumbers = new int[numbers.Length - 1];
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        if (j < i)
                        {
                            newNumbers[j] = numbers[j];
                        }
                        else if (j > i)
                        {
                            newNumbers[j - 1] = numbers[j];
                        }
                    }
                    valid = IsValid2(newNumbers);
                    if (!valid)
                    {
                        // try to remove the next number
                        newNumbers = new int[numbers.Length - 1];
                        for (int j = 0; j < numbers.Length; j++)
                        {
                            if (j < i + 1)
                            {
                                newNumbers[j] = numbers[j];
                            }
                            else if (j > i + 1)
                            {
                                newNumbers[j - 1] = numbers[j];
                            }
                        }
                        valid = IsValid2(newNumbers);
                    }
                    break;
                }
            }
            if (!valid)
            {
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] - 1 == numbers[i + 1] || numbers[i] - 2 == numbers[i + 1] || numbers[i] - 3 == numbers[i + 1])
                    {
                        valid = true;
                    }
                    else
                    {
                        int[] newNumbers = new int[numbers.Length - 1];
                        for (int j = 0; j < numbers.Length; j++)
                        {
                            if (j < i)
                            {
                                newNumbers[j] = numbers[j];
                            }
                            else if (j > i)
                            {
                                newNumbers[j - 1] = numbers[j];
                            }
                        }
                        valid = IsValid2(newNumbers);
                        if (!valid)
                        {
                            // try to remove the next number
                            newNumbers = new int[numbers.Length - 1];
                            for (int j = 0; j < numbers.Length; j++)
                            {
                                if (j < i + 1)
                                {
                                    newNumbers[j] = numbers[j];
                                }
                                else if (j > i + 1)
                                {
                                    newNumbers[j - 1] = numbers[j];
                                }
                            }
                            valid = IsValid2(newNumbers);
                        }
                        break;
                    }
                }
            }
            return valid;
        }
        public static bool IsValid2(int[] numbers)
        {
            bool valid = true;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] + 1 == numbers[i + 1] || numbers[i] + 2 == numbers[i + 1] || numbers[i] + 3 == numbers[i + 1])
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                    break;
                }
            }
            if (!valid)
            {
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] - 1 == numbers[i + 1] || numbers[i] - 2 == numbers[i + 1] || numbers[i] - 3 == numbers[i + 1])
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        break;
                    }
                }
            }
            return valid;
        }


    }
}
