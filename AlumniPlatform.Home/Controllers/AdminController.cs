
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPlatform.Home.Models;
using AlumniPlatform.Logic.BLL;
using AlumniPlatform.Logic.Model;

namespace AlumniPlatform.Home.Controllers
{
    //[MyAuthorFilter(Name="AdminController")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        /// <summary>
        /// 指示板
        /// </summary>
        /// <returns></returns>
        [MyAuthorFilter]
        public ActionResult Index()
        {
            ViewData["CurrentPage"] = "/Admin/Index";
            return View();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [MyAuthorFilter]
        public ActionResult UserList()
        {
            ViewData["CurrentPage"] = "/Admin/UserList";
            return View();
        }

        [MyAuthorFilter]
        public ActionResult Report()
        {
            ViewData["CurrentPage"] = "/Admin/Report";
            return View();
        }

        [MyAuthorFilter]
        public ActionResult LogInfo()
        {
            ViewData["CurrentPage"] = "/Admin/LogInfo";
            return View();
        }

        public JsonResult Get(string userId)
        {
            ResultInfo result = new ResultInfo();
            UserService user = new UserService();

            result.Data = user.Get(userId);
            if (result.Data != null)
            {
                result.IsSuccess = true;
            }
            else
            {
                result.ErrorCode = -1;
                result.Message = "获取用户出错!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult Delete(int id)
        {
            ResultInfo result = new ResultInfo();
            UserService user = new UserService();

            result.IsSuccess = user.DeleteUser(id);
            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "删除用户失败!";

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult Save(UserInfo userInfo)
        {
            ResultInfo result = new ResultInfo();
            UserService user = new UserService();
            result.IsSuccess = user.ModifyUser(userInfo);
            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "修改用户信息失败!";

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult ResetPwd(string userId)
        {
            ResultInfo result = new ResultInfo();

            UserService user = new UserService();
            result.IsSuccess = user.ResetUserPwd(userId);

            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "修改用户信息失败!";

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 启用选择的用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult EnableUser(string userId)
        {
            ResultInfo result = new ResultInfo();

            UserService user = new UserService();
            result.IsSuccess = user.EnableUsers(userId,1);

            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "修改用户信息失败!";

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult GetUserList(int pageNumber, int pageSize, string searchText)
        {
            //判定是否登录
            List<UserInfo> userInfos = new List<UserInfo>();
            int total = 0;

            UserService user = new UserService();

            userInfos = user.GetUserList(searchText, pageSize, pageNumber, out total);
            var data = new
            {
                total,
                rows = userInfos,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        [MyAuthorFilter]
        public JsonResult ModifyPassword(string userId,string oldPwd,string newPwd)
        {
            ResultInfo result = new ResultInfo();
            if (string.IsNullOrEmpty(userId))
            {
                result.ErrorCode = 10;
                result.Message = "用户名不能为空!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                result.ErrorCode = 11;
                result.Message = "新旧密码不能为空!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            UserService user = new UserService();
            result.IsSuccess = user.ModiyPwd(userId, oldPwd, newPwd);
            if (!result.IsSuccess)
            {
                result.ErrorCode = -1;
                result.Message = "密码修改失败!";
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取仪表盘数据
        /// </summary>
        /// <returns></returns>
        [MyAuthorFilter]
        public JsonResult GetDashBorad()
        {
            DashBoardService dashBoardService = new DashBoardService();
            var data = dashBoardService.Get();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
