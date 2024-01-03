using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class ImageDeleteModel
    {
        public string ImageURL { get; set; }
        public string FolderName { get; set; }
        public int IsImageDocType { get; set; }
        //0 : Candidate Profile Pick
        //1 : Resume
        //2 : Communication
        //3 : CheckList


    }
}
