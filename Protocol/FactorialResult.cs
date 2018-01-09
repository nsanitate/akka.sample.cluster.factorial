using System;
using System.Numerics;

namespace Sample.Cluster.Factorial.Protocol
{
    public class FactorialResult
    {
        public int N { get; set; }
        public BigInteger Factorial { get; set; }

        public FactorialResult(int n, BigInteger factorial) {
            N = n;
            Factorial = factorial;
        }
    }
}