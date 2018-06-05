/*****************************************************************
*Copyright (c) 2017 Astronergy All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：HZCND-008104
*命名空间：Common.DbFactory
*文件名：  SqlBase
*版本号：  V1.0.0.0
*当前的用户域：ASTRONERGY
*创建人：  wei.zhan
*电子邮箱：zhanwei103@126.com
*创建时间：2017/5/11 15:23:43

*描述:
*=================================================================
*修改标记
*修改时间：2017/5/11 15:23:43
*修改人：  wei.zhan
*版本号：  V1.0.0.0
*描述：
*
******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbFactory
{
    public class SqlBase
    {
        public AbstractFactory db = null;
        public void GetDataBase()
        {
            this.db = Factory.GetInstance().CreateInstance();
        }
        public void GetDataBase(string name)
        {
            this.db = Factory.GetInstance().CreateInstance(name);
        }
    }
}
