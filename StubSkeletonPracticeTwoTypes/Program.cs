using StubSkeletonPracticeTwoTypes.Client;
using StubSkeletonPracticeTwoTypes.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes {
    class Program {
        static void Main(string[] args) {
            Skeleton skeleton = new Skeleton();
            skeleton.Start();

            Client.Client client = new Client.Client(new Stub());
            client.Execute();
        }
    }
}
