/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：7b8d7425-ab17-4c7c-ace7-8f1ebaf40fd2
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.Model
 * 类名称：UserInfo
 * 文件名：UserInfo
 * 创建时间：2018/5/22 13:30:09
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

namespace AlumniPlatform.Logic.Model
{
    public class UserInfo
    {

        public int Index { set; get; }
        public string UserID { set; get; }
        public int Role { set; get; }
        public string Email { set; get; }
        public int Status { set; get; }
        public string Salt { set; get; }
        public string Password { set; get; }
        public string RealName { set; get; }
        public DateTime RegisterTime { set; get; }
        public string Phone { set; get; }
        public string Permissions { set; get; }
    }
}
