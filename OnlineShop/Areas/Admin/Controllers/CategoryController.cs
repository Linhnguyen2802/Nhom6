
using Model.Dao;
using Models.DAO;
using Models.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new CategoryDao().ViewDetail1(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();

                var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                category.Name = category.Name;
                category.DisplayOrder = category.DisplayOrder;
                category.CreatedBy = session.UserName;
                category.ModifiedBy = session.UserName;
                category.Status = category.Status;
                category.MetaTitle = category.MetaTitle;
                category.ParentID = category.ParentID;
                var result = dao.Update(category);
                if (result)
                {
                  //  SetAlert("Cập nhật loại tin tức thành công", "success");
                    SetAlert(StaticResources.Resources.Updatenewstypesuccessfully, "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                 //   ModelState.AddModelError("", "Cập nhật không thành công");
                    ModelState.AddModelError("", StaticResources.Resources.Updatefailed);
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (category.Name != null)
            {
                if (ModelState.IsValid)
                {
                    var dao = new CategoryDao();
                    var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                    category.Name = category.Name;
                    category.Status = category.Status;
                    category.DisplayOrder = category.DisplayOrder;
                    category.CreatedBy = session.UserName;
                    category.MetaTitle = category.MetaTitle;
                    long id = dao.Insert(category);
                    if (id > 0)
                    {

                        // SetAlert("Thêm loại tin tức thành công ", "success");
                        SetAlert(StaticResources.Resources.Addsuccessfulnewscategories, "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        //  ModelState.AddModelError("", "Thêm không thành công");
                        ModelState.AddModelError("", StaticResources.Resources.Addfailed);
                    }
                }

            }
            //SetViewBag();
            // SetAlert("Thêm sản phẩm  không thành công", "success");
            //SetAlert("Tên loại tin tức bắt buộc", "error");
            SetAlert(StaticResources.Resources.Nametypeofnewsrequired, "error");
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
           // SetAlert("Xoá thành công", "success");
            SetAlert(StaticResources.Resources.Deletesuccessful, "success");
            return RedirectToAction("Index");
        }
    }
}