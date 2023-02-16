namespace Project_Genetic
{
    public abstract class Constants
    {
        //test
        public const int NO_FIRST_GENERATION=1000;
        public const int NO_ITERATIONS=100;
        public const int NO_EACH_GENERATION=1000;
        public const int NO_PARENTS=100;
        public const int NO_SAMPLES=10;
        public const int PROB_JAHESH=5;// P= 1 / PROB_JAHESH

        public const int MIN_RANGE_TEST=-100;
        public const int MAX_RANGE_TEST=100;
        
        public const double STEP=0.1;
        public const double EPSILON=0.00001;
        public const string EPSILONSTR="0.00001";
        public const double GOOD_MSE=0.001;


        //random
        public const int DEPTH=10;
        public const int MINVALUE=-20;
        public const int MAXVALUE=20;
        public static string[] VARIABLES={"x"};
        public static string[] OPERATORS={"+","-","*","/","^","sin","cos"};
        public static string[] UNARY_OPERATORS={"sin","cos"};

        // public const int NUMBEROFOPERATORS=4;
        public static double RealFunction(double x){
            // return x+12;
            // return Math.Tan(x);
            // return (x*x +3*x-Math.Sin(x))/(2*x+0.001);
            // return Math.Sin(x)/(x+0.001);
            // return x*x*x+x*x;
            // return 100*(x*x*x+x*x);
            // return (x*x*x+x*x)*Math.Tan(x);
            // return -100*x*x*x+2*x+100;
            // return 11000.5;
            // return 12*x-2462;
            // return Math.Tan(x);
            // return 1/(Math.Sin(x)+0.001);
            // return 1/(Math.Sin(Math.PI* x)+0.001);
            // return Math.Exp(x);
            return 100*x*x+0.001*x+10000;


            //chand zabeteii:
            // if(x<-50)
            //     return x*x;
            // if(x<50)
            //     return -x+10;
            // else
            //     return 2*x*x+20*x;
        }
        public static string RealFunctionString(){
            // return "x+12";
            // return "tan(x)";
            // return "\"((x^2+3*x-sin(x))/(2*x))\"";
            // return "\"(sin(x))/(x+0.001)\"";
            // return "\"x^3+x^2\"";
            // return "\"100*(x^3+x^2)\"";
            // return "\"(x^3+x^2)*tan(x)\"";
            // return "\"-100*x^3+2*x+100\"";
            // return"\"11000.5\"";
            // return"\"12*x-2462\"";
            // return"\"tan(x)\"";
            // return "\"1/(sin(x)+0.001)\"";
            // return "\"1/(sin(pi*x)+0.001)\"";
            // return "\"exp(x)\"";
            return "\"100*x^2+0.001*x+10000\"";

        }
        public static double[] ResultFunction;
        public static void CalculateResultsRealFunction(){
            ResultFunction=new double[Convert.ToInt32((MAX_RANGE_TEST-MIN_RANGE_TEST)/STEP)+1];
            int index=0;
            for (double i = MIN_RANGE_TEST; i <= MAX_RANGE_TEST; i+=STEP,index++)
            {
                ResultFunction[index]=RealFunction(i);
            }
        }
    }
}