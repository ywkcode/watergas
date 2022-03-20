using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    /// <summary>
    /// 缓存帮助类
    /// 2019-09-27 ywk
    /// </summary>
    public static  class CacheHelper
    { 
        public static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(IConfiguration _configuration)
        {
            var options = new MemoryCacheEntryOptions
            {
                //绝对过期时间 从配置项读取
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(Convert.ToDouble(_configuration.GetValue<string>("CacheSettings:ExpiredTimeHour"))),
                //滑动过期时间
                SlidingExpiration = TimeSpan.FromMinutes(Convert.ToDouble(_configuration.GetValue<string>("CacheSettings:SlidingExpiration")))
            };

            return options;
        }
    }
}
