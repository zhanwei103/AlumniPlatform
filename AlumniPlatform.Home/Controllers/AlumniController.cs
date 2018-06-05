using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPlatform.Home.Models;
using AlumniPlatform.Logic.BLL;
using AlumniPlatform.Logic.Model;

namespace AlumniPlatform.Home.Controllers
{
    public class AlumniController : Controller
    {
        //
        // GET: /Article/
          [MyAuthorFilter]
        public ActionResult List()
        {
            ViewData["CurrentPage"] = "/Alumni/List";
            return View();
        }
          [MyAuthorFilter]
        public ActionResult Edit()
        {
            ViewData["CurrentPage"] = "/Alumni/Edit";
            return View();
        }
          [MyAuthorFilter]
        public ActionResult Modify(int id)
        {
            AlumniService service = new AlumniService();
            AlumniInfo alumniInfo = service.Get(id);
            if (alumniInfo != null)
            {
                ViewData["CurrentPage"] = "/Alumni/Modify";
                return View(alumniInfo);
            }
            else
            {
                return View();
            }
        }
          [MyAuthorFilter]
        public ActionResult Preview(int id)
        {
            AlumniService service = new AlumniService();
            AlumniInfo alumniInfo = service.Get(id);
            if (alumniInfo != null)
            {
                ViewData["CurrentPage"] = "/Alumni/Preview";
                return View(alumniInfo);
            }
            else
            {
                return View();
            }
        }


        #region 动作
          [MyAuthorFilter]
        public JsonResult Save(AlumniInfo alumniInfo)
        {
            ResultInfo result = new ResultInfo();
            if (string.IsNullOrEmpty(alumniInfo.RealName) || string.IsNullOrEmpty(alumniInfo.Gender)
                || string.IsNullOrEmpty(alumniInfo.Phone) || string.IsNullOrEmpty(alumniInfo.Address))
            {
                result.ErrorCode = 1;
                result.Message = "缺少参数";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            alumniInfo.Nation = string.IsNullOrEmpty(alumniInfo.Nation) ? string.Empty : alumniInfo.Nation;
            alumniInfo.NativePlace = string.IsNullOrEmpty(alumniInfo.NativePlace) ? string.Empty : alumniInfo.NativePlace;
            alumniInfo.MaritalStatus = string.IsNullOrEmpty(alumniInfo.MaritalStatus) ? string.Empty : alumniInfo.MaritalStatus;
            alumniInfo.Email = string.IsNullOrEmpty(alumniInfo.Email) ? string.Empty : alumniInfo.Email;
            alumniInfo.MaxDegree = string.IsNullOrEmpty(alumniInfo.MaxDegree) ? string.Empty : alumniInfo.MaxDegree;
            alumniInfo.Company = string.IsNullOrEmpty(alumniInfo.Company) ? string.Empty : alumniInfo.Company;
            alumniInfo.Position = string.IsNullOrEmpty(alumniInfo.Position) ? string.Empty : alumniInfo.Position;
            alumniInfo.GraduatedFrom = string.IsNullOrEmpty(alumniInfo.GraduatedFrom) ? string.Empty : alumniInfo.GraduatedFrom;
            alumniInfo.Major = string.IsNullOrEmpty(alumniInfo.Major) ? string.Empty : alumniInfo.Major;
            alumniInfo.AlumniPosition = string.IsNullOrEmpty(alumniInfo.AlumniPosition) ? string.Empty : alumniInfo.AlumniPosition;
            alumniInfo.Hobby = string.IsNullOrEmpty(alumniInfo.Hobby) ? string.Empty : alumniInfo.Hobby;
            alumniInfo.Remark = string.IsNullOrEmpty(alumniInfo.Remark) ? string.Empty : alumniInfo.Remark;
            

            AlumniService service = new AlumniService();
            result.IsSuccess = service.Modify(alumniInfo);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

          [MyAuthorFilter]
        public JsonResult Delete(int SerialNumber)
        {
            ResultInfo result = new ResultInfo();

            AlumniService service = new AlumniService();
            result.IsSuccess = service.Delete(SerialNumber);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
          [MyAuthorFilter]
        public JsonResult QueryList(int pageNumber, int pageSize, string searchText)
        {
            AlumniService service = new AlumniService();
            int total = 0;
            List<AlumniInfo> alumniInfos = service.Query(searchText, pageSize, pageNumber, out total);

            var data = new
            {
                total = total,
                rows = alumniInfos
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 下载Excel文件
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public FileResult ExportExcel(string searchText)
        {
            AlumniService service = new AlumniService();
            List<AlumniInfo> alumniInfos = service.Query();

            string[] headers =
                {
                    "编号", "姓名", "性别", "民族", "籍贯", "生日", "婚姻状况", "手机号码", "电子邮箱", "联系地址", "最高学历",
                    "毕业院校", "主修专业", "毕业时间", "兴趣爱好", "校友会职务", "工作单位", "社会职位", "实名认证", "备注"
                };
            byte[] buffer = FileHelper.ExportToExcel(headers, alumniInfos, "校友名录");
            return File(buffer, "application/ms-excel", "哈理工校友通讯录.xls");

        }

        /// <summary>
        /// 上传图片动作
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadPhoto()
        {
            ResultInfo result = new ResultInfo();

            var file = Request.Files;

            string productName = Request["type"];
            if (string.IsNullOrEmpty(productName))
            {
                result.ErrorCode = 100;
                result.Message = "请输入文件类型";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            string BasePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(BasePath, "Image\\Photo\\" + productName + "\\");//图片路径
            int count = Request.Files.Count;//图片个数

            for (int i = 0; i < count; i++)
            {
                HttpPostedFileBase imageFile = Request.Files[i];
                if (imageFile != null)
                {
                    //获取文件类型
                    string fileName = Path.GetFileName(imageFile.FileName).ToLower();
                    string fileExt = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!fileExt.Equals(".jpg"))
                    {
                        result.ErrorCode = 101;
                        result.Message = "请选择(.jpg)类型的文件";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    string filePath = string.Empty;

                    SaveFile(imageFile, path, fileName);
                }
            }
            //ProductDisplayDb productDb = new ProductDisplayDb();
            //DataTable dt = productDb.QueryUserDetailByID(productName);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    bool result = productDb.UpdateOne(productName, "Image\\Photo\\" + productName, count);
            //}
            //else
            //{
            //    bool result = productDb.InsertOne(productName, "Image\\Photo\\" + productName, count, 250);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="path">路径</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public bool SaveFile(HttpPostedFileBase file, string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string savePath = path + name;
            file.SaveAs(savePath);

            return true;
        }


        #endregion
    }
}
