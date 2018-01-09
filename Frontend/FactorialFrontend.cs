using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.Event;
using Akka.Routing;
using Sample.Cluster.Factorial.Protocol;

namespace Sample.Cluster.Factorial.Frontend
{
    public class FactorialFrontend : UntypedActor
    {
        int UpToN { get; set; }
        bool Repeat { get; set; }
        ILoggingAdapter Log { get; set; }
        IActorRef BackendRouter { get; set; }

        public FactorialFrontend(int upToN, bool repeat)
        {
            Log = Logging.GetLogger(Context.System, this);
            BackendRouter = Context.ActorOf(FromConfig.Instance.Props(), "factorialBackendRouter");
            UpToN = upToN;
            Repeat = repeat;
        }

        protected override void PreStart()
        {
            SendJob();
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(10));
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case FactorialResult result:
                    Log.Debug("{0}! = {1}", result.N, result.Factorial);
                    if (result.N == UpToN)
                    {
                        // if (Repeat)
                        // {
                        //     SendJob();
                        // }
                        // else
                        // {
                        Context.Stop(Self);
                        // }
                    }
                    break;
                case ReceiveTimeout _:
                    Log.Info("Timeout");
                    // SendJob();
                    break;
            }
        }

        private void SendJob()
        {
            Log.Info("Starting batch of factorials up to [{0}]", UpToN);
            for (var n = 1; n <= UpToN; n++)
            {
                BackendRouter.Tell(n);
            }
        }

        public static Props Props(int upToN, bool repeat) =>
            Akka.Actor.Props.Create(() => new FactorialFrontend(upToN, repeat));
    }
}