/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：de1f9a7a-5861-4561-91e3-55f055118a68
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.DAL
 * 类名称：AlumniDao
 * 文件名：AlumniDao
 * 创建时间：2018/5/22 8:53:57
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
using AlumniPlatform.Logic.Model;
using Common.DbFactory;

namespace AlumniPlatform.Logic.DAL
{
    public class AlumniDao : SqlBase, IBaseDao<AlumniInfo>
    {
        public AlumniDao()
        {
            base.GetDataBase();
        }

        public bool Insert(AlumniInfo obj)
        {
            bool result = false;
            try
            {
                DbParameter[] param = { 
                                    new SqlParameter("@RealName",SqlDbType.NVarChar),
                                    new SqlParameter("@Gender",SqlDbType.NVarChar),
                                    new SqlParameter("@Nation",SqlDbType.NVarChar),
                                    new SqlParameter("@NativePlace",SqlDbType.NVarChar),
                                    new SqlParameter("@BirthDate",SqlDbType.NVarChar),
                                    new SqlParameter("@MaritalStatus",SqlDbType.NVarChar),
                                    new SqlParameter("@Phone",SqlDbType.NVarChar),
                                    new SqlParameter("@Email",SqlDbType.NVarChar),
                                    new SqlParameter("@Address",SqlDbType.NVarChar),
                                    new SqlParameter("@IsCertified",SqlDbType.Int),
                                    new SqlParameter("@MaxDegree",SqlDbType.NVarChar),
                                    new SqlParameter("@GraduatedFrom",SqlDbType.NVarChar),
                                    new SqlParameter("@Major",SqlDbType.NVarChar),
                                    new SqlParameter("@GraduationDate",SqlDbType.NVarChar),
                                    new SqlParameter("@Hobby",SqlDbType.NVarChar),
                                    new SqlParameter("@AlumniPosition",SqlDbType.NVarChar),
                                    new SqlParameter("@Company",SqlDbType.NVarChar),
                                    new SqlParameter("@Position",SqlDbType.NVarChar),
                                    new SqlParameter("@Remark",SqlDbType.NVarChar),
                                   };
                param[0].Value = obj.RealName;
                param[1].Value = obj.Gender;
                param[2].Value = obj.Nation;
                param[3].Value = obj.NativePlace;
                param[4].Value = string.IsNullOrEmpty(obj.BirthDate) ? string.Empty : obj.BirthDate;
                param[5].Value = obj.MaritalStatus;
                param[6].Value = obj.Phone;
                param[7].Value = obj.Email;
                param[8].Value = obj.Address;
                param[9].Value = obj.IsCertified;
                param[10].Value = obj.MaxDegree;
                param[11].Value = obj.GraduatedFrom;
                param[12].Value = obj.Major;
                param[13].Value = string.IsNullOrEmpty(obj.GraduationDate) ? string.Empty : obj.GraduationDate;
                param[14].Value = obj.Hobby;
                param[15].Value = obj.AlumniPosition;
                param[16].Value = obj.Company;
                param[17].Value = obj.Position;
                param[18].Value = obj.Remark;

                string sqlCommand = @"INSERT INTO [T_ALUMNI_INFO]
                                           ([RealName]
                                           ,[Gender]
                                           ,[Nation]
                                           ,[NativePlace]
                                           ,[BirthDate]
                                           ,[MaritalStatus]
                                           ,[Phone]
                                           ,[Email]
                                           ,[Address]
                                           ,[IsCertified]
                                           ,[MaxDegree]
                                           ,[GraduatedFrom]
                                           ,[Major]
                                           ,[GraduationDate]
                                           ,[Hobby]
                                           ,[AlumniPosition]
                                           ,[Company]
                                           ,[Position]
                                           ,[Remark])
                                     VALUES
                                           (@RealName
                                           ,@Gender
                                           ,@Nation
                                           ,@NativePlace
                                           ,@BirthDate
                                           ,@MaritalStatus
                                           ,@Phone
                                           ,@Email
                                           ,@Address
                                           ,@IsCertified
                                           ,@MaxDegree
                                           ,@GraduatedFrom
                                           ,@Major
                                           ,@GraduationDate
                                           ,@Hobby
                                           ,@AlumniPosition
                                           ,@Company
                                           ,@Position
                                           ,@Remark)";
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

        public bool Update(AlumniInfo obj)
        {
            bool result = false;
            try
            {
                DbParameter[] param = { 
                                    new SqlParameter("@RealName",SqlDbType.NVarChar),
                                    new SqlParameter("@Gender",SqlDbType.NVarChar),
                                    new SqlParameter("@Nation",SqlDbType.NVarChar),
                                    new SqlParameter("@NativePlace",SqlDbType.NVarChar),
                                    new SqlParameter("@BirthDate",SqlDbType.NVarChar),
                                    new SqlParameter("@MaritalStatus",SqlDbType.NVarChar),
                                    new SqlParameter("@Phone",SqlDbType.NVarChar),
                                    new SqlParameter("@Email",SqlDbType.NVarChar),
                                    new SqlParameter("@Address",SqlDbType.NVarChar),
                                    new SqlParameter("@IsCertified",SqlDbType.Int),
                                    new SqlParameter("@MaxDegree",SqlDbType.NVarChar),
                                    new SqlParameter("@GraduatedFrom",SqlDbType.NVarChar),
                                    new SqlParameter("@Major",SqlDbType.NVarChar),
                                    new SqlParameter("@GraduationDate",SqlDbType.NVarChar),
                                    new SqlParameter("@Hobby",SqlDbType.NVarChar),
                                    new SqlParameter("@AlumniPosition",SqlDbType.NVarChar),
                                    new SqlParameter("@Company",SqlDbType.NVarChar),
                                    new SqlParameter("@Position",SqlDbType.NVarChar),
                                    new SqlParameter("@Remark",SqlDbType.NVarChar),
                                    new SqlParameter("@SerialNumber",SqlDbType.Int)
                                   };
                param[0].Value = obj.RealName;
                param[1].Value = obj.Gender;
                param[2].Value = obj.Nation;
                param[3].Value = obj.NativePlace;
                param[4].Value = string.IsNullOrEmpty(obj.BirthDate) ? string.Empty : obj.BirthDate;
                param[5].Value = obj.MaritalStatus;
                param[6].Value = obj.Phone;
                param[7].Value = obj.Email;
                param[8].Value = obj.Address;
                param[9].Value = obj.IsCertified;
                param[10].Value = obj.MaxDegree;
                param[11].Value = obj.GraduatedFrom;
                param[12].Value = obj.Major;
                param[13].Value = string.IsNullOrEmpty(obj.GraduationDate) ? string.Empty : obj.GraduationDate;
                param[14].Value = obj.Hobby;
                param[15].Value = obj.AlumniPosition;
                param[16].Value = obj.Company;
                param[17].Value = obj.Position;
                param[18].Value = obj.Remark;
                param[19].Value = obj.SerialNumber;

                string sqlCommand = @"UPDATE [T_ALUMNI_INFO]
                                       SET [RealName] = @RealName
                                          ,[Gender] = @Gender
                                          ,[MaritalStatus] = @MaritalStatus
                                          ,[Nation] = @Nation
                                          ,[NativePlace] = @NativePlace
                                          ,[IsCertified] = @IsCertified
                                          ,[BirthDate] = @BirthDate
                                          ,[Phone] = @Phone
                                          ,[Email] = @Email
                                          ,[Address] = @Address
                                          ,[MaxDegree] = @MaxDegree
                                          ,[GraduatedFrom] = @GraduatedFrom
                                          ,[Major] = @Major
                                          ,[GraduationDate] = @GraduationDate
                                          ,[Hobby] = @Hobby
                                          ,[AlumniPosition] = @AlumniPosition
                                          ,[Company] = @Company
                                          ,[Position] = @Position
                                          ,[Remark] = @Remark
                                     WHERE [SerialNumber]=@SerialNumber";
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

        public bool Delete(int key)
        {
            bool result = false;
            try
            {
                DbParameter[] param = { 
                                    new SqlParameter("@SerialNumber",SqlDbType.Int)
                                   };
                param[0].Value = key;

                string sqlCommand = @"DELETE FROM [T_ALUMNI_INFO]
                                     WHERE [SerialNumber]=@SerialNumber";
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

        public List<AlumniInfo> Query()
        {
            try
            {
                List<AlumniInfo> alumniInfos = new List<AlumniInfo>();
                string sqlCommand = "select * from [T_ALUMNI_INFO]";

                DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AlumniInfo alumniInfo = new AlumniInfo();
                        ToModel(dr, alumniInfo);
                        alumniInfos.Add(alumniInfo);
                    }
                }

                return alumniInfos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AlumniInfo Get(int key)
        {
            try
            {
                AlumniInfo alumniInfo = new AlumniInfo();

                DbParameter[] param = { 
                                    new SqlParameter("@SerialNumber",SqlDbType.Int)
                                   };
                param[0].Value = key;

                string sqlCommand = "select * from [T_ALUMNI_INFO] WHERE [SerialNumber]=@SerialNumber ";

                DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, param);
                if (dt != null && dt.Rows.Count > 0)
                {

                    ToModel(dt.Rows[0], alumniInfo);
                }
                return alumniInfo;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool Modify(AlumniInfo obj)
        {
            if (obj.SerialNumber != 0)
            {
                return Update(obj);
            }
            else
            {
                return Insert(obj);
            }
        }

        public List<AlumniInfo> Query(string conditions, int pageSize, int currentPage, out int total)
        {
            List<AlumniInfo> alumniInfos = new List<AlumniInfo>();
            total = 0;
            DbParameter[] param = { 
                                    new SqlParameter("@Conditions",SqlDbType.NVarChar)
                                   };
            param[0].Value = "%" + conditions + "%";

            string sqlCommand1 = "select * from [T_ALUMNI_INFO] ";
            string sqlCommand2 = "select Count(*) from [T_ALUMNI_INFO] ";
            string sqlCommand3 = " where RealName like @Conditions or Major like @Conditions ";

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
            string sqlCommand = "select top " + pageSize + " o.* from (select row_number() over(order by [RealName] asc) as rownumber,* from(" + sqlCommand1 + sqlCommand3 + ") as oo) as o where rownumber>" + prevPageFirst;

            DataTable dt = db.ExecuteDataTable(CommandType.Text, sqlCommand, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AlumniInfo alumniInfo = new AlumniInfo();
                    ToModel(dr, alumniInfo);
                    alumniInfos.Add(alumniInfo);
                }
            }

            return alumniInfos;
        }

        private void ToModel(DataRow dr, AlumniInfo alumniInfo)
        {
            alumniInfo.SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
            alumniInfo.RealName = dr["RealName"].ToString();
            alumniInfo.Gender = dr["Gender"].ToString();
            alumniInfo.MaritalStatus = dr["MaritalStatus"].ToString();
            alumniInfo.Nation = dr["Nation"].ToString();
            alumniInfo.NativePlace = dr["NativePlace"].ToString();
            alumniInfo.IsCertified = Convert.ToInt32(dr["IsCertified"]);
            alumniInfo.BirthDate = dr["BirthDate"].ToString();
            alumniInfo.Phone = dr["Phone"].ToString();
            alumniInfo.Email = dr["Email"].ToString();
            alumniInfo.Address = dr["Address"].ToString();
            alumniInfo.MaxDegree = dr["MaxDegree"].ToString();
            alumniInfo.GraduationDate = dr["GraduationDate"].ToString();
            alumniInfo.Major = dr["Major"].ToString();
            alumniInfo.GraduatedFrom = dr["GraduatedFrom"].ToString();
            alumniInfo.AlumniPosition = dr["AlumniPosition"].ToString();
            alumniInfo.Company = dr["Company"].ToString();
            alumniInfo.Position = dr["Position"].ToString();
            alumniInfo.Remark = dr["Remark"].ToString();
        }

    }
}
