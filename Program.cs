using System;
using System.Linq;
using System.Collections.Generic;
using static Project_Genetic.Constants;
using System.Diagnostics;

namespace Project_Genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            DateTime start = DateTime.Now;
            Constants.CalculateResultsRealFunction();
            List<Tree> trees = new List<Tree>(NO_FIRST_GENERATION);
            TreeBuilder treeBuilder = new TreeBuilder();
            Console.WriteLine("creating first generation({0} trees)",NO_FIRST_GENERATION);
            // double min=double.MaxValue;
            // bool foundAtFirst=false;
            List<double> MSEs = new List<double>();
            for (int i = 0; i < NO_FIRST_GENERATION; i++)
            {
                Tree tree = new Tree();
                treeBuilder.build(tree);
                tree.CalculateMSE();
                trees.Add(tree);
                // if(double.IsNormal(tree.MSE) && tree.MSE<min){
                //     min = tree.MSE;
                // }
                // if(min<GOOD_MSE){
                //     foundAtFirst=true;
                //     break;
                // }
            }
            Console.WriteLine("first generation created successfully");
            trees = trees.Where(t => double.IsNormal(t.MSE)).ToList();
            trees.Sort();
            trees = trees.Take(NO_EACH_GENERATION).ToList();
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            int mseGood = NO_PARENTS * 3 / 4;
            int mseBad = NO_PARENTS - mseGood;
            // TreeBuilder treeBuilder = new TreeBuilder();
            // if(foundAtFirst) goto jump;
            System.Console.WriteLine("Genetic iterations started! no of iterations:{0}, no of parents:{1}, no of each generation:{2}",NO_ITERATIONS,NO_PARENTS,NO_EACH_GENERATION);
            for (int i = 0; i < NO_ITERATIONS; i++)//Genetic
            {
                Console.Write("\r"+Math.Round(100.0 * i / NO_ITERATIONS) + "% done!");
                for (int j = 0; j < NO_PARENTS; j++)
                {
                    //TODO: choose parents better
                    int randomI1, randomI2;
                    if (j < mseGood)
                    {
                        randomI1 = random.Next(mseGood);
                        randomI2 = random.Next(mseGood);
                    }
                    else
                    {
                        randomI1 = random.Next(NO_EACH_GENERATION);
                        randomI2 = random.Next(NO_EACH_GENERATION);
                    }
                    Tree treeParent1 = trees[randomI1], treeParent2 = trees[randomI2];
                    Tree treeChild1 = treeParent1.Clone(), treeChild2 = treeParent2.Clone();
                    //TODO: make the children

                    treeBuilder.SwapChildren(ref treeChild1, ref treeChild2);
                    //TODO: random number choose Jahesh
                    //TODO2: Jahesh not working
                    if (treeBuilder.DecideJahesh())
                    {
                        if (treeBuilder.ChooseTree())
                        {
                            treeBuilder.doJahesh(ref treeChild1);
                        }
                        else
                        {
                            treeBuilder.doJahesh(ref treeChild2);
                        }
                    }

                    treeChild1.CalculateMSE();
                    treeChild2.CalculateMSE();
                    if(!trees.Any(tr=>tr.MSE==treeChild1.MSE))
                        trees.Add(treeChild1);
                    if(!trees.Any(tr=>tr.MSE==treeChild2.MSE))
                        trees.Add(treeChild2);
                }
                trees = trees.Where(t => double.IsNormal(t.MSE)).ToList();
                trees.Sort();
                trees = trees.Take(NO_EACH_GENERATION).ToList();
                MSEs.Add(trees[0].MSE);
                if(trees[0].MSE<GOOD_MSE){
                    break;
                }
            }
        // jump:    
        //     if(foundAtFirst){
        //         trees = trees.Where(t => double.IsNormal(t.MSE)).ToList();
        //         trees.Sort();
        //         trees = trees.Take(NO_EACH_GENERATION).ToList();
                
        //     }
            System.Console.WriteLine("\n"+(DateTime.Now-start).ToHumanReadableString());
            runPlot(Constants.RealFunctionString());
            runPlotMSE(string.Format("[{0}]",string.Join(',',MSEs)));
            for (int i = 0; i < Math.Min(NO_SAMPLES,trees.Count); i++)
            {
                Console.Write((i + 1) + "th:");
                Console.Write(trees[i]);
                Console.WriteLine(" " + trees[i].MSE);
                if(i<1 || (i<3 && (trees[i].MSE<GOOD_MSE)))
                    runPlot(trees[i].ToString());
            }

        }
        static void runPlot(string func)
        {
            string file="\"C:\\Users\\Sadegh\\Desktop\\code\\Term5\\AI Abdi\\Project Genetic\\plot.py\"";
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";//@"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.1264.0_x64__qbz5n2kfra8p0\python.exe";
            start.Arguments =string.Format("{0} {1}",file,func);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
            }
        }
        static void runPlotMSE(string list)
        {
            string file="\"C:\\Users\\Sadegh\\Desktop\\code\\Term5\\AI Abdi\\Project Genetic\\plot mse.py\"";
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";//@"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.1264.0_x64__qbz5n2kfra8p0\python.exe";
            start.Arguments =string.Format("{0} {1}",file,list);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
            }
        }

    }
}