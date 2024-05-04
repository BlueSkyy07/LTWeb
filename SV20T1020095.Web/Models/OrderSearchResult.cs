using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class OrderSearchResult : BasePaginationResutlt
    {
        public int Status { get; set; } = 0;
        public string TimeRange { get; set; } = "";
        public List<Order> Data { get; set; } = new List<Order>();
    }
}
