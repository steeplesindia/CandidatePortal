
namespace CandidatePortal.Shared.Entities
{
    public class Job: AXModel
    {
        public Job()
        {
            Applications = new HashSet<Application>();
        }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? DataArea { get; set; }

        //public long Number { get; set; } 
        public string? PortalLink { get; set; }
        public string? Locations { get; set; }
        public int? NoOfOpenings { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public Int16? Status { get; set; }
        public int? PublishedToPortal { get; set; }
        public byte[]? RowVersion { get; set; }
        public int? IsSynced { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
