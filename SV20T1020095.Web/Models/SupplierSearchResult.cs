using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class SupplierSearchResult : BasePaginationResult
    {
        public List<Supplier> Data { get; set; }
    }
}
