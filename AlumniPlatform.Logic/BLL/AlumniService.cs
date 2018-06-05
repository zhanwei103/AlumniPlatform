/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：27e2da78-486f-46c5-a2a2-579b41a8bc84
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.BLL
 * 类名称：AlumniService
 * 文件名：AlumniService
 * 创建时间：2018/5/22 10:33:01
 * 创建人：詹伟
 * 创建说明：
 *****************************************************************************************************
 * 修改人：
 * 修改时间：
 * 修改说明：
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniPlatform.Logic.DAL;
using AlumniPlatform.Logic.Model;

namespace AlumniPlatform.Logic.BLL
{
    public class AlumniService
    {
        AlumniDao sqlServerDb = new AlumniDao();

        public bool Modify(AlumniInfo alumniInfo)
        {
            return sqlServerDb.Modify(alumniInfo);
        }

        public bool Delete(int key)
        {
            return sqlServerDb.Delete(key);
        }

        public AlumniInfo Get(int key)
        {
            return sqlServerDb.Get(key);
        }

        public List<AlumniInfo> Query(string searchText,int pageSize,int currentPage,out int total)
        {
            return sqlServerDb.Query(searchText,pageSize,currentPage,out total);
        }

        public List<AlumniInfo> Query()
        {
            return sqlServerDb.Query();
        }
    }
}
