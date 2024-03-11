using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class CustomerSearchResult : BasePaginationResult
    {
        public List<Customer> Data { get; set; }
    }
}
