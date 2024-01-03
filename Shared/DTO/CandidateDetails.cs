

using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CandidatePortal.Shared.DTO
{   
    public class CandidateDetails 
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? ContactNo { get; set; }
        public string? ImageURL { get; set; }
        public long? Number { get; set; }

    }
}
