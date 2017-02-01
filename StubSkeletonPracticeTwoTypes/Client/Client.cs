using StubSkeletonPracticeTwoTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes.Client {
    class Client {
        private Stub stub;

        public Client(Stub stub) {
            this.stub = stub;
        }

        internal void Execute() {
            Console.WriteLine("\nFINAL ANSWER :  5 + 5 = " + stub.Add(5, 5) + "\n");
            Console.WriteLine("\nFINAL ANSWER : 4 + 5 = " + stub.Add(4, 5) + "\n");
            Console.WriteLine("\nFINAL ANSWER : 51 + 5 = " + stub.Add(51, 5) + "\n");
            Console.WriteLine("Oldest ANSWER : " + stub.Oldest(new Person("Dave", 5), new Person("Ava", 15)));
        }
    }
}
