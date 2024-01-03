using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Client.Repository
{
    public interface IApplicationCommunication
    {
        #region Candidate
        Task<ResponseStatusData> AddCommunicationAsync(ApplicationCommunicationAddDTO item);
        Task<ResponseStatusData> EditCommunicationAsync(ApplicationCommunicationAddDTO item);
        Task<ResponseStatusData> DeleteCommunicationAsync(long Number);
        Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(long NApplicationNumber);
        Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(long Id);
        #endregion

        #region Master
        Task<ApplicationCommunicationDocumentAddDTO> UploadSingleFileWithRandomName(MultipartFormDataContent content);
        Task<bool> DeleteImageFileAsync(ImageDeleteModel obj_ImageModel);
        Task<bool> DeleteAzureBlobFile(string FileName);

        Task<ResponseStatusData> GetApplicationJobDetails(long ApplicationNumber);
        Task<byte[]> DownloadCommunication(string FileName);
        #endregion

    }
}
