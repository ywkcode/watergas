using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlsugarBase
    { 
      /// <summary>
      /// 连接字符串
      /// </summary>
    public string connectionString = string.Empty;


    /// <summary>
    /// 
    /// </summary>
    public SqlSugarClient DB => GetInstance();

    SqlSugarClient GetInstance()
    {

        var db = new SqlSugarClient(
            new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.MySql,
                IsShardSameThread = true,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            }
        );
        return db;
    }
}
}
