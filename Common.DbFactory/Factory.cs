using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbFactory
{
    public sealed class Factory
    {
        private static volatile Factory singleFactory = null;
        private static object syncObj = new object();
        
        /// <summary> 
        /// Factory类构造函数 
        /// </summary> 
        private Factory()
        {
        }

        /// <summary> 
        /// 获得Factory类的实例 
        /// </summary> 
        /// <returns>Factory类实例</returns> 
        public static Factory GetInstance()
        {
            if (singleFactory == null)
            {
                lock (syncObj)
                {
                    if (singleFactory == null)
                    {
                        singleFactory = new Factory();
                    }
                }
            }
            return singleFactory;
        }

        /// <summary> 
        /// 建立AbstractFactory类实例 
        /// </summary> 
        /// <returns>Factory类实例</returns> 
        public AbstractFactory CreateInstance()
        {
            string name = ConfigurationManager.AppSettings["defaultDataBase"];
            return CreateInstance(name);       
        }

        public AbstractFactory CreateInstance(string name)
        {
            AbstractFactory abstractFactory = null;
            string dataBaseType=ConfigurationManager.ConnectionStrings[name].ProviderName;


            switch (dataBaseType)
            {
                case "System.Data.SqlClient":
                    {
                        abstractFactory = new SqlFactory(name);
                        break;
                    }
                case "System.Data.OracleClient":
                    {
                        abstractFactory = new OracleFactory(name);
                        break;
                    }
                case "MySql.Data.MySqlClient":
                    {
                        abstractFactory = new MySqlFactory(name);
                        break;
                    }
                default: abstractFactory = null; 
                    break;
            }
            return abstractFactory;
        }

    }
}
