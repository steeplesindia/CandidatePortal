using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class ResponseStatusData
    {
        public object Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public long Id { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ErrorStatus
    {
        public static string Error = "Error";
        public static string Warning = "Warning";
        public static string Success = "Success";
    }
}
