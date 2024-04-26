// IImageRepository.cs
using System.Threading.Tasks;

namespace TicketBookingProject.IRepository
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(string base64String);
        Task<Byte[]> GetImageDataAsync(int id);
    }
}
