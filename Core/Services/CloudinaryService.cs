using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DTO;
using Core.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = SixLabors.ImageSharp.Size;

namespace Core.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private static readonly HashSet<string> AllowedImageExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
        private static readonly HashSet<string> AllowedMimeTypes = new HashSet<string> { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/tiff" };

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public string GetImageUrl(string publicId)
        {
            return _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
        }

        public string GetTransformedImageUrl(string publicId, int width, int height)
        {
            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Width(width).Height(height).Crop("fill"))
                .BuildUrl(publicId);
        }

        public async Task<DeletionResult> DeleteFileAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<ImageUploadResult> UploadFileAsync(MediaFileUploadRequest file)
        {
            if (file == null || file.File.Length == 0 || !AllowedImageExtensions.Contains(Path.GetExtension(file.File.FileName).ToLowerInvariant()) || !AllowedMimeTypes.Contains(file.File.ContentType))
            {
                throw new ArgumentException("Invalid file type or MIME type.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.File.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using (var image = await Image.LoadAsync(memoryStream))
                {
                    int maxWidth = 800, maxHeight = 800;
                    image.Mutate(x => x.Resize(new ResizeOptions { Size = new Size(maxWidth, maxHeight), Mode = ResizeMode.Max }));

                    using (var resizedMemoryStream = new MemoryStream())
                    {
                        await image.SaveAsJpegAsync(resizedMemoryStream);
                        resizedMemoryStream.Position = 0;

                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.File.FileName, resizedMemoryStream),
                            UseFilename = true,
                            UniqueFilename = false,
                            Overwrite = true
                        };

                        return await _cloudinary.UploadAsync(uploadParams);
                    }
                }
            }
        }
    }

}
