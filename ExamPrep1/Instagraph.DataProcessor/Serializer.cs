using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos.Import;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentedPosts = context
                .Posts
                .Where(x => x.Comments.Any() == false)
                .OrderBy(x => x.Id)
                .ProjectTo<UncommentedPostDto>()
                .ToList();

            var jsonProduct = JsonConvert.SerializeObject(uncommentedPosts, Formatting.Indented);

            return jsonProduct;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            throw new NotImplementedException();
        }
    }
}
