// ImageRepository.cs
using System;
using System.Threading.Tasks;
using TicketBookingProject.Data.ApplicationDbContext;
using TicketBookingProject.Data.Models;
using TicketBookingProject.IRepository;
using TicketBookingProject.IRepositry;

namespace TicketBookingProject.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region GetImageById
        public async Task<byte[]> GetImageDataAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return null;
            }

            return image.Data;
        }
        #endregion
        #region UploadImage
        public async Task<string> UploadImageAsync(string base64String)
        {
            try
            {
                byte[] imageData = Convert.FromBase64String(base64String);
                var image = new Image { Data = imageData };
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();
                return $"data:image/jpeg;base64,{image.Image_Id}";
            }
            catch (FormatException ex)
            {
                throw new Exception("Invalid base64 string format.", ex);
            }
        }
        public void AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
        public byte[] GetImageDataByPassengerId(int passengerId)
        {
            var image = _context.Images.FirstOrDefault(i => i.P_Id == passengerId);
            return image?.Data;
        }

        #endregion
    }
}
