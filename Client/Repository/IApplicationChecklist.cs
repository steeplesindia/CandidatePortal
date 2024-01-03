using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Client.Repository
{
    public interface IApplicationChecklist
    {
        #region Check List
        Task<ResponseStatusData> Add(ApplicationChecklistAddDTO item);
        Task<ResponseStatusData> Edit(ApplicationChecklistAddDTO item);
        Task<ResponseStatusData> Delete(long Number);
        Task<ResponseStatusData> GetAppCommunicaionByAppsNumber(long NApplicationNumber);
        Task<ResponseStatusData> GetAppCommunicaionByNumber(long Id);
        #endregion

        #region Files
        Task<ApplicationChecklistDocumentAddDTO> UploadSingleFileWithRandomNameCheckList(MultipartFormDataContent content);
        Task<bool> DeleteImageFileAsync(ImageDeleteModel obj_ImageModel);
        Task<bool> DeleteAzureBlobFile(string FileName);
        Task<byte[]> Downloadchecklist(string FileName);

        #endregion
    }
}
