using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Project_Genetic
{
    class Tree:IComparable<Tree>
    {
        public Node Root { get; set; }
        private double x1;
        public double MSE;
        public Tree()
        {
            this.MSE=double.PositiveInfinity;
        }
        public void CalculateMSE(){
            double varince=0,eval;
            int count=Convert.ToInt32((Constants.MAX_RANGE_TEST-Constants.MIN_RANGE_TEST)/Constants.STEP)+1;
            int index=0;
            for (double i = Constants.MIN_RANGE_TEST; i <= Constants.MAX_RANGE_TEST; i+=Constants.STEP,index++)
            {
                eval=Evaluate(i);
                if(eval==Constants.ResultFunction[index]){
                    continue;
                }
                if(!double.IsNormal(eval)){
                    count--;
                    continue;
                }
                varince+=Math.Pow(eval-Constants.ResultFunction[index],2);
            }
            varince/=count;

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
        public override string ToString(){
            return recurText(Root);
        }
        private string recurText(Node node){
            if(node.dataType!=DataType.OPERATOR) return node.Data;
            else if(node.Right==null)
                return "("+node.Data+"("+recurText(node.Left)+")"+")";
            return "("+recurText(node.Left)+node.Data+recurText(node.Right)+")";
        }

        public int CompareTo(Tree? other)
        {
            return this.MSE.CompareTo(other.MSE);
        }
    }
}