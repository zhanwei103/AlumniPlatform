using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AlumniPlatform.Home.Models;
using AlumniPlatform.Logic.BLL;
using AlumniPlatform.Logic.Model;

namespace AlumniPlatform.Home.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }


        #region 用户操作
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            Session["AuthorToken"] = null;

            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session.Abandon();//取消当前会话

            return View("Login");
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLogin()
        {
            ResultInfo result = new ResultInfo();
            string userId = Request["userId"];
            string password = Request["password"];

            if (string.IsNullOrEmpty(userId))
            {
                result.ErrorCode = 10;
                result.Message = "用户名不能为空!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(password))
            {
                result.ErrorCode = 11;
                result.Message = "密码不能为空!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            UserInfo userInfo = new UserInfo();
            userInfo.UserID = userId;
            userInfo.Password = password;
            UserService user = new UserService();
            if (user.CheckLogin(userId, password, ref userInfo))
            {
                Session["CurrentUser"] = userInfo.UserID;
                Session["AuthorToken"] = Guid.NewGuid();
                CookiesHelper.SetCookie(userInfo);

                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["count"] = Convert.ToInt32(System.Web.HttpContext.Current.Application["count"]) + 1;
                System.Web.HttpContext.Current.Application.UnLock();

                result.IsSuccess = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.ErrorCode = 12;
                result.Message = "用户名和密码不匹配!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyPassword()
        {
            bool result = false;

            string userId = Request["userId"];
            if (string.IsNullOrEmpty(userId))
            {
                return Content("no:10");
            }
            string oldPwd = Request["oldPassword"];
            string newPwd = Request["password"];
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                return Content("no:11");
            }

            UserService user = new UserService();
            result = user.ModiyPwd(userId, oldPwd, newPwd);
            if (result)
                return Content("ok:0");
            else
                return Content("no:1");
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="factory"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="realname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult RegisterUser(string userId,  string email, string phone, string realname, string password)
        {
            ResultInfo result = new ResultInfo();

            UserInfo userInfo = new UserInfo();
            userInfo.UserID = userId;
            userInfo.RealName = realname;
            userInfo.Email = email;
            userInfo.Phone = phone;
            userInfo.Password = password;
            userInfo.Role = 3;//默认普通用户

            UserService user = new UserService();
            result.IsSuccess = user.Register(userInfo);//写入数据库

            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "用户注册失败";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
