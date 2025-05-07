using CloudinaryDotNet.Actions;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICloudinaryService
    {
        string GetImageUrl(string publicId);
        string GetTransformedImageUrl(string publicId, int width, int height);
        Task<DeletionResult> DeleteFileAsync(string publicId);
        Task<ImageUploadResult> UploadFileAsync(MediaFileUploadRequest file);
    }
}
