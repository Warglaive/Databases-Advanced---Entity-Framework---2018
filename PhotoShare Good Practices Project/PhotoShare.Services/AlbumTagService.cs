using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class AlbumTagService : IAlbumTagService
    {
        private readonly PhotoShareContext context;

        public AlbumTagService(PhotoShareContext context)
        {
            this.context = context;
        }
        public AlbumTag AddTagTo(int albumId, int tagId)
        {
            return null;
        }
    }
}