using System.Xml.Serialization;

namespace App.Dtos
{
    [XmlType("part")]
    public class PartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("quantity")]
        public int Quantity { get; set; }

        [XmlAttribute("supplier_id")]
        public int SupplierId { get; set; }
    }
}