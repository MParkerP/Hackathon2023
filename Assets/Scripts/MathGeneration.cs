using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Mathbuds
{
    public class ExpandedMath
    {

        /*       THIS WAS USED BEFORE INPUTTED INTO UNITY SO THAT TESTING COULD OCCUR IN THE IDE :D 
        public static void Main(String[] args)
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

        public static int[] generation(int difficulty, List<string> selectedOperators)
        {

            int arrSize;
            Random rand = new Random();
            /*
                Sets size of equation deonoted by difficulty
                Each # refers to the amount of operations (ex: Difficulty 1 = 1 operation)
            */
            switch (difficulty)
            {
                case 1: arrSize = 3; break;
                case 2: arrSize = 5; break;
                case 3: arrSize = 7; break;
                case 4: arrSize = 9; break;
                default: arrSize = 0; break;
            }

            //Creates an array to store the equation. Even indexes are numbers, odd indexes are operations
            int[] arr = new int[arrSize];
            /*
                Operations Key:
                1 = Multiplication
                2 = Division
                3 = Addition
                4 = Subtraction
            */

            //Generates a random number between 1 and 26 every even index.
            for (int i = 0; i < arr.Length; i += 2)
            {
                arr[i] = rand.Next(25) + 1;
            }

            // Generates the operations on odd indexes
            List<string> possibleOperators = new List<string>{ "placeholder", "*", "/", "+", "-" };

            for (int i = 1; i < arr.Length; i += 2)
            {
                string generatedOperator = "";
                int aNum = 0;
                while (!selectedOperators.Contains(generatedOperator))
                {
                    aNum = rand.Next(0, 4) + 1;
                    generatedOperator = possibleOperators[aNum];
                }
                
                arr[i] = aNum;

            }

            //This verifies that difficult Multiplication (Numbers Higher than 12), Non-whole division, and negative numbers never occur
            //TODO- This is not scaled for more than one operation. Perhaps a for loop starting at i = 1 and i+=2 would fix this, but division needs reworked
            switch (arr[1])
            {
                case 1:
                    if (arr[0] > 12 || arr[2] > 12)
                    {
                        //if the numbers being multiplied are greater than 12, they are regenerated
                        arr[0] = rand.Next(12) + 1;
                        arr[2] = rand.Next(12) + 1;
                    }
                    break;
                case 2:
                    if (arr[0] > 12 || arr[2] > 12 || arr[0] == 0 || arr[2] == 0)
                    {
                        //If dividing numbers are greater than 12, or we are dividing by zero, regenerate numbers
                        arr[0] = rand.Next(12) + 1;
                        arr[2] = rand.Next(12) + 1;
                    }
                    break;
                case 4:
                    if (arr[2] > arr[0])
                    {
                        //Flips two numbers if they subtract to be a negative number
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

                    /*Note: This will not work with larger equations (if expanded with a for loop as described above the above switch statement)
                        Example: Given an equation 3 / 4 / 9
                        It would regenerate 3 and 4 until their division is a whole number. Lets say this new equation is now 10 / 2 / 9.
                        Then it would regenerate the 2 and the 9 until the division is a whole number. Lets say this turns out to be 10 / 3 / 6. 

                        The program would then be complete with an equation 10 / 3 / 6, which will not be a whole number when dividied. 

                        A solution to this could be hardcoding only one division operation in the equation, and that could be checked using a similar switch statement as above. 
                    */
                }
            }

            return arr;
        }

        public static int findSolution(int[] arr)
        {
            /*
                This algorithm takes the equation array as its input and reads from left to right. It scans for either a 1 or 2(multiplication or division) in the odd indexes to apply PEMDAS
                Once it confirms there is no more multiplication or division, it will then look for addition/subtraction
                When it reaches an operation, it will then perform the operation, and shrink the array.
                When the array reaches size = 1, arr[0] is the solution

                    Example: [3,1,2,3,5] (3 * 2 + 5)
                    It scans the array and finds multiplication, and then does the operation and shrinks the array
                    New array: [6,3,5]
                    Scans for multiplication/division
                    (not found)
                    Scans for addition/subtraction
                    Finds addition, performs the operation, and shrinks the array
                    New array: [11]
                    Now the array is of size = 1, 11 is the solution to the equation
            */
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
