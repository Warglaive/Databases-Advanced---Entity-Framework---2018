using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ProductShop.App.Dto.Import
{
    [XmlType("category")]
    public class CategoryDto
    {
        [XmlElement("name")]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }
    }
}