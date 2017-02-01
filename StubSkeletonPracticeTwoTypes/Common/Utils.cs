using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StubSkeletonPracticeTwoTypes.Common {
    public static class Utils {

        public static object DeserializeObject(string xml) {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(new StringReader(xml), settings)) {
                reader.MoveToContent();

                return DeserializeObject(reader);
            }
        }

        public static IXmlSerializable DeserializeObject(XmlReader reader) {
            string className = reader.Name;

            // http://stackoverflow.com/questions/1825147/type-gettypenamespace-a-b-classname-returns-null
            Type type = Type.GetType("StubSkeletonPracticeTwoTypes.Common." + className );

            IXmlSerializable obj = (IXmlSerializable)Activator.CreateInstance(type);
            obj.ReadXml(reader);

            return obj;
        }
    }
}
