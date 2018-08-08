using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos.Import
{
    public class CommentPostDto
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}