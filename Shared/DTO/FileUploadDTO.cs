using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class FileUploadDTO
    {
        public IFormFile file { get; set; }
        public string FolderName { get; set; }
    }
}
