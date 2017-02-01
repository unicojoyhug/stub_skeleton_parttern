using StubSkeletonPracticeTwoTypes.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StubSkeletonPracticeTwoTypes.Server {
    class Skeleton {
        internal void Start() {
            new Thread(Run).Start();
        }

        private void Run() {
            TcpListener server = new TcpListener(IPAddress.Any, ConnectInfo.SKELETON_PORT);
            server.Start();

            Console.WriteLine("Server ready!");

            while (true)
                using (Socket connection = server.AcceptSocket()) {
                    NetworkStream stream = new NetworkStream(connection);
                    StreamReader reader = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);

                    string methodName;
                    List<object> parameters = new List<object>();

                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreWhitespace = true;
                    settings.IgnoreComments = true;

                    // parse request
                    using (XmlReader xmlReader = XmlReader.Create(reader, settings)) {
                        xmlReader.MoveToContent();

                        xmlReader.ReadStartElement("method-call");

                        methodName = xmlReader.ReadElementContentAsString();

                        xmlReader.ReadStartElement("parameters");

                        while (xmlReader.IsStartElement("parameter")) {
                            xmlReader.ReadStartElement(); // <parameter>

                            if (xmlReader.IsStartElement())
                                parameters.Add(Utils.DeserializeObject(xmlReader));

                            else
                                parameters.Add(xmlReader.ReadString());

                            xmlReader.ReadEndElement(); // </parameter>
                        }

                        xmlReader.ReadEndElement(); // </parameters>

                        //r.ReadEndElement(); // </method-call> note: if this end element is read => xmlReader blocks!
                    }

                    // test: write request to console
                    Console.Write("Request received: " + methodName + "(");
                    foreach (object parameter in parameters)
                        Console.Write(" " + parameter);
                    Console.WriteLine((parameters.Count > 0 ? " " : "") + ")");

                    // execute method call
                    object reply = ExecuteMethodCall(methodName, parameters);

                    // return reply to client stub
                    if (reply is IXmlSerializable)
                        ReturnXmlSerializableReply(writer, (IXmlSerializable)reply);
                    else
                        writer.Write("<int>" + reply + "</int>");

                    writer.Flush();

                    // test: write reply to console
                    Console.WriteLine("Reply send: " + reply);
                }
        }

        private void ReturnXmlSerializableReply(StreamWriter writer, IXmlSerializable reply) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "  ";
            settings.Indent = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                reply.WriteXml(xmlWriter);
        }

        private object ExecuteMethodCall(string methodName, List<object> parameters) {
            Calculator calculator = new Calculator();
            PersonChecker personChecker = new PersonChecker();
            object result = null;

            switch (methodName) {
                case "Add":
                    result = calculator.Add(int.Parse((string)parameters[0]), int.Parse((string)parameters[1]));
                    break;

                case "Oldest":
                    result = personChecker.Oldest((Person)parameters[0], (Person)parameters[1]);
                    break;
            }

            return result;
        }
    }
}

