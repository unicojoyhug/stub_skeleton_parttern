using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StubSkeletonPracticeTwoTypes.Common {

    public class XmlObjectSerializer {
    private object subject;
    private Dictionary<string, object> fields;

    public XmlObjectSerializer( object subject ) {
      this.subject = subject;

      fields = new Dictionary<string, object>();
    }

    public void AddField( string name, object value ) {
      fields.Add( name, value );
    }

    public void WriteXml( XmlWriter writer ) {
      writer.WriteStartElement( subject.GetType().Name );
      {
        foreach ( string name in fields.Keys )
          writer.WriteElementString( name, fields[ name ].ToString() );
      }
      writer.WriteEndElement();
    }
  }
}
