
//using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Shared.Helpers
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;

        //[FromQuery(Name = "PageNumber")]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        //[FromQuery(Name = "PageSize")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        //[FromQuery(Name = "OrderBy")]
        public string OrderBy { get; set; }

        //[FromQuery(Name = "SearchTerm")]
        public string? SearchTerm { get; set; }

    }
}
