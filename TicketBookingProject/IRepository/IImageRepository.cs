// IImageRepository.cs
using System.Threading.Tasks;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.IRepository
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(string base64String);
        Task<Byte[]> GetImageDataAsync(int id);
        void AddImage(Image image);
        byte[] GetImageDataByPassengerId(int passengerId);
    }
}
