using AutoMapper;
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
        }
    }
}