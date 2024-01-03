namespace CandidatePortal.Client.Model
{
    public class ResponseModel
    {
        public string sStatus { get; set; }

        public int nStatusCode { get; set; }

        public string sMessage { get; set; }

        public int nID { get; set; }

        public object Data { get; set; }
    }
}
