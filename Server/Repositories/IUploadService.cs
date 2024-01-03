using Azure.Storage.Blobs;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Server.Repositories
{
    public interface IUploadService
    {
        Task<string> UploadSingleFile(IFormFile file, string FolderName);
        Task<bool> DeleteSingleFile(string FileName, string FolderName, int IsImageResume);
        //Task<bool> DeleteSingleResumeOrFile(string FileName, string sFolder);

        Task<ApplicationCommunicationDocumentAddDTO> UploadSingleFileWithRandomName(IFormFile IFile, string sFolder);
        Task<List<ApplicationCommunicationDocumentAddDTO>> UploadMultipleFileWithRandomName(List<IFormFile> IFile, string sFolder);

        Task<FileNameListModel> UploadFileWithRandomName(IFormFile IFile, string sFolder);
    }
}
