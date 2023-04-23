// See https://aka.ms/new-console-template for more information
// Test Code :3
// Made by Ronald :(

namespace MathEquations
{
    class MainMath
    {
        private static double Num1;
        private static double Num2;
        private static double C = 0;
        private static String plus = "+";
        private static String minus = "-";
        private static String divide = "/";
        private static String multiply = "*";


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
                    Console.WriteLine(Num1 + plus + Num2 + " = " + addMethod(Num1, Num2));
                    break;
                case 1:
                    Console.WriteLine(Num1 + minus + Num2 + " = " + subMethod(Num1, Num2));
                    break;
                case 2:
                    Console.WriteLine(Num1 + multiply + Num2 + " = " + multMethod(Num1, Num2));
                    break;
                case 3:
                    Console.WriteLine(Num1 + divide + Num2 + " = " + divMethod(Num1, Num2));
                    break;
            }
            
        }

        public static double addMethod(double a, double b)
        {
            C = a + b;
            return C;
        }

        public static double subMethod(double a, double b)
        {
            if(b > a)
            {
                Num1 = a;
                Num2 = b;
            }

            C = a-b;
            return C;
        }

        public static double multMethod(double a, double b)
        {
            C = a * b;
            return C;
        }

        public static double divMethod(double a, double b)
        {
            if(b > a)
            {
                double d = b;
                b = a;
                a = d;
            }

            C = Math.Round(a/b,2);
            return C;
        }

    }
}
