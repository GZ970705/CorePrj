using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.SqlTools
{
    public class SqlSugarConfig
    {
        /// <summary>
        /// 从配置文件读取数据库连接字符串
        /// </summary>
        static readonly string conStr = new ConfigurationBuilder()
          .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
          .Build().GetConnectionString("SqlServerConnection");//配置文件节的名字

        /// <summary>
        /// 基本配置项
        /// </summary>
        public static SqlSugarClient db
        {
            get => new SqlSugarClient(new ConnectionConfig() 
            {
                ConnectionString = conStr,//连接字符串
                DbType = SqlSugar.DbType.SqlServer,//数据库类型
                IsAutoCloseConnection = true,//是否自动关闭数据库连接
                InitKeyType = InitKeyType.Attribute
            });
        }

    }
}
