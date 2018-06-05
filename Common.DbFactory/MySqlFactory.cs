/*****************************************************************
*Copyright (c) 2017 Astronergy All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：HZCND-008104
*命名空间：Common.DbFactory
*文件名：  MySqlHelper
*版本号：  V1.0.0.0
*当前的用户域：ASTRONERGY
*创建人：  wei.zhan
*电子邮箱：zhanwei103@126.com
*创建时间：2017/5/11 15:05:08

*描述:
*=================================================================
*修改标记
*修改时间：2017/5/11 15:05:08
*修改人：  wei.zhan
*版本号：  V1.0.0.0
*描述：
*
******************************************************************/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbFactory
{
    public class MySqlFactory:AbstractFactory
    {
          private MySqlConnection m_conn = null;
        
        private string connectionString = string.Empty;

        /// <summary>
        /// 默认数据库
        /// </summary>
        public MySqlFactory()
        {
            //获取默认数据库名称
            string name = ConfigurationManager.AppSettings["defaultDataBase"];
            if (!string.IsNullOrEmpty(name))
            {
                //可以根据默认数据库进行配置
                connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
        }
        
        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <param name="name"></param>
        public MySqlFactory(string name)
        {
            //可以根据数据库连接字符串name来获取字符串信息
            connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


        public DbConnection CreateConnection()
        {
            m_conn = new MySqlConnection(connectionString);
            return m_conn;
        }


        public int ExecuteNonQuery(System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public System.Data.DataTable ExecuteDataTable(System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                MySqlConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DataTable dt = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                MySqlConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DataSet ds = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public System.Data.Common.DbDataReader ExecuteReader(System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                MySqlConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public int ExecuteNonQuery(System.Data.Common.DbConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(System.Data.Common.DbConnection connection, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        private void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


    }
}
