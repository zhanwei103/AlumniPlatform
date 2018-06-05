/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：70c6b1b0-92f9-4899-ab9b-297bf0f3c72e
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.DAL
 * 类名称：UserDao
 * 文件名：UserDao
 * 创建时间：2018/5/22 13:33:05
 * 创建人：詹伟
 * 创建说明：
 *****************************************************************************************************
 * 修改人：
 * 修改时间：
 * 修改说明：
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniPlatform.Logic.Model;
using Common.DbFactory;

namespace AlumniPlatform.Logic.DAL
{
    public class UserDao : SqlBase, IBaseDao<UserInfo>
    {

        public UserDao()
        {
            base.GetDataBase();
        }
        /// <summary>
        /// 插入一个用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool Insert(UserInfo userInfo)
        {
            bool result = false;

            string sqlCommand = "insert into [T_USER_INFO] ([User_ID],[Role_ID],[Email],[Salt],[Password],[Status],[Register_Time],[Real_Name],[Phone],[Permission]) values (@UserID,@Role,@Email,@Salt,@Password,@Status,@RegisterTime,@RealName,@Phone,@Permission) ";
            DbParameter[] param = { 
                                  new SqlParameter("@UserID",DbType.String),
                                  new SqlParameter("@Role",DbType.Int32),
                                  new SqlParameter("@Email",DbType.String),
                                  new SqlParameter("@Salt",DbType.String),
                                  new SqlParameter("@Password",DbType.String),
                                  new SqlParameter("@Status",DbType.Int32),
                                  new SqlParameter("@RegisterTime",DbType.DateTime),
                                  new SqlParameter("@RealName",DbType.String),
                                  new SqlParameter("@Phone",DbType.String),
                                  new SqlParameter("@Permission",DbType.String),
                                  };
            param[0].Value = userInfo.UserID;
            param[1].Value = userInfo.Role;
            param[2].Value = userInfo.Email;
            param[3].Value = userInfo.Salt;
            param[4].Value = userInfo.Password;
            param[5].Value = userInfo.Status;
            param[6].Value = userInfo.RegisterTime;
            param[7].Value = userInfo.RealName;
            param[8].Value = userInfo.Phone;
            param[9].Value = userInfo.Permissions;

            int rows = db.ExecuteNonQuery(CommandType.Text, sqlCommand, param);
            if (rows == 1)
                result = true;

            return result;
        }

        /// <summary>
        /// 查询用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo Get(string userId)
        {
            UserInfo userInfo = null;
            string sqlCommand = "select * from [T_USER_INFO] where User_ID=@UserID";
            DbParameter[] param = { 
                                  new SqlParameter("@UserID",DbType.String)
                                  };
            param[0].Value = userId;

            DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, param);
            if (dt != null && dt.Rows.Count == 1)
            {
                userInfo = new UserInfo();
                ToModel(dt.Rows[0], userInfo);
            }
            return userInfo;
        }

