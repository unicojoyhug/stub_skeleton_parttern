using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace StubSkeletonPracticeTwoTypes.Common {
    public class Person : IXmlSerializable {
        private string name;
        private int age;

        public Person() {
        }

        public Person(string name, int age) {
            this.name = name;
            this.age = age;
        }

        public int CompareTo(Person other) {
            return this.age.CompareTo(other.age);
        }

        // obsolete, and never used by the .NET Framework
        XmlSchema IXmlSerializable.GetSchema() {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader) {
            XmlObjectDeserializer deserializer = new XmlObjectDeserializer(reader);

            name = deserializer.GetValueAsString("name");
            age = deserializer.GetValueAsInt("age");
        }

        public void WriteXml(XmlWriter writer) {
            XmlObjectSerializer serializer = new XmlObjectSerializer(this);

            serializer.AddField("name", name);
            serializer.AddField("age", age);

            serializer.WriteXml(writer);
        }

        public override string ToString() {
            return "[Person: name=\"" + name + "\", age=" + age +  "]";
        }

        
    }
}
