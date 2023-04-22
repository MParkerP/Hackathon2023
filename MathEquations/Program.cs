// See https://aka.ms/new-console-template for more information
// Test Code :3
// Made by Ronald :(

namespace MathEquations
{
    class MainMath
    {
        private static int Num1;
        private static int Num2;
        private static int C = 0;


        public static void Main(String[] args) 
        {
            randEquation();
        }



        public static void randNum()
        {
            Random rand = new Random();
            int num1 = rand.Next(25);
            int num2 = rand.Next(25);

            Num1 = num1;
            Num2 = num2;
        }

        public static void randEquation()
        {
            randNum();
            Random rand = new Random();
            int index = rand.Next(4);
            
            switch(index)
            {
                case 0:
                    Console.WriteLine(Num1 + " + " + Num2 + " = " + addMethod(Num1, Num2));
                    break;
                case 1:
                    Console.WriteLine(Num1 + " - " + Num2 + " = " + subMethod(Num1, Num2));
                    break;
                case 2:
                    Console.WriteLine(Num1 + " * " + Num2 + " = " + multMethod(Num1, Num2));
                    break;
                case 3:
                    Console.WriteLine(Num1 + " / " + Num2 + " = " + divMethod(Num1, Num2));
                    break;
            }
            Console.WriteLine("\n" + C);
            
        }

        public static int addMethod(int a, int b)
        {
            C = a + b;
            return a + b;
        }

        public static int subMethod(int a, int b)
        {
            C = Math.Abs(a-b);
            return Math.Abs(a-b);
        }

        public static int multMethod(int a, int b)
        {
            C = a * b;
            return a * b;
        }

        public static int divMethod(int a, int b)
        {
            return 0;
        }

    }
}
