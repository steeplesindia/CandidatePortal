using CandidatePortal.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateSkillDTO : CandidateSkillAddEditDTO
    {
        public SkillDTO Skill { get; set; }
        public SkillLevelDTO SkillLevel { get; set; }

    }
}
