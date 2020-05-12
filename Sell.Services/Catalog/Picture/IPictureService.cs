using Sell.Service.DTOs;
using Shop.Service.DTOs;
using System.Threading.Tasks;

namespace Sell.Services.Catalog.Picture
{
    public interface IPictureService
    {
        Task<bool> CheckExists(int ID);
        Task<PictureDTO> RegisterBase64PictureAsync(PictureUploadBase64DTO pictureUploadDTO);
        Task<PictureDTO> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO);
        Task<PictureDTO> SearchPictureByIdAsync(int id);
    }
}