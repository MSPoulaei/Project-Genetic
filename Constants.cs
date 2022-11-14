namespace Project_Genetic
{
    public abstract class Constants
    {
        //test
        public const int MINRANGETEST=-1000;
        public const int MAXRANGETEST=1000;


        //random
        public const int DEPTH=4;
        public const int MINVALUE=-20;
        public const int MAXVALUE=20;
        public static string[] VARIABLES={"x1"};
        public static string[] OPERATORS={"+","-","*","/","^","sin","cos"};
        public static string[] UNARYOPERATORS={"sin","cos"};

        // public const int NUMBEROFOPERATORS=4;
        public static double RealFunction(double x){
            return x+12;
        }
    }
}