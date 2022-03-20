using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCoreFrame.WebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Filter
{
    /// <summary>
    /// 登陆验证
    /// </summary>
    public class CustomActionCheckFilterAttribute : ActionFilterAttribute
    {
        #region Identity
      
     
        public CustomActionCheckFilterAttribute( )
        {
            
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
          
            if (CurrentUser.UserName == null)
            {
                context.HttpContext.Response.WriteAsync("<script>window.parent.location.href='../Account/Login'</script>");
                context.Result = new RedirectResult("~/Account/Login");
            } 
        }
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
