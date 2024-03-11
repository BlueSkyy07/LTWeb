﻿using Microsoft.AspNetCore.Mvc;
using SV20T1020095.BusinessLayers;
using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Controllers
{
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            var model = new Models.SupplierSearchResult()
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
            ViewBag.Title = "Bổ sung nhà cung cấp";
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhập thông tin nhà cung cấp";
            Supplier? model = CommonDataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");

            }

            return View(model);
        }
        [HttpPost]

        public IActionResult Save(Supplier data)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                {
                    ModelState.AddModelError(nameof(data.SupplierName), "Vui lòng nhập tên nhà cung cấp");
                }
                if (string.IsNullOrWhiteSpace(data.ContactName))
                {
                    ModelState.AddModelError(nameof(data.ContactName), "Vui lòng nhập tên giao dịch");
                }
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    ModelState.AddModelError(nameof(data.Email), "Vui lòng nhập Email");
                }
                if (string.IsNullOrWhiteSpace(data.Province))
                {
                    ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh thành");
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }

                if (data.SupplierID == 0)
                {
                    int id = CommonDataService.AddSupplier(data);
                }
                else
                {
                    bool result = CommonDataService.UpdateSupplier(data);
                    if (!result)
                    {
                        ViewBag.Title = "Cập nhật nhà cung cấp";
                        ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng với nhà cung cấp");
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
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedSupplier(id);
            return View(model);
        }
    }
}
