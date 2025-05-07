using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class MediaFile
    {
        public string FileName { get; set; }
        public Stream FileContent { get; set; }
        public string? ResourceType { get; set; }
    }

    public class UploadMediaResult
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
    }

    public class DeleteMediaResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class MediaFileUploadRequest
    {
        public IFormFile File { get; set; }
    }

}
