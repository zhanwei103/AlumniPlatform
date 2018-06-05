using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPlatform.Logic.Model;


namespace AlumniPlatform.Home.Models
{
    public static class CookiesHelper
    {
        public static void SetCookie(UserInfo userInfo)
        {
            HttpCookie userCookie=new HttpCookie("CurrentUser");

            userCookie.Values.Add("userid",userInfo.UserID);
            userCookie.Values.Add("role", userInfo.Role.ToString());

            HttpContext.Current.Response.AppendCookie(userCookie);
        }

        public static UserInfo GetCookie()
        {
            UserInfo userInfo=null;
            HttpCookie userCookie = HttpContext.Current.Request.Cookies["CurrentUser"];
            if (userCookie != null)
            {
                userInfo = new UserInfo();
                userInfo.UserID = userCookie.Values["userid"];
                userInfo.Role = Convert.ToInt32(userCookie.Values["role"]);
                return userInfo;
            }
            else
            {
                return userInfo;
            }
        }
    }
}