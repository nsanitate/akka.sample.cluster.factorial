using System;
using System.Numerics;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using Sample.Cluster.Factorial.Protocol;

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
            switch (message)
            {
                case int n:
                    Log.Info("I'm working on job {0}", n);
                    // Task.Run(() => Factorial(n))
                    // .PipeTo(Sender);
                    Sender.Tell(Factorial(n), Self);
                    break;
            }
        }

        private FactorialResult Factorial(int n)
        {
            var acc = BigInteger.One;
            for (int i = 1; i <= n; i++)
            {
                acc = BigInteger.Multiply(acc, new BigInteger(i));
            }
            return new FactorialResult(n, acc);
        }

        public static Props Props() =>
            Akka.Actor.Props.Create(() => new FactorialBackend());
    }
}