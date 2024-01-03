using CandidatePortal.Shared.Helpers;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateParameters : QueryStringParameters
    {
        public CandidateParameters()
        {
            OrderBy = "FirstName";
        }
    }
}
