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
            Random random = new Random();
            int[] arr = new int[difficulty * 2 + 1];
            bool hasDivision = false;
            //Generate Operands
            for (int i = 0; i < arr.Length; i += 2)
            {
                arr[i] = random.Next(24) + 1;
            }
            /*
                Operations Key:
                1 = Multiplication
                2 = Division
                3 = Addition
                4 = Subtraction
            */
            // Generates the operations on odd indexes
            List<string> possibleOperators = new List<string> { "placeholder", "*", "/", "+", "-" };

            for (int i = 1; i < arr.Length; i += 2)
            {
                string generatedOperator = "";
                int operators = -1;
                while (!selectedOperators.Contains(generatedOperator))
                {
                    do
                    {
                        operators = random.Next(0, 4) + 1;
                        generatedOperator = possibleOperators[operators];
                    } while (operators == 2 && hasDivision);
                }

                if(operators == 2)
                {
                    hasDivision = true;
                }
                arr[i] = operators;

            }

            if (checkEqu(arr))
            {
                arr = generation(difficulty, selectedOperators);
            }

            return arr;

        }

        public static bool checkEqu(int[] arr)
        {
            Random random = new Random();
            bool containsMLeft = false;
            bool containsMRight = false;
            for (int i = 1; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case 1:
                        //Makes multiplication no greater than 12
                        if (arr[i - 1] > 12 || arr[i + 1] > 12) { arr[i - 1] = random.Next(11) + 1; arr[i + 1] = random.Next(11) + 1; }
                        break;
                    case 2:
                        //Makes division no greater than 12
                        if (arr[i - 1] > 12 || arr[i + 1] > 12) { arr[i - 1] = random.Next(11) + 1; arr[i + 1] = random.Next(11) + 1; }
                        /**
                         * This algorithm splits the equation from the division sign and checks if multiplication would impact the divisor/dividend
                         * it then checks the equation to verify that once impacted numbers are changed, division still stays whole. 
                         * 
                         */
                        int[] leftSide = new int[i];
                        int[] rightSide = new int[arr.Length - i - 1];
                        for (int j = 0; j < i; j++)
                        {
                            leftSide[j] = arr[j];
                        }
                        for (int j = 0; j < arr.Length - i - 1; j++)
                        {
                            rightSide[j] = arr[j + i + 1];
                        }

                        if (!(i - 2 < 0))
                        {
                            if (arr[i - 2] == 1)
                            {
                                containsMLeft = true;
                            }
                        }

                        if (!(i + 3 > arr.Length))
                        {
                            if (arr[i + 2] == 1)
                            {
                                containsMRight = true;
                            }
                        }

                        if (!containsMLeft)
                        {
                            int[] temp = { arr[i - 1] };
                            leftSide = temp;
                        }
                        if (!containsMRight)
                        {
                            int[] temp = { arr[i + 1] };
                            rightSide = temp;
                        }
                        if (findSolution(leftSide) % findSolution(rightSide) != 0)
                        {
                            return true;
                        }
                        break;
                    case 4:
                        if (findSolution(arr) < 0) { return true; }
                            break;
                }
            }
            return false;
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
            int numOfmd = 0;
            int solution = 0;
            bool operationDone = false;
            while (arr.Length != 1)
            {
                for (int i = 1; i < arr.Length; i += 2)
                {
                    if (arr[i] == 1 || arr[i] == 2) { numOfmd++; }
                }
                for (int i = 1; i < arr.Length; i += 2)
                {
                    int temp = 0;
                    if (!mdDone && !operationDone)
                    {

                        switch (arr[i])
                        {
                            case 1: temp = arr[i - 1] * arr[i + 1]; operationDone = true; numOfmd--; break;
                            case 2: temp = arr[i - 1] / arr[i + 1]; operationDone = true; numOfmd--; break;

                            default: break;
                        }
                        if (numOfmd == 0)
                        {
                            mdDone = true;
                        }


                    }
                    if (mdDone && !operationDone)
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
                        tempArr[i - 1] = temp;
                        for (int j = 0; j < tempArr.Length; j++)
                        {
                            if (j < i - 1)
                            {
                                tempArr[j] = arr[j];
                            }
                            else if (j > i - 1)
                            {
                                tempArr[j] = arr[j + 2];
                            }
                        }
                        arr = tempArr;
                        operationDone = false;
                        i = -1;
                    }
                }
            }


            solution = arr[0];

            return solution;
        }

        public static int SolveEquation(int[] array)
        {
            
            int result = array[0];
            // Perform multiplication and division operations first
            for (int i = 1; i < array.Length; i += 2)
            {
                int operatorNumber = array[i];
                int operand = array[i + 1];

                if (operatorNumber == 1) // Multiplication
                {
                    result *= operand;
                }
                else if (operatorNumber == 2) // Division
                {
                    result /= operand;
                }
            }

            // Calculate the final result by performing addition and subtraction operations
            for (int i = 1; i < array.Length; i += 2)
            {
                int operatorNumber = array[i];
                int operand = array[i + 1];

                if (operatorNumber == 3) // Addition
                {
                    result += operand;
                }
                else if (operatorNumber == 4) // Subtraction
                {
                    result -= operand;
                }
            }

            return result;
           
        }
    }


}
