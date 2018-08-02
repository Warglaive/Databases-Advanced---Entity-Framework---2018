using System.Xml.Serialization;

namespace App.Dtos
{
    [XmlType("supplier")]
    public class SuppliersDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}