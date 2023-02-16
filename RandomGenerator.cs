namespace Project_Genetic
{
    public class RandomGenerator
    {
        private Random random;
        public RandomGenerator(Random random)
        {
            this.random = random;
        }
        public double randomDouble(){
            double daraje=random.NextDouble();// 0.0 - 1.0
            int randomNumber=random.Next(Constants.MINVALUE,Constants.MAXVALUE);
            return Math.Round(daraje*randomNumber,1);
        }
        public string randomOperator(){
            return Constants.OPERATORS[random.Next(Constants.OPERATORS.Length)];
        }
        

    }
}