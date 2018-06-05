using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbFactory
{
    public interface AbstractFactory
    {
        /// <summary>
        /// 创建一个连接
        /// </summary>
        /// <returns>返回数据库连接</returns>
        DbConnection CreateConnection();
        /// <summary>
        /// 执行午返回的查询
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行查询,返回DataTable
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回DataTable</returns>
        DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params DbParameter[] commandParameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回Reader</returns>
        DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] commandParameters);

        /// <summary>
        /// 返回单个值（需转化成数据库对应的类型）
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回object</returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters);
        
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="trans">数据库事物</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters);
        
        /// <summary>
        /// 返回单个值（需转化成数据库对应的类型）
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回单个值</returns>
        object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters);
    }
}

