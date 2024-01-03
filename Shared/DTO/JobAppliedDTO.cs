
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class JobAppliedDTO : AXModelDTO
    {
        public string Code { get; set; }

        public string? Description { get; set; }

        public string? LongDescription { get; set; }

        public DateTime? OpeningDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public long CandidateNumber { get; set; }

        public long JobERPRecId { get; set; }

        public Int16? ApplicationStatus { get; set; }

        public DateTime? AppliedDate { get; set; }

        public long ApplicationNumber { get; set; }

        //Akshay 09-06-2022
        //public long Number { get; set; }
        public string? PortalLink { get; set; } = "";
        public string? Locations { get; set; } = "";
        public int? NoOfOpenings { get; set; }
        public DateTime? ApplicationDeadline { get; set; } = null;
        public Int16 Status { get; set; }
        public int? PublishedToPortal { get; set; }
        public byte[]? RowVersion { get; set; }
        public int IsSynced { get; set; }
        public int? CheckListPendingCount { get; set; }


        public JobAppliedDTO()
        {
            obj_applicationQuestionnaire = new ApplicationQuestionnaireDTO();
        }
        public ApplicationQuestionnaireDTO obj_applicationQuestionnaire { get; set; }
    }
}
