// ImageController.cs
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBookingProject.IRepository;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageRepository _imageRepository;

    public ImageController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    #region GetImageById
    [HttpGet("GetImageById/{id}")]
    public async Task<IActionResult> GetImageById(int id)
    {
        try
        {
            var imageData = await _imageRepository.GetImageDataAsync(id);
            if (imageData == null)
            {
                return NotFound("Image not found");
            }

            return File(imageData, "image/jpeg"); // Assuming images are JPEGs
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving image: {ex.Message}");
        }
    }
    #endregion
    #region UploadingImage
    [HttpPost("UploadImage")]
    public async Task<ActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please provide a valid image file");
        }

        if (!IsImageFile(file))
        {
            return BadRequest("Please provide a valid image file (JPEG, PNG, GIF, BMP)");
        }

        try
        {
            string base64String;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                base64String = Convert.ToBase64String(memoryStream.ToArray());
            }
              
            var imageUrl = await _imageRepository.UploadImageAsync(base64String);
            return Content(imageUrl, "text/plain");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading image: {ex.Message}");
        }

    }
    #endregion
    
    private bool IsImageFile(IFormFile file)
    {
        var contentType = file.ContentType.ToLower();
        return contentType.StartsWith("image/");
    }
}
