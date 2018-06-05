/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：eb9e12bd-c5b3-4cec-b90b-777a9dabf46d
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.DAL
 * 类名称：BaseDao
 * 文件名：BaseDao
 * 创建时间：2018/5/22 8:42:37
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

namespace AlumniPlatform.Logic.DAL
{
    public interface IBaseDao<T>
    {
        bool Insert(T obj);
        bool Update(T obj);
        bool Modify(T obj);
        bool Delete(int key);
        T Get(int key);
        List<T> Query();
        List<T> Query(string conditions, int pageSize, int currentPage, out int total);
    }
}
