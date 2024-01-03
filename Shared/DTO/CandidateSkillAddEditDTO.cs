using CandidatePortal.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateSkillAddEditDTO : AXModelDTO
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }

        //[StringLength(30, ErrorMessage = "Skill can not be greater than 30 characters")]
        public long? SkillERPRecId { get; set; }

        public long? SkillLevelERPRecId { get; set; }

        //[StringLength(10, ErrorMessage = "Level Type can not be greater than 10 characters")]
        //public string LevelType { get; set; }

        //[StringLength(20, ErrorMessage = "Level can not be greater than 20 characters")]
        //public int Level { get; set; }

       // [StringLength(10, ErrorMessage = "Level Date can not be greater than 10 characters")]
        public DateTime? LevelDate { get; set; }

        //[MaxLength(20, ErrorMessage = "Year Experiance can not be greater than 20 characters")]
        public decimal? YearOfExperience { get; set; }

        public int? IsSynced { get; set; }
        public byte[]? RowVersion { get; set; }
        //public SkillDTO Skill { get; set; }
        //public SkillLevelDTO SkillLevel { get; set; }
        public string? SkillCode { get; set; }
        public string? SkillLevelCode { get; set; }



    }
}
