/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：86379ae4-0267-47f9-ad2d-0f936d8d3304
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.BLL
 * 类名称：DashBoardService
 * 文件名：DashBoardService
 * 创建时间：2018/6/3 19:49:42
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
    public class DashBoardService
    {
        DashBoardDao service = new DashBoardDao();

        public AlumniDisInfo Get()
        {
            return service.Get();
        }
    }
}
