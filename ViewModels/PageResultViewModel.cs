
using System.Collections.Generic;

namespace AspNetPagingApi.Controllers.ViewModels
{
    public class PageResultViewModel<T>
    {
        public int PageNumber { get; set; }        
        public int PageSize { get; set; }       
        public int TotalNumberOfPages { get; set; }       
        public int TotalNumberOfRecords { get; set; }        
        public IEnumerable<T> Results { get; set; }
    }
}