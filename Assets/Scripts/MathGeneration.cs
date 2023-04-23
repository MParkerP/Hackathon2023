using System;

namespace MathBuds
{
    public class ExpandedMath
    {

/*        public static void Main(String[] args)
        {
            int solution;
            int difficulty = 1;
            int[] eqSentence = generation(difficulty);

            for (int i = 0; i < eqSentence.Length; i++)
            {
                Console.WriteLine(eqSentence[i]);
            }

            *//*
            for(int i : eqSentence[]) {
                Console.WriteLine(i);
            }
            *//*

            solution = findSolution(eqSentence);
            Console.WriteLine("The Solution is: " + solution);
        }*/

        public static int[] generation(int difficulty)
        {
            int arrSize;
            Random rand = new Random();

            switch (difficulty)
            {
                case 1: arrSize = 3; break;
                case 2: arrSize = 5; break;
                case 3: arrSize = 7; break;
                case 4: arrSize = 9; break;
                default: arrSize = 0; break;
            }

            int[] arr = new int[arrSize];

            for (int i = 0; i < arr.Length; i += 2)
            {
                arr[i] = rand.Next(25) + 1;
            }

            // Generates the 
            for (int i = 1; i < arr.Length; i += 2)
            {
                arr[i] = rand.Next(0, 4) + 1;
            }

            // Check to see if multiplication or division integers are greater than 12. If so, make the numbers 12 or less
            switch (arr[1])
            {
                case 1:
                    if (arr[0] > 12 || arr[2] > 12)
                    {
                        arr[0] = rand.Next(13);
                        arr[2] = rand.Next(13);
                    }
                    break;
                case 2:
                    if (arr[0] > 12 || arr[2] > 12 || arr[0] == 0 || arr[2] == 0)
                    {
                        arr[0] = rand.Next(13);
                        arr[2] = rand.Next(13);
                    }
                    break;
                case 4:
                    if (arr[2] > arr[0])
                    {
                        int d = arr[2];
                        arr[2] = arr[0];
                        arr[0] = d;
                    }
                    break;
                default:
                    break;
            }


            // Check to see if the numbers for division equals an integer. If not, generate new numbers
            if (arr[1] == 2 && arr[0] % arr[2] != 0)
            {
                while (arr[1] == 2 && arr[0] % arr[2] != 0)
                {
                    arr[0] = rand.Next(12) + 1;
                    arr[2] = rand.Next(12) + 1;
                }
            }

            return arr;
        }


        public static int findSolution(int[] arr)
        {
            bool mdDone = false;
            int solution = 0;
            bool operationDone = false;
            for (int i = 1; i < arr.Length; i += 2)
            {
                int temp = 0;
                if (!mdDone)
                {
                    if (arr[i] == 1 || arr[i] == 2)
                    {
                        switch (arr[i])
                        {
                            case 1: temp = arr[i - 1] * arr[i + 1]; operationDone = true; break;
                            case 2: temp = arr[i - 1] / arr[i + 1]; operationDone = true; break;

                            default: break;
                        }
                    }
                    else { mdDone = true; }
                }
                if (mdDone)
                {
                    switch (arr[i])
                    {
                        case 3: temp = arr[i - 1] + arr[i + 1]; operationDone = true; break;
                        case 4: temp = arr[i - 1] - arr[i + 1]; operationDone = true; break;
                        default: break;
                    }
                }

                if (operationDone)
                {
                    int[] tempArr = new int[arr.Length - 2];
                    int count = 1;
                    tempArr[0] = temp;
                    for (int j = 3; j < arr.Length; j++)
                    {
                        tempArr[count] = arr[j];
                        count++;
                    }
                    arr = tempArr;

                }
            }


            solution = arr[0];



            return solution;
        }



    }
}