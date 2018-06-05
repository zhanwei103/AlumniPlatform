using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Data.OracleClient;

namespace Common.DbFactory
{
    public class OracleFactory:AbstractFactory
    {

        private OracleConnection m_conn = null;
        private string connectionString = string.Empty;

        /// <summary>
        /// 默认数据库
        /// </summary>
        public OracleFactory()
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
        public OracleFactory(string name)
        {
            //可以根据数据库连接字符串name来获取字符串信息
            connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        
       
        public DbConnection CreateConnection()
        {
            m_conn = new OracleConnection(connectionString);
            return m_conn;
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                OracleConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DataTable dt = new DataTable();

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
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

        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                OracleConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DataSet ds = new DataSet();

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
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

        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                OracleConnection.ClearAllPools();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 命令执行前准备
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
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
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }



    }
}

