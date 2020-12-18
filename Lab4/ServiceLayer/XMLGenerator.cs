using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ServiceLayer
{
    public class XMLGenerator
    {
        /*
        public void CreateXML<T>(T obj, string path)
        {
            string x = ToXML(obj);
            using (var streamWriter = new StreamWriter(path)) streamWriter.Write(x);
        }*/

        public string ToXML<T>(T obj)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(stringwriter, obj);
                return stringwriter.ToString();
            }
        }

        public string ToXSD<T>(T obj)
        {
            string xml = ToXML(obj);
            XmlReader reader;

            using (var stringreader = new StringReader(xml)) reader = XmlReader.Create(stringreader);

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();

            schemaSet = schema.InferSchema(reader);

            string str = "";

            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stringWriter))
                    {
                        s.Write(writer);
                    }

                    str += stringWriter.ToString();
                }
            }

            return str;
        }
    }
}
