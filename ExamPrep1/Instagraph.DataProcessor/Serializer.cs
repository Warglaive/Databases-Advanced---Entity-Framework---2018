using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos.Export;
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
            var users = context
                .Users
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(i => i.Id)
                .ProjectTo<PopularUsersDto>()
                .ToList();

            var jsonProduct = JsonConvert.SerializeObject(users, Formatting.Indented);
            return jsonProduct;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            throw new NotImplementedException();
        }
    }
}
