using System.Xml.Serialization;

namespace XMLDemo
{
    [XmlType("book")]
    public class Book
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "isbn")]
        public string ISBN { get; set; }
    }
}