        /// <summary>
        /// 检测用户账号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CheckUserID(string userID)
        {
            bool result = false;
            DbParameter[] param = { 
                                    new SqlParameter("@userID",SqlDbType.NVarChar,50)
                                   };
            param[0].Value = userID;


            string sqlCommand = "select Count(*) from [T_USER_INFO] where [User_ID]=@userID";
            int rows = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlCommand, param));
            if (rows > 0)
                result = true;

            return result;
        }

        /// <summary>
        /// 检测用户email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            bool result = false;
            SqlParameter[] param = { 
                                    new SqlParameter("@email",SqlDbType.NVarChar,50)
                                   };
            param[0].Value = email;


            string sqlCommand = "select Count(*) from [T_User_Info] where [Email]=@email";
            int rows = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlCommand, param));
            if (rows > 0)
                result = true;

            return result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool Update(UserInfo userInfo)
        {
            bool result = false;

            string sqlCommand = "update [T_User_Info] set [Role_ID]=@Role,[Email]=@Email,[Salt]=@Salt,[Password]=@Password,[Status]=@Status,[Real_Name]=@RealName,[Phone]=@Phone where [User_ID]=@UserID";
            DbParameter[] param = { 
                                  new SqlParameter("@UserID",DbType.String),
                                  new SqlParameter("@Role",DbType.Int32),
                                  new SqlParameter("@Email",DbType.String),
                                  new SqlParameter("@Salt",DbType.String),
                                  new SqlParameter("@Password",DbType.String),
                                  new SqlParameter("@Status",DbType.Int32),
                                  new SqlParameter("@RegisterTime",DbType.DateTime),
                                  new SqlParameter("@RealName",DbType.String),
                                  new SqlParameter("@Phone",DbType.String),
                                  };
            param[0].Value = userInfo.UserID;
            param[1].Value = userInfo.Role;
            param[2].Value = userInfo.Email;
            param[3].Value = userInfo.Salt;
            param[4].Value = userInfo.Password;
            param[5].Value = userInfo.Status;
            param[6].Value = userInfo.RegisterTime;
            param[7].Value = userInfo.RealName;
            param[8].Value = userInfo.Phone;

            int rows = db.ExecuteNonQuery(CommandType.Text, sqlCommand, param);
            if (rows == 1)
                result = true;

            return result;
        }
        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateMulStatus(string userId, int status)
        {
            bool result = false;

            string sqlCommand = "update [T_User_Info] set [Status]=@Status where [User_ID] in (" + userId + ")";

            SqlParameter[] param = { 
                                   new SqlParameter("@Status",SqlDbType.Int)
                                   };
            param[0].Value = status;

            int rows = db.ExecuteNonQuery(CommandType.Text, sqlCommand, param);
            if (rows > 0)
                result = true;

            return result;
        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ResetUserPwd(string userId, string salt, string password)
        {
            bool result = false;

            string sqlCommand = "update [T_User_Info] set [Salt]=@Salt,[Password]=@Password where [User_ID] in (" + userId + ")";

            SqlParameter[] param = { 
                                   new SqlParameter("@Salt",SqlDbType.NVarChar,50),
                                   new SqlParameter("@Password",SqlDbType.NVarChar,50)
                                   };
            param[0].Value = salt;
            param[1].Value = password;

            int rows = db.ExecuteNonQuery(CommandType.Text, sqlCommand, param);
            if (rows > 0)
                result = true;

            return result;
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<UserInfo> Query(string conditions, int pageSize, int currentPage, out int total)
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            total = 0;
            DbParameter[] param = { 
                                    new SqlParameter("@Conditions",SqlDbType.NVarChar)
                                   };
            param[0].Value = "%" + conditions + "%";

            string sqlCommand1 = "select a.* from [T_User_Info] a";
            string sqlCommand2 = "select Count(*) from [T_User_Info] a ";
            string sqlCommand3 = " where a.User_ID like @Conditions or a.Real_Name like @Conditions ";

            //总条数
            total = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlCommand2 + sqlCommand3, param));

            int prevPageFirst = pageSize * (currentPage - 1);
            //如果第一条大于总共的记录数
            if (prevPageFirst > total)
            {
                prevPageFirst = 0;
            }
            //如果是最后一页则
            if (total < pageSize * currentPage)
            {
                pageSize = total - prevPageFirst;
            }
            string sqlCommand = "select top " + pageSize + " o.* from (select row_number() over(order by [User_ID] asc) as rownumber,* from(" + sqlCommand1 + sqlCommand3 + ") as oo) as o where rownumber>" + prevPageFirst;

            DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UserInfo userInfo = new UserInfo();
                    ToModel(dr, userInfo);
                    userInfos.Add(userInfo);
                }
            }

            return userInfos;
        }

        private void ToModel(DataRow dr, UserInfo userInfo)
        {
            userInfo.Index = Convert.ToInt32(dr["Index"].ToString());
            userInfo.UserID = dr["User_ID"].ToString();
            userInfo.Role = Convert.ToInt32(dr["Role_ID"].ToString());
            userInfo.Email = dr["Email"].ToString();
            userInfo.Salt = dr["Salt"].ToString();
            userInfo.Password = dr["Password"].ToString();
            userInfo.Status = Convert.ToInt32(dr["Status"].ToString());
            userInfo.RegisterTime = DateTime.Parse(dr["Register_Time"].ToString());
            userInfo.Phone = dr["Phone"].ToString();
            userInfo.RealName = string.IsNullOrEmpty(dr["Real_Name"].ToString()) ? string.Empty : dr["Real_Name"].ToString();
            userInfo.Permissions = string.IsNullOrEmpty(dr["Permission"].ToString()) ? string.Empty : dr["Permission"].ToString();
        }

        public bool Modify(UserInfo obj)
        {
            if (obj.Index != 0)
            {
                return Update(obj);
            }
            else
            {
                return Insert(obj);
            }
        }

        public bool Delete(int key)
        {
            bool result = false;
            try
            {
                DbParameter[] param = { 
                                    new SqlParameter("@Index",SqlDbType.Int)
                                   };
                param[0].Value = key;

                string sqlCommand = @"DELETE FROM [T_USER_INFO]
                                     WHERE [Index]=@Index";
                int rows = db.ExecuteNonQuery(CommandType.Text, sqlCommand, param);
                if (rows == 1)
                    result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public UserInfo Get(int key)
        {
            try
            {
                UserInfo userInfo = null;

                string sqlCommand = "select * from [T_User_Info] where [Index]=@Index";

                SqlParameter[] param = { 
                                   new SqlParameter("@Index",SqlDbType.Int)
                                   };
                param[0].Value = key;

                DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    userInfo = new UserInfo();
                    ToModel(dt.Rows[0], userInfo);
                }

                return userInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UserInfo> Query()
        {
            throw new NotImplementedException();
        }

    }
}
