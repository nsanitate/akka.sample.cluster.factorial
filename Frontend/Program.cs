using System;
using Akka.Actor;
using Akka.Configuration;

namespace Sample.Cluster.Factorial.Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            var upToN = 200;

            var config = ConfigurationFactory.ParseString("akka.cluster.roles = [frontend]")
                .WithFallback(Configuration.Configuration.Fallback);

            var system = ActorSystem.Create("ClusterSystem", config);
            system.Log.Info("Factorials will start when 2 backend members in the cluster.");

            var cluster = Akka.Cluster.Cluster.Get(system);

            cluster.RegisterOnMemberUp(() => system.ActorOf(FactorialFrontend.Props(upToN, false), "factorialFrontend"));

            cluster.RegisterOnMemberRemoved(() =>
            {
                system.RegisterOnTermination(() => System.Environment.Exit(0));
                system.Terminate();
            });
            
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
