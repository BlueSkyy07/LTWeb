using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class CategorySearchResult : BasePaginationResult
    {
        public List<Category> Data { get; set; }
    }
}
