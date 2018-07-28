using System.IO;
using System.Xml.Serialization;

namespace XMLDemo
{
    public class Program
    {
        public static void Main()
        {
            var lib = new Library
            {
                Name = "biblioteka deba",
                Id = 2
            };
            XmlSerializer serializer = new XmlSerializer(typeof(Library));

            using (var writer = new StreamWriter("library.xml"))
            {
                serializer.Serialize(writer, lib);
            }
        }
    }
}