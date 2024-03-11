using Microsoft.AspNetCore.Mvc;
using SV20T1020095.BusinessLayers;
using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(out rowCount, page, PAGE_SIZE, searchValue ?? "");
            var model = new Models.EmployeeSearchResult()
            {
                Page = page,
                PageSize = PAGE_SIZE,
                SearchValue = searchValue ?? "",
                RowCount = rowCount,
                Data = data
            };


            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";

            var model = new Employee()
            {
                EmployeeID = 0,
                BirthDate = new DateTime(1990, 1, 1),
                Photo = "nophoto.png"
            };

            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhập thông tin nhân viên";
            Employee? model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");

            }
            if (string.IsNullOrEmpty(model.Photo))
            {
                model.Photo = "nophoto.png";
            }

            return View(model);
        }
        [HttpPost]

        public IActionResult Save(Employee data, string birthDateInput, IFormFile? uploadPhoto)
        {
            //xử lý ngày sinh
            DateTime? birthDate = birthDateInput.ToDateTime();
            if (birthDate.HasValue)
            {
                data.BirthDate = birthDate.Value;
            }
            //xử lý ảnh upload (nếu có ảnh upload thì lưu ảnh và ghi gán lại tên file ảnh mới cho employee)
            if (uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}"; // ten file
                string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images\employees"); // duong dan luu file
                string filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                data.Photo = fileName;
            }
            try
            {
                if (data.EmployeeID == 0)
                {
                    int id = CommonDataService.AddEmployee(data);
                }
                else
                {
                    bool result = CommonDataService.UpdateEmployee(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedEmployee(id);
            return View(model);
        }
    }
}
