using System;
using System.Linq;
using System.Collections.Generic;

namespace Project_Genetic
{
    class Tree
    {
        public Node Root { get; set; }
        private double x1;
        private double MSE=double.PositiveInfinity;
        public void CalculateMSE(){
            double varince=0;
            for (int i = Constants.MINRANGETEST; i <= Constants.MAXRANGETEST; i++)
            {
                varince+=Math.Pow(Evaluate(i)-Constants.RealFunction(i),2);
            }
            varince/=Constants.MAXRANGETEST-Constants.MAXRANGETEST;

            MSE=varince;
        }
        public double Evaluate(double x1){
            this.x1 =x1;
            return evaluate(Root);
        }
        private double evaluate(Node node){
            if(node==null) return double.NaN;
            switch (node.dataType)
            {
                case DataType.NUMBER:
                    return double.Parse(node.Data);
                case DataType.VARIABLE:
                    return x1;
                case DataType.OPERATOR:
                    double left=evaluate(node.Left);
                    double right=evaluate(node.Right);
                    switch (node.Data)
                    {
                        case "+":
                            return left+right;
                        case "-":
                            return left-right;
                        case "*":
                            return left*right;
                        case "/":
                            return left/right;
                        case "^":
                            return Math.Pow(left,right);
                        case "sin":
                            return Math.Sin(left);
                        case "cos":
                            return Math.Cos(left);
                    }
                break;
            }
            return double.NaN;
        }
        
    }
}