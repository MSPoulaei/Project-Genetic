using System;
using System.Linq;
using System.Collections.Generic;

namespace Project_Genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree=new Tree();
            TreeBuilder treeBuilder = new TreeBuilder(tree);

            tree.Root=new Node(){
                Data="+",
                dataType=DataType.OPERATOR,
                Right=new Node(){
                    Data="123.425",
                    dataType=DataType.NUMBER,
                },
                Left=new Node(){
                    Data="35235.124",
                    dataType=DataType.NUMBER,
                }
            };
            Console.WriteLine(tree.Evaluate(1));
            Console.WriteLine(123.425+35235.124);
            
            

        }
    }
}