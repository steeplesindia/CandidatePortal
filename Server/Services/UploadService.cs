using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Microsoft.WindowsAzure.Storage;

namespace CandidatePortal.Server.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _azureConnectionString;
        //private readonly IReturnResponseService _returnResponse;
        private readonly BlobServiceClient _blobServiceClient; //Ashvin 22-06-2022
        private readonly PortalDbContext _context;

        public UploadService(IConfiguration configuration, BlobServiceClient blobServiceClient, PortalDbContext context) //, IReturnResponseService returnResponse //Ashvin 22-06-2022
        {
            _azureConnectionString = configuration.GetConnectionString("AzureBlobConnection");
            _blobServiceClient = blobServiceClient; //Ashvin 22-06-2022
            _context = context;
        }

        public async Task<string> UploadSingleFile(IFormFile IFile, string sFolder)
        {
            var container = new BlobContainerClient(_azureConnectionString, sFolder);
            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var blob = container.GetBlobClient(IFile.FileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            using (var fileStream = IFile.OpenReadStream())
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = IFile.ContentType });
            }

            if (blob.Uri.ToString() is not null)
            {
                return blob.Uri.ToString(); // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return ""; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
            }
        }


        public async Task<FileNameListModel> UploadFileWithRandomName(IFormFile IFile, string sFolder)
        {
            FileNameListModel obj_filemodel = new FileNameListModel();
            try
            {
                var container = new BlobContainerClient(_azureConnectionString, sFolder);
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                obj_filemodel.SRandomName = sFolder + "_" + Guid.NewGuid().ToString().Substring(0, 13) + Path.GetExtension(IFile.FileName);

                var blob = container.GetBlobClient(obj_filemodel.SRandomName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                using (var fileStream = IFile.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = IFile.ContentType });
                }

                if (blob.Uri.ToString() is not null)
                {
                    obj_filemodel.SOrgFileName = IFile.FileName;
                    obj_filemodel.SFilePath = blob.Uri.ToString();
                    return obj_filemodel; // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                }
                else
                {
                    return obj_filemodel; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicationCommunicationDocumentAddDTO> UploadSingleFileWithRandomName(IFormFile IFile, string sFolder)
        {
            ApplicationCommunicationDocumentAddDTO obj_filemodel = new ApplicationCommunicationDocumentAddDTO();
            try
            {
                var container = new BlobContainerClient(_azureConnectionString, sFolder);
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                obj_filemodel.DocumentRandomName = sFolder + "_" + Guid.NewGuid().ToString().Substring(0, 13) + Path.GetExtension(IFile.FileName);

                var blob = container.GetBlobClient(obj_filemodel.DocumentRandomName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                using (var fileStream = IFile.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = IFile.ContentType });
                }

                if (blob.Uri.ToString() is not null)
                {
                    obj_filemodel.DocumentName = IFile.FileName;
                    obj_filemodel.DocumentPath = blob.Uri.ToString();
                    return obj_filemodel; // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                }
                else
                {
                    return obj_filemodel; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ApplicationCommunicationDocumentAddDTO>> UploadMultipleFileWithRandomName(List<IFormFile> IFile, string sFolder)
        {
            List<ApplicationCommunicationDocumentAddDTO> obj_filemodel = new List<ApplicationCommunicationDocumentAddDTO>();

            foreach (var itemfile in IFile)
            {
                var SRandomName = "";
                var container = new BlobContainerClient(_azureConnectionString, sFolder);
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                SRandomName = sFolder + "_" + Guid.NewGuid().ToString().Substring(0, 13) + Path.GetExtension(itemfile.FileName);

                var blob = container.GetBlobClient(SRandomName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                using (var fileStream = itemfile.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = itemfile.ContentType });
                }

                if (blob.Uri.ToString() is not null)
                {
                    obj_filemodel.Add(new ApplicationCommunicationDocumentAddDTO
                    {
                        DocumentRandomName = SRandomName,
                        DocumentName = itemfile.FileName,
                        DocumentPath = blob.Uri.ToString(),
                    });
                }
                else
                {
                    obj_filemodel.Add(new ApplicationCommunicationDocumentAddDTO
                    {
                        DocumentRandomName = "",
                        DocumentName = "",
                        DocumentPath = "",
                    });
                }
            }
            return obj_filemodel; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
        }

        public async Task<bool> DeleteSingleFile(string FileName, string sFolder, int IsImageResume)
        {
            //var container = new BlobContainerClient(_azureConnectionString, sFolder);
            //var createResponse = await container.CreateIfNotExistsAsync();
            //if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
            //    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            //var blob = container.GetBlobClient(FileName);

            //if (await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots))
            //{
            //    return false; // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            //}
            //else
            //{
            //    return true; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
            //}

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_azureConnectionString);
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(sFolder);
            var blob = cloudBlobContainer.GetBlobReference(Path.GetFileName(FileName));
            var isdelete = await blob.DeleteIfExistsAsync();

            if (isdelete)
                await UpdateFileName(FileName, IsImageResume);

            return isdelete;
            //if (isdelete)
            //{
            //    return false; // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            //}
            //else
            //{
            //    return true; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
            //}
        }

        public async Task<bool> UpdateFileName(string FileName, int IsImageResume)
        {
            Candidate obj_Candidates = new Candidate();
            if (IsImageResume == 0)
            {
                obj_Candidates = _context.Candidates.Where(x => x.ImageURL == FileName).FirstOrDefault();
                if (obj_Candidates is not null)
                {
                    obj_Candidates.ImageURL = "";
                    obj_Candidates.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (IsImageResume == 0)
            {
                obj_Candidates = _context.Candidates.Where(x => x.ResumeURL == FileName).FirstOrDefault();
                if (obj_Candidates is not null)
                {
                    obj_Candidates.ResumeURL = "";
                    obj_Candidates.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (IsImageResume == 2)
            {
                ApplicationCommunicationDocument obj_ApplicationComm = new ApplicationCommunicationDocument();
                obj_ApplicationComm = _context.ApplicationCommunicationDocuments.Where(x => x.DocumentPath == FileName).FirstOrDefault();
                if (obj_ApplicationComm is not null)
                {
                    obj_ApplicationComm.DocumentPath = "";
                    obj_ApplicationComm.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (IsImageResume == 3)
            {
                ApplicationChecklistDocument ApplicationChecklistDoc = new ApplicationChecklistDocument();
                ApplicationChecklistDoc = _context.ApplicationChecklistDocuments.Where(x => x.DocumentPath == FileName).FirstOrDefault();
                if (ApplicationChecklistDoc is not null)
                {
                    ApplicationChecklistDoc.DocumentPath = "";
                    ApplicationChecklistDoc.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Ashvin 22-06-2022
        public async Task<byte[]> DownloadFileFromAzure(string FileName, string sContainerName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(sContainerName);

            var blobClient = blobContainer.GetBlobClient(FileName);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        //Ashvin 22-06-2022

        //public async Task<bool> DeleteSingleResumeOrFile(string FileName, string sFolder)
        //{
        //    var container = new BlobContainerClient(_azureConnectionString, sFolder);
        //    var createResponse = await container.CreateIfNotExistsAsync();
        //    if (createResponse is not null && createResponse.GetRawResponse().Status == 201)
        //        await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
        //    var blob = container.GetBlobClient(FileName);

        //    if (await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots))
        //    {
        //        return true; // _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
        //    }
        //    else
        //    {
        //        return false; //return _returnResponse.ReturnResponse(blob, 0, 13, ErrorStatus.Success, 200, true);
        //    }
        //}
    }
}
