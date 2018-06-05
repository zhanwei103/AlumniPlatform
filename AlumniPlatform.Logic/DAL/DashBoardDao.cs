/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：88fd3cab-78d2-4dd8-864a-dbd6b5fbff8d
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.DAL
 * 类名称：DashBoardDao
 * 文件名：DashBoardDao
 * 创建时间：2018/6/3 19:50:14
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniPlatform.Logic.Model;
using Common.DbFactory;

namespace AlumniPlatform.Logic.DAL
{
    public class DashBoardDao:SqlBase
    {
        public DashBoardDao()
        {
            base.GetDataBase();
        }

        public AlumniDisInfo Get()
        {
            AlumniDisInfo alumniDisInfo = new AlumniDisInfo();
            try
            {
                string sqlCommand = @"SELECT COUNT(0) FROM [T_ALUMNI_INFO]";

                alumniDisInfo.Total= Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlCommand, null));

                string sqlCommand1 = @"SELECT COUNT(0) FROM [T_ALUMNI_INFO] WHERE [Gender]='男'";

                alumniDisInfo.Male = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlCommand1, null));

                alumniDisInfo.Female = alumniDisInfo.Total - alumniDisInfo.Male;

            }
            catch (Exception ex)
            { 
            
            }
            return alumniDisInfo;
        }
    }
}
