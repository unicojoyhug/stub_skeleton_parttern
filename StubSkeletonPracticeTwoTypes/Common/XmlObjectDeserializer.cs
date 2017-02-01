using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StubSkeletonPracticeTwoTypes.Common {

    public class XmlObjectDeserializer {
    private string className;
    private Dictionary<string, object> fields = new Dictionary<string,object>();

    public XmlObjectDeserializer( XmlReader reader ) {
      className = reader.Name;

      reader.ReadStartElement(); // <class-name>
      {
        while ( reader.IsStartElement() ) {
          string name = reader.Name;
          string value = reader.ReadElementContentAsString();

          fields.Add( name, value );
        }
      }
      reader.ReadEndElement(); // </class-name>
    }

    public string ClassName {
      get { return className; }
    }

    public bool GetValueAsBool( string name ) {
      return Boolean.Parse( GetValueAsString( name ) );
    }

    public int GetValueAsInt( string name ) {
      return Int32.Parse( GetValueAsString( name ) );
    }

    public string GetValueAsString( string name ) {
      return fields[ name ].ToString();
    }
  }
}
