using Microsoft.AspNetCore.Components.Forms;

namespace CandidatePortal.Client.Model
{
    public class FileUploadSaveFile
    {

        public string SSchoolName { get; set; }
        public string SFromDate { get; set; }
        public string SToDate { get; set; }
        public string SDigree { get; set; }

        public string SFieldOfStudy { get; set; }
        public string SGrade { get; set; }

        public List<IBrowserFile> Files { get; set; }
        public List<IBrowserFile> selectedFiles { get; set; } = new List<IBrowserFile>();

        //public class FileData
        //{
        //    public IBrowserFile? SFile { get; set; }


        //    //public byte[] ImageBytes { get; set; }
        //    //public string FileName { get; set; }
        //    //public string FileType { get; set; }
        //    //public long FileSize { get; set; }
        //}
    }
}
