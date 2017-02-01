using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StubSkeletonPracticeTwoTypes.Client {
    internal class CallSerializer {
        private string methodName;
        private List<object> parameters;

        internal CallSerializer(string methodName) {
            this.methodName = methodName;

            parameters = new List<object>();
        }

        internal void Add(object parameter) {
            parameters.Add(parameter);
        }

        internal string Marshal() {
            using (StringWriter stringWriter = new StringWriter()) {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.IndentChars = "  ";
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings)) {
                    writer.WriteStartDocument();
                    {
                        writer.WriteStartElement("method-call");
                        {
                            writer.WriteElementString("method-name", methodName);

                            writer.WriteStartElement("parameters");
                            {
                                foreach (object parameter in parameters) {
                                    writer.WriteStartElement("parameter");

                                    if (parameter is IXmlSerializable)
                                        ((IXmlSerializable)parameter).WriteXml(writer);
                                    else
                                        writer.WriteString(parameter.ToString());

                                    writer.WriteEndElement();
                                }
                            }
                            writer.WriteEndElement(); // </parameters>
                        }
                        writer.WriteEndElement(); // </method-call>
                    }
                    writer.WriteEndDocument();
                }

                return stringWriter.ToString();
            }
        }
    }
}

