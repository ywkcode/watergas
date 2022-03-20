using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Filter
{
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 异常拦截 没被处理时 记录日志错误
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            //记录日志
            var actionName = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];
            _logger.LogError($"--------{actionName} Error Begin--------");
            _logger.LogError($"  Error Detail:" + context.Exception.Message);
            //拦截处理
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new TableData
                {
                    status = false,
                    msg = context.Exception.Message
                });//中断式---请求到这里结束了，不再继续Action 
                context.ExceptionHandled = true;
            }
            _logger.LogError($"--------{actionName} Error End--------");
        }
    }
}
