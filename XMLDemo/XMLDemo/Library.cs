using System.Xml.Serialization;

namespace XMLDemo
{
    [XmlRoot(ElementName = "library")]
    public class Library
    {
        public Library()
        {
            this.Books = new Book[]
            {
                new Book()
                {
                    Author="asen",
                    Title="zelen",
                    ISBN="123"
                },
                new Book()
                {
                    Author="slaveto",
                    Title="vtoraKniga",
                    ISBN="321"
                },
            };
        }
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("LibraryName")]
        public string Name { get; set; }

        [XmlArray(ElementName = "books")]
        public Book[] Books { get; set; }
    }
}