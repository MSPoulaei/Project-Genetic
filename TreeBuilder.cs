namespace Project_Genetic
{
    class TreeBuilder
    {
        private Tree tree;
        private Random random;
        private RandomGenerator randomGenerator;

        public TreeBuilder(Tree tree)
        {
            this.tree = tree;
            int seed = unchecked((int)DateTime.Now.Ticks);
            this.random = new Random(seed);
            this.randomGenerator = new RandomGenerator(random);
        }
        public void build()
        {
            tree.Root = new Node()
            {
                Data = randomGenerator.randomOperator(),
                dataType = DataType.OPERATOR,
                Left = buildNode(random.Next(0, Constants.DEPTH)),
                Right = buildNode(random.Next(0, Constants.DEPTH)),
            };
        }
        private Node buildNode(int depth)
        {
            if (depth == 0)
            {
                if (random.Next(0, 1) == 1)//Variable or Number
                    return new Node()//Number
                    {
                        Data = randomGenerator.randomDouble().ToString(),
                        dataType = DataType.NUMBER,
                        Left=null,
                        Right=null
                    };
                else
                    return new Node()//Variable
                    {
                        Data = Constants.VARIABLES[random.Next(Constants.VARIABLES.Length)],
                        dataType = DataType.VARIABLE,
                        Left=null,
                        Right=null
                    };
            }
            string op=randomGenerator.randomOperator();
            return new Node(){
                Data = op,
                dataType = DataType.OPERATOR,
                Left = buildNode(random.Next(0, depth-1)),
                //unary operator works only with Left Node (Rigth will be null)
                Right = (Constants.UNARYOPERATORS.Contains(op) ? null : buildNode(random.Next(0, depth-1))),
            };

        }

    }
}