using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("user")]
    public class ExportUsersDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public SoldProduct[] Product { get; set; }
    }
}