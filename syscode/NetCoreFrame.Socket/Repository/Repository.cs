 
using SqlSugar;
using System;
using System.Collections.Generic;
namespace NetCoreFrame.SocketConsole.Repository
{
    public class Repository<T> : SimpleClient<T> where T : class, new()
    {

        public Repository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {

            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = "Data Source=localhost;Database=water;User ID=root;Password=123456;pooling=true;CharSet=utf8;port=3306;sslmode=none",

                });
            }
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public List<T> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
            return null;
        }

    }
}
