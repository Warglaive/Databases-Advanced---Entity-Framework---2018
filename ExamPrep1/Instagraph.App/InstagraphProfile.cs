using AutoMapper;
using Instagraph.DataProcessor.Dtos.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(
                    des => des.ProfilePicture,
                    opt => opt.Ignore());

            //CreateMap<UserDto, User>()
            //    .ForMember(
            //        des => des.ProfilePictureId,
            //        opt => opt.Ignore());
            //CreateMap<UserDto, User>()
            //    .ForMember(
            //        des => des.Posts,
            //        opt => opt.Ignore());
            //CreateMap<UserDto, User>()
            //    .ForMember(
            //        des => des.UsersFollowing,
            //        opt => opt.Ignore());
            //CreateMap<UserDto, User>()
            //    .ForMember(
            //        des => des.Followers,
            //        opt => opt.Ignore());
            //CreateMap<UserDto, User>()
            //    .ForMember(
            //        des => des.Comments,
            //        opt => opt.Ignore());
        }
    }
}