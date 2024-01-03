using Microsoft.AspNetCore.Http;

namespace CandidatePortal.Shared.PortalUtilities
{
    public interface IPortalUtilities
    {
        string GetHostName();
        string GetIP4Address();
        string GetIP6Address();
        string GetMACAddress();

        string GetUniqueFileName(IFormFile flUpload, string sWebrootPath, string sFolder, string sPrefix, string sSessionName, bool bThumbnails, int nWidth, int nHeight);

        void DeleteFileFromFolder(string sLocation);

        string HashSHA1(string sValue);

        string ErrorCodeByIndex(int nIndex);
        string RandomString(int size);
        
        string CalculateRelativeTime(DateTime myTime);

        bool DateCompare(DateTime startDate, DateTime endDate);
    }
}
