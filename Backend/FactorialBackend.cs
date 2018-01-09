using System;
using System.Numerics;
using Akka.Actor;
using Akka.Event;

namespace Sample.Cluster.Factorial.Backend
{
    public class FactorialBackend : UntypedActor
    {
        ILoggingAdapter Log { get; set; }

        public FactorialBackend()
        {
            Log = Logging.GetLogger(Context.System, this);
        }

        protected override void OnReceive(object message)
        { 
            Log.Info("I'm in: {0}", message);
            switch (message)
            {
                case int n:
                    // Task.Run(() => Factorial(n))
                    // .PipeTo(Sender);
                    break;
            }
        }

        // private FactorialResult Factorial(int n)
        // {
        //     var acc = BigInteger.One;
        //     for (int i = 1; i <= n; i++)
        //     {
        //         acc = BigInteger.Multiply(acc, new BigInteger(i));
        //     }
        //     return new FactorialResult(n, acc);
        // }

        public static Props Props() =>
            Akka.Actor.Props.Create(() => new FactorialBackend());
    }
}