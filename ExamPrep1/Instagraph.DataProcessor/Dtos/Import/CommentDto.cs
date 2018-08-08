using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos.Import
{
    [XmlType("comment")]
    public class CommentDto
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("post")]
        public CommentPostDto PostId { get; set; }
    }
}