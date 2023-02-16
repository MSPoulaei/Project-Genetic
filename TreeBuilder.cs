namespace Project_Genetic
{
    class TreeBuilder
    {
        private Random random;
        private RandomGenerator randomGenerator;

        public TreeBuilder()
        {
            int seed = unchecked((int)DateTime.Now.Ticks);
            this.random = new Random(seed);
            this.randomGenerator = new RandomGenerator(random);
        }
        public void build(Tree tree)
        {
            tree.Root = buildNode(Constants.DEPTH);
        }
        private Node buildNode(int depth)
        {
            if (depth == 0)
            {
                if (random.Next(3) == 1)//Variable or Number
                    return new Node()//Number
                    {
                        Data = randomGenerator.randomDouble().ToString(),
                        dataType = DataType.NUMBER,
                        Left = null,
                        Right = null,
                    };
                else
                    return new Node()//Variable
                    {
                        Data = Constants.VARIABLES[random.Next(Constants.VARIABLES.Length)],
                        dataType = DataType.VARIABLE,
                        Left = null,
                        Right = null,
                    };
            }
            string op = randomGenerator.randomOperator();
            Node newNode = new Node()
            {
                Data = op,
                dataType = DataType.OPERATOR,
            };
            if (op == "^")
            {
                newNode.Left = buildNode(random.Next(0, depth));
                newNode.Right = buildNodePower(newNode);
                return newNode;
            }
            if(op=="/"){
                newNode.Left = buildNode(random.Next(0, depth));
                newNode.Right=new Node()
                {
                    Data = "+",
                    dataType = DataType.OPERATOR,
                    Left=buildNode(random.Next(0, depth)),
                    Right=new Node()//Number
                    {
                        Data = Constants.EPSILONSTR,
                        dataType = DataType.NUMBER,
                        Left = null,
                        Right = null,
                    }
                };
                return newNode;
            }
            newNode.Left = buildNode(random.Next(0, depth));
            //unary operator works only with Left Node (Rigth will be null)
            newNode.Right = (Constants.UNARY_OPERATORS.Contains(op) ? null : buildNode(random.Next(0, depth)));
            return newNode;

        }
        private Node buildNodePower(Node parent)
        {
            return new Node()
            {
                Data = random.Next(1, 4).ToString(),
                dataType = DataType.NUMBER,
                Left = null,
                Right = null,
                // Parent = parent
            };
        }
        public void SwapChildren(ref Tree child1, ref Tree child2)
        {
            Node parent1, chosen1, parent2, chosen2;
            int counter1, counter2;
            (chosen1, parent1, counter1) = ChooseNode(ref child1);
            (chosen2, parent2, counter2) = ChooseNode(ref child2);
            int counterStuck=0;
            while (counterStuck++<50)
            {
                List<int> depths1 = new List<int>();
                List<int> depths2 = new List<int>();
                calculateDepth(chosen1, 0, ref depths1);
                calculateDepth(chosen2, 0, ref depths2);
                bool notOk = false;
                if (counter1 + depths2.Max() > Constants.DEPTH)
                {
                    (chosen1, parent1, counter1) = ChooseNode(ref child1);
                    notOk = true;
                }
                if (counter2 + depths1.Max() > Constants.DEPTH)
                {
                    (chosen2, parent2, counter2) = ChooseNode(ref child2);
                    depths1.Clear();
                    calculateDepth(chosen1, 0, ref depths1);
                    notOk = true;
                }
                if (!notOk) break;
            }
            bool Right1 = (parent1.Right == chosen1);
            bool Right2 = (parent2.Right == chosen2);
            if (Right1) parent1.Right = chosen2;
            else parent1.Left = chosen2;

            if (Right2) parent2.Right = chosen1;
            else parent2.Left = chosen1;

        }
        private (Node, Node, int) ChooseNode(ref Tree tree)
        {
            int counter = 0;
            Node current = tree.Root, previous = null;
            int randomChooser = random.Next(2);
            while (current.Left != null)
            {
                if (randomChooser == 0)
                {
                    if (current.Left == null) break;
                    previous = current;
                    current = current.Left;
                }
                else if (randomChooser == 1)
                {
                    if (current.Right == null)
                    {
                        if (previous == null)
                        {
                            previous = current;
                            current = current.Left;
                        }
                        break;
                    }
                    previous = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
                randomChooser = random.Next(0, 3 + (counter++));//0 left, 1 right, 2-inf choose current node
            }
            if (previous.Data == "^" && current == previous.Right)
            {
                return ChooseNode(ref tree);
            }

            return (current, previous, counter);
        }
        public bool DecideJahesh()
        {
            return random.Next(Constants.PROB_JAHESH + 1) == 0;
        }
        public bool ChooseTree()
        {
            return random.Next(2) == 0;
        }
        public void doJahesh(ref Tree tree)
        {
            Node chosen, parent;
            int counter;
            (chosen, parent, counter) = ChooseNode(ref tree);
            bool Right = (parent.Right == chosen);
            Node newNode = buildNode(random.Next(3));
            if (Right) parent.Right = newNode;
            else parent.Left = newNode;
        }
        public void calculateDepth(Node root, int counter, ref List<int> depths)
        {
            if (root == null) {depths.Add(counter); return;}
            calculateDepth(root.Left, counter + 1, ref depths);
            calculateDepth(root.Right, counter + 1, ref depths);
        }

    }
}
