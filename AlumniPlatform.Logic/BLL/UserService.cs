using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniPlatform.Logic.DAL;
using AlumniPlatform.Logic.Model;


namespace AlumniPlatform.Logic.BLL
{
    public class UserService
    {
        UserDao userDb = new UserDao();

        private int errorCode = 0;
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode 
        { 
            get { return this.errorCode; } 
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool Register(UserInfo userInfo)
        {
            bool result = false;

            if (string.IsNullOrEmpty(userInfo.UserID) || string.IsNullOrEmpty(userInfo.Email) || string.IsNullOrEmpty(userInfo.Password))
            {
                return false;
            }
            if (userInfo.Role < 1)
            {//用户角色不正确
                return false;
            }
            if (userDb.CheckUserID(userInfo.UserID) || userDb.CheckEmail(userInfo.Email))
            {//检查userID和Email
                return false;
            }
            //密码加密
            userInfo.Salt = Hash.CreateSalt();
            userInfo.Password = Hash.CreateHash(userInfo.Password, userInfo.Salt);
            userInfo.RegisterTime = DateTime.Now;
            userInfo.Status = 2;//待审核
            userInfo.Permissions = "";//权限

            result = userDb.Insert(userInfo);

            return result;
        }

        /// <summary>
        /// 检查登录用户名和密码
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin(string userId,string password,ref UserInfo userInfo)
        {
            bool result = false;
            userInfo = userDb.Get(userId);

            if (userInfo!=null&&userInfo.Status == 1)
            {
                string str_HashPwd = Hash.CreateHash(password, userInfo.Salt);
                if (str_HashPwd != userInfo.Password)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else
            {//用户不存在或未审核
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public bool ModiyPwd(string userId, string oldPwd, string newPwd)
        {
            bool result = false;
            UserInfo userInfo = userDb.Get(userId);
            string str_HashPwd = Hash.CreateHash(oldPwd, userInfo.Salt);
            if (str_HashPwd == userInfo.Password)
            {//旧密码验证成功
                userInfo.Salt = Hash.CreateSalt();
                userInfo.Password = Hash.CreateHash(newPwd, userInfo.Salt);
                result = userDb.Update(userInfo);
            }
            else
            {
                result = false;
            }

            return result;
        }
        
        public bool EnableUsers(string userId, int status)
        {
            string[] userIds = userId.Split(',');
            string[] temp = new string[userIds.Length];
            for (int i = 0; i < userIds.Length; i++)
            {
                temp[i] = "'" + userIds[i] + "'";
            }
            userId = string.Join(",", temp);
            return userDb.UpdateMulStatus(userId, status);
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserList(string conditions, int pageSize, int currentPage, out int total)
        {
            total = 0;
            return userDb.Query(conditions, pageSize, currentPage, out total);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo Get(string userId)
        {
            return userDb.Get(userId);
        }
        #region 批量操作
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int key)
        {
            bool result = false;

            result = userDb.Delete(key);

            return result;
        }
        
        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool ModifyUser(UserInfo userInfo)
        {
            bool result = false;
            //获取旧信息
            UserInfo olduserInfo = userDb.Get(userInfo.UserID);

            if (userInfo == null)
            {
                return false;
            }
            //olduserInfo.Role = userInfo.Role;//角色未启用
            olduserInfo.RealName = userInfo.RealName;
            olduserInfo.Email = userInfo.Email;
            olduserInfo.Phone = userInfo.Phone;

            result=userDb.Update(olduserInfo);

            return result;
        }
        
        
        /// <summary>
        /// 重置多用户密码
        /// </summary>
        /// <param name="userId">多用户用英文逗号隔开</param>
        /// <returns></returns>
        public bool ResetUserPwd(string userId)
        {
            bool reslut = false;

            string[] userIds = userId.Split(',');
            string[] temp = new string[userIds.Length];
            for (int i = 0; i < userIds.Length; i++)
            {
                temp[i] = "'" + userIds[i] + "'";
            }
            userId = string.Join(",", temp);

            string password = "123456";//重置密码
            string salt = Hash.CreateSalt();
            password = Hash.CreateHash(password, salt);

            reslut = userDb.ResetUserPwd(userId, salt, password);
            return reslut;
        }

        #endregion

    }
}
