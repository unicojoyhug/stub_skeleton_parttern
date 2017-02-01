using StubSkeletonPracticeTwoTypes.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes.Client {
    class Stub : ICalcaulator, IPersonChecker {
        public int Add(int x, int y) {
            return int.Parse((string)ExecuteMethodCall("Add", x, y));
        }

        public Person Oldest(Person p1, Person p2) {
            return (Person)ExecuteMethodCall("Oldest", p1, p2);
        }

        private object ExecuteMethodCall(string methodName, params object[] parameters) {
            CallSerializer serializer = new CallSerializer(methodName);

            foreach (object parameter in parameters)
                serializer.Add(parameter);

            string request = serializer.Marshal();

            using (TcpClient client = new TcpClient()) {
                Connect(client, 10.0);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(request);
                writer.Flush();

                System.Console.WriteLine("Request send: " + request);

                string reply = reader.ReadToEnd();

                System.Console.WriteLine("Reply received: " + reply);

                if (reply.StartsWith("<int>"))
                    return reply.Substring("<int>".Length, reply.Length - "<int></int>".Length);
                else
                    return Utils.DeserializeObject(reply);
            }

        }


        private void Connect(TcpClient client, double timeout) {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!client.Connected && watch.Elapsed.TotalSeconds < timeout) {
                try {
                    client.Connect(ConnectInfo.SKELETON_HOST, ConnectInfo.SKELETON_PORT);
                } catch (SocketException) {
                    // ignore connection failed
                    Thread.Sleep(100);
                }
            }

            watch.Stop();
        }
    }
}


