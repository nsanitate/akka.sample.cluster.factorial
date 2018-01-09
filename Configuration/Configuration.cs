using System;
using Akka.Configuration;

namespace Sample.Cluster.Factorial.Configuration
{
    public class Configuration
    {
        public static Config Fallback = ConfigurationFactory.ParseString(@"
				akka {
                    actor {
                        provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                        deployment {
                            /factorialFrontend/factorialBackendRouter = { 
                                router = round-robin-group
                                routees.paths = [""/user/factorialBackend""]
                                cluster {
                                    enabled = on
                                    use-role = backend
                                    allow-local-routees = off
                                }
                            }
                        }
                    }
                    remote {
                        dot-netty.tcp {
                            hostname = ""127.0.0.1""
                            port = 0
                        }
                    }
                    cluster {
                        min-nr-of-members = 3
                        role {
                            frontend.min-nr-of-members = 1
                            backend.min-nr-of-members = 2
                        }
                        seed-nodes = [
                            ""akka.tcp://ClusterSystem@127.0.0.1:2551""
                            ""akka.tcp://ClusterSystem@127.0.0.1:2552""
                        ]
                        auto-down-unreachable-after = 10s
                    }
                }
            ");
    }
}
