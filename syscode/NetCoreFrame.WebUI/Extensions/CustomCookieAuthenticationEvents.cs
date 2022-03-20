using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    /// <summary>
    /// 自定义Cookie验证事件
    /// </summary>
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public async override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            //获取当事人信息
            var userPrincipal = context.Principal;
            //获取当事人最后登陆的时间
            var lastChaged = userPrincipal.Claims.Where(c => c.Type == "LastChanged").Select(c => c.Value).First();

            if (!string.IsNullOrEmpty(lastChaged))
            {
                //取数据库中的LastChanged字段判断用户是否修改过。
                //Do Something()
                //如果修改过 登出
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
