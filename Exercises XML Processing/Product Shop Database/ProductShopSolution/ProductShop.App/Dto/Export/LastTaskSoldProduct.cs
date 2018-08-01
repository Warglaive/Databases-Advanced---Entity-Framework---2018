using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("sold-products")]
    public class LastTaskSoldProduct
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public LastTaskProduct[] Products { get; set; }
    }
}