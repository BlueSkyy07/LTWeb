using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class ShipperSearchResult : BasePaginationResult
    {
        public List<Shipper> Data { get; set; }
    }
}
