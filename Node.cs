using System;
using System.Linq;
using System.Collections.Generic;

namespace Project_Genetic
{
    class Node
    {
        public string Data { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }
        public DataType dataType { get; set; } 
    }
}