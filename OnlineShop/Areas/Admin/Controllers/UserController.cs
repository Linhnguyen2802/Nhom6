using Common;
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
    public class UserController : BaseController
    {
       
        public ActionResult Logout()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            //return Redirect("/");
            return RedirectToAction("Index","Login");
        }
        public ActionResult Select(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HashCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }


        [HttpGet]
        [HashCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HashCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (user.UserName == null)
            {
             //   SetAlert("Tên tài khoản bắt buộc", "error");
                SetAlert(StaticResources.Resources.Accountnamerequired, "error");
                return View("Create");
            }
            else if (user.Password == null)
            {
             //   SetAlert("Mật khẩu bắt buộc", "error");
                SetAlert(StaticResources.Resources.PasswordRequired, "error");
                return View("Create");
            }
            else if (ModelState.IsValid)
            {
                var dao = new UserDao();
                user.Password = Encryptor.MD5Hash(user.Password);
                user.GroupID = "ADMIN";
                var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                user.CreatedBy = session.UserName;
                user.CreatedDate = DateTime.Now;
                long id = dao.Insert(user);
                if (id > 0)
                {
                  //  SetAlert("Thêm tài khoản thành công", "success");
                    SetAlert(StaticResources.Resources.Addaccountsuccessfully, "success");
                    return RedirectToAction("Select", "User");
                }
                else
                {
                 //   ModelState.AddModelError("", "Thêm tài khoản không thành công");
                    ModelState.AddModelError("", StaticResources.Resources.Addingaccountfailed);
                }
            }
            return View("Create");
        }

        [HttpPost]
        [HashCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var session = (Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                user.ModifiedBy = session.UserName;
                user.ModifiedDate = DateTime.Now;
                var result = dao.Update(user);
                if (result)
                {
                 //   SetAlert("Cập nhật tài khoản thành công", "success");
                    SetAlert(StaticResources.Resources.Updateaccountsuccessfully, "success");
                    return RedirectToAction("Select", "User");
                }
                else
                {
                 //   ModelState.AddModelError("", "Cập nhật tài khoản không thành công");
                    ModelState.AddModelError("", StaticResources.Resources.Updatefailed);
                }
            }
            return View("Select");
        }

        [HttpDelete]
        [HashCredential(RoleID = "EDIT_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HashCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}