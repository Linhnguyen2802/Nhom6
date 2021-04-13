using Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        //
        // GET: /Admin/ProductCategory/
        OnlineShopDbContext db = new OnlineShopDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Select(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet] //  truyen viewbag len view them san pham
        public ActionResult Create()
        {
            SetViewBag();
            return View("Create");
        }



        [HttpPost] // them san pham
        public ActionResult Create(ProductCategory pdcategory)
        {
            if (pdcategory.Name != null)
            {
                if (ModelState.IsValid)
                {
                    var dao = new ProductCategoryDao();
                    pdcategory.MetaTitle = pdcategory.MetaTitle;

                    pdcategory.CreatedDate = DateTime.Now;
                    var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                    pdcategory.CreatedBy = session.UserName;
                    pdcategory.MetaKeywords = pdcategory.MetaTitle;
                    pdcategory.MetaDescriptions = pdcategory.MetaTitle;

                    long id = dao.Insert(pdcategory);
                    if (id > 0)
                    {

                     //   SetAlert("Thêm loại sản phẩm thành công", "success");
                        SetAlert(StaticResources.Resources.Addsuccessfulproductcategories, "success");

                        return RedirectToAction("Select", "ProductCategory");
                    }
                    else
                    {
                      //  ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                        ModelState.AddModelError("", StaticResources.Resources.Addproducttypefailed);
                    }
                }

            }
            SetViewBag();
            // SetAlert("Thêm sản phẩm  không thành công", "success");
          //  SetAlert("Tên sản phẩm bắt buộc", "error");
            SetAlert(StaticResources.Resources.Requiredproductname, "error");
            return View("Create");
        }


        public ActionResult Edit(int id)
        {
            SetViewBag();
            var pdproduct = new ProductCategoryDao().ViewDetail(id);
            return View(pdproduct);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory pdproduct)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                pdproduct.Name = pdproduct.Name;
                pdproduct.MetaTitle = pdproduct.MetaTitle;
                pdproduct.CreatedBy = session.UserName;
                pdproduct.ModifiedBy = session.UserName;

                var result = dao.Update(pdproduct);
                if (result)
                {
                 //   SetAlert("Cập nhật loại sản phẩm thành công", "success");
                    SetAlert(StaticResources.Resources.Updateproducttypesuccessfully, "success");
                    return RedirectToAction("Select", "ProductCategory");
                }
                else
                {
                 //   ModelState.AddModelError("", "Cập nhật không thành công");
                    ModelState.AddModelError("", StaticResources.Resources.Updatefailed);
                }
            }
            SetViewBag();
            return View("Select");
        }


        [HttpDelete] // xoa san pham
        public ActionResult Delete(int id)
        {
            new ProductCategoryDao().Delete(id);
         //   SetAlert("Xoá thành công", "success");
            SetAlert(StaticResources.Resources.Deletesuccessful, "success");
            return RedirectToAction("Index");
        }

        // viewbag truyen dropdownlist category
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}
