using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class EmployeeSearchResult : BasePaginationResult
    {
        public List<Employee> Data { get; set; }
    }
}
