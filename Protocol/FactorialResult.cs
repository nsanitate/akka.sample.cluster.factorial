using System;
using System.Numerics;

namespace Sample.Cluster.Factorial.Protocol
{
    public class FactorialResult
    {
        public int N { get; set; }
        public String Factorial { get; set; }

        public FactorialResult(int n, String factorial) {
            N = n;
            Factorial = factorial;
        }
    }
}