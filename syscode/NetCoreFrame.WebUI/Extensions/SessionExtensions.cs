using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    /// <summary>
    /// Session扩展
    /// </summary>
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        //{
        //   var user = new Frame_User();
        //user.UserName = "用户名称";
        //    HttpContext.Session.Set<Frame_User>("SessionTest", user);
        //    var sessionresult = HttpContext.Session.Get<Frame_User>("SessionTest");
        //}
     
    }
}
