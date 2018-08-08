using AutoMapper;
using Instagraph.DataProcessor.Dtos.Export;
using Instagraph.DataProcessor.Dtos.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<PictureDto, Picture>();

            CreateMap<UserDto, User>()
                .ForMember(
                    des => des.ProfilePicture,
                    opt => opt.Ignore());

            CreateMap<UserFollowerDto, UserFollower>()
                .ForMember(x => x.User,
                    opt => opt.Ignore())
                .ForMember(x => x.Follower,
                    opt => opt.Ignore());

            CreateMap<PostDto, Post>()
                .ForMember(x => x.User,
                    opt => opt.Ignore())
                .ForMember(x => x.Picture,
                    opt => opt.Ignore());

            CreateMap<CommentDto, Comment>()
                .ForMember(x => x.Content,
                    opt => opt.MapFrom(src => src.Content))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CommentPostDto, Comment>()
                .ForMember(x => x.PostId,
                    opt => opt.MapFrom(x => x.Id));


            CreateMap<Post, UncommentedPostDto>()
                .ForMember(p => p.Picture,
                    opt => opt.MapFrom(x => x.Picture.Path))
                .ForMember(u => u.User,
                    opt => opt.MapFrom(x => x.User.Username));

            //CreateMap<User, PopularUsersDto>()
            //    .ForMember(c => c.Followers,
            //        opt => opt.MapFrom(x => x.Followers.Count));

            CreateMap<User, PopularUsersDto>()
                .ForMember(dto => dto.Followers,
                    f => f.MapFrom(u => u.Followers.Count));
        }
    }
}