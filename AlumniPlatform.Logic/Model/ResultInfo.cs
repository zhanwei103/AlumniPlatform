/*****************************************************************************************************
 * 本代码版权归Astrongergy所有，All Rights Reserved (C) 2018-2066
 *****************************************************************************************************
 * 所属域：ASTRONERGY
 * 登录用户：wei.zhan
 * CLR版本：4.0.30319.42000
 * 唯一标识：0e7b573b-f748-4827-97f1-82b0984fd935
 * 机器名称：HZCND-10401006
 * 联系人邮箱：zhanwei103@126.com
 *****************************************************************************************************
 * 命名空间：AlumniPlatform.Logic.Model
 * 类名称：ResultInfo
 * 文件名：ResultInfo
 * 创建时间：2018/5/22 13:14:14
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
    public class ResultInfo
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
