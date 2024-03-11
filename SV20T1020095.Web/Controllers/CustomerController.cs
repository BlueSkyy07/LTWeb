using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SV20T1020095.BusinessLayers;
using SV20T1020095.DomainModels;
using SV20T1020095.Web.Models;
using System.CodeDom;
using System.Net.WebSockets;
namespace SV20T1020095.Web.Controllers
{
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 20;
        private const string CUSTOMER_SEARCH = "customer_search";// tên biến dùng để lưu trong session
        public IActionResult Index()
        {
            //Lấy đầu vào tìm kiếm hiện đang lưu lại trong session
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CUSTOMER_SEARCH);
            //Trường hợp trong session chưa có điều kiện thì tạo điều kiện mới
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CustomerSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            //Lưu lại điều kiện tìm kiếm vào trong session
            ApplicationContext.SetSessionData(CUSTOMER_SEARCH, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Customer model = new Customer()
            {
                CustomerId = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhập thông tin khách hàng";
            Customer? model = CommonDataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");

            }

            return View(model);
        }


        [HttpPost]

        public IActionResult Save(Customer data)
        {

            try
            {
                //kiểm soát đầu vào và đưa các thông báo lỗi và trong ModeState (nếu có)
                if(string.IsNullOrWhiteSpace(data.CustomerName)){
                    ModelState.AddModelError("CustomerName", "tên không được để trống");
                }
                if (string.IsNullOrWhiteSpace(data.ContactName))
                {
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                }
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    ModelState.AddModelError("Email", "Vui lòng nhập Email của khách");
                }
                if (string.IsNullOrWhiteSpace(data.Province))
                {
                    ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh thành");
                }
                //Thông qua thuộc tính IsVAlid của ModeState để kiểm tra xem có tồn tại lỗi hay không
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerId == 0 ? "Bổ sung khách hàng" : "Cập nhật thông tin khách hàng";
                    return View("Edit", data);
                }
                if (data.CustomerId == 0)
                {
                    int id = CommonDataService.AddCustomer(data);
                    if(id < 0)
                    {
                        ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng");
                        return View("Edit", data);
                    }
                }
                else
                {
                    bool result = CommonDataService.UpdateCustomer(data);
                    if (!result)
                    {
                        ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng với khách hàng");
                        return View("Edit", data);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Không thể lưu dữ liệu, vui lòng thử lại sau vài phút");
                return View("Edit", data);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedCustomer(id);
            return View(model);
        }
    }
}
