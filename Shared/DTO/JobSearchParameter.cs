using CandidatePortal.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Shared.DTO
{
    public class JobSearchParameter : QueryStringParameters
    {
        public JobSearchParameter()
        {
            OrderBy = "Code";
            //Filters = new List<string>();
        }

        //[FromQuery(Name = "sLocationSearch")]
        public string sLocationSearch { get; set; } = "India";

        //public List<string> Filters { get; set; }
    }
}
