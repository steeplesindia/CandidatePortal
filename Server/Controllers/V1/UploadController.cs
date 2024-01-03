using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web;

namespace CandidatePortal.Server.Controllers.V1
{
    [Route("api/v{:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    //[Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _repo;
        private readonly IReturnResponseService _returnResponse;
        private readonly string _azureConnectionString;
        private CloudStorageAccount storageAccount;

        ReturnResponses resp = new ReturnResponses();

        public UploadController(IUploadService _upRepo, IReturnResponseService returnResponse, IConfiguration configuration)
        {
            _repo = _upRepo;
            _returnResponse = returnResponse;
            _azureConnectionString = configuration.GetConnectionString("AzureBlobConnection");
        }

        //[HttpPost("UploadImage")]
        //public async Task<IActionResult> UploadImage()
        //{
        //    try
        //    {
        //        var FilesName = await _repo.UploadSingleFile(Request.Form.Files[0], "images");
        //        return Ok(FilesName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost("UploadResume")]
        //public async Task<IActionResult> UploadSingleFileResume()
        //{
        //    try
        //    {
        //        var FilesName = await _repo.UploadSingleFile(Request.Form.Files[0], "resume");
        //        return Ok(FilesName);
        //        //return resp.ReturnResponseData(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost("UploadFileWithRandomName")]
        public async Task<FileNameListModel> UploadFileWithRandomName(IFormFile IFile, string sFolderName)
        {
            try
            {
                return await _repo.UploadFileWithRandomName(IFile, sFolderName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Upload Candidate Profile pick
        [HttpPost("UploadCandidatePhoto")]
        public async Task<IActionResult> UploadCandidatePhoto()
        {
            return Ok(await UploadFileWithRandomName(Request.Form.Files[0], "images"));
        }

        //Upload Candidate Resume
        [HttpPost("UploadCandidateResume")]
        public async Task<IActionResult> UploadCandidateResume()
        {
            return Ok(await UploadFileWithRandomName(Request.Form.Files[0], "resume"));
        }

        [HttpPost("UploadSingleFileWithRandomName")]
        public async Task<IActionResult> UploadSingleFileWithRandomName()
        {
            try
            {
                ApplicationCommunicationDocumentAddDTO fileNameLists = new ApplicationCommunicationDocumentAddDTO();
                fileNameLists = await _repo.UploadSingleFileWithRandomName(Request.Form.Files[0], "communication");
                return Ok(fileNameLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Upload Check List
        [HttpPost("UploadSingleFileWithRandomNameCheckList")]
        public async Task<IActionResult> UploadSingleFileWithRandomNameCheckList()
        {
            try
            {
                ApplicationCommunicationDocumentAddDTO fileNameLists = new ApplicationCommunicationDocumentAddDTO();
                fileNameLists = await _repo.UploadSingleFileWithRandomName(Request.Form.Files[0], "checklist");
                return Ok(fileNameLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost("UploadMultipleFileWithRandomName")]
        public async Task<IActionResult> UploadMultipleFileWithRandomName()
        {
            try
            {
                List<ApplicationCommunicationDocumentAddDTO> fileNameLists = new List<ApplicationCommunicationDocumentAddDTO>();
                fileNameLists = await _repo.UploadMultipleFileWithRandomName(Request.Form.Files.ToList(), "resume");
                return Ok(fileNameLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpGet("Download/{name}")]
        public async Task<IActionResult> Download(string name)
        {

            var container = new BlobContainerClient(_azureConnectionString, "communication");
            var blob = container.GetBlobClient(name);
            if (await blob.ExistsAsync())
            {
                var a = await blob.DownloadAsync();
                return File(a.Value.Content, a.Value.ContentType, name);
            }
          
            return BadRequest();
        }

        [HttpGet("Downloadchecklist/{name}")]
        public async Task<IActionResult> Downloadchecklist(string name)
        {
            var container = new BlobContainerClient(_azureConnectionString, "checklist");
            var blob = container.GetBlobClient(name);
            if (await blob.ExistsAsync())
            {
                using (var ms = new MemoryStream())
                {
                    var a = await blob.DownloadAsync();
                    return File(a.Value.Content, a.Value.ContentType, name);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public byte[] DownloadFileFromBlob(string blobReferenceKey)
        {
            var storageAccount = CloudStorageAccount.Parse(_azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            // Get Blob Container  
            var container = blobClient.GetContainerReference("checklist");
            // Get reference to blob (binary content)  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobReferenceKey);
            using (var ms = new MemoryStream())
            {
                if (Convert.ToBoolean(blockBlob.ExistsAsync()))
                {
                    blockBlob.DownloadToStreamAsync(ms);
                }
                return ms.ToArray();
            }
        }

        [HttpGet("DownloadCommunication/{name}")]
        public async Task<IActionResult> DownloadCommunication(string name)
        {
            try
            {
                var container = new BlobContainerClient(_azureConnectionString, "communication");
                var blob = container.GetBlobClient(name);
                if (await blob.ExistsAsync())
                {
                    var a = await blob.DownloadAsync();
                    return File(a.Value.Content, a.Value.ContentType, name);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("DeleteFileAsync")]
        public async Task<IActionResult> DeleteFileAsync([FromBody] ImageDeleteModel obj_ImageModel)
        {
            try
            {
                bool IsDelete = await _repo.DeleteSingleFile(obj_ImageModel.ImageURL, obj_ImageModel.FolderName, obj_ImageModel.IsImageDocType);
                return Ok(IsDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest();
        }

        [HttpDelete("DeleteAzureBlobFile/{fileName}/{folderName}")]
        public async Task<IActionResult> DeleteAzureBlobFile(string fileName, string folderName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_azureConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = folderName;//_configuration.GetValue<string>("BlobContainerName");
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
            var blob = cloudBlobContainer.GetBlobReference(HttpUtility.UrlDecode("/" + folderName + "/" + fileName));
            if (await blob.DeleteIfExistsAsync())
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpDelete("DeleteAzureBlobFileNew/{fileName}")]
        public async Task<IActionResult> DeleteAzureBlobFileNew(string fileName)
        {
            string strContainerName = "checklist";//_configuration.GetValue<string>("BlobContainerName");
            BlobClient blobclient = new BlobClient(_azureConnectionString, strContainerName, fileName);
            blobclient.Delete();

            if (true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
