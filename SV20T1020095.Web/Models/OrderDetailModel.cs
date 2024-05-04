using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class OrderDetailModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
