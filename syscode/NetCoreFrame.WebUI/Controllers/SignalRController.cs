using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.WebUI.Extensions;
using NetCoreFrame.WebUI.Models;
using NetCoreFrame.WebUI.Views.Hubs;

namespace NetCoreFrame.WebUI.Controllers
{
    public class SignalRController : Controller
    {
        private readonly IHubContext<SignalrHubs> _countHub;
        private readonly ILogger<SignalRController> _logger;
        public SignalRController(IHubContext<SignalrHubs> countHub, ILogger<SignalRController>  logger)
        {
            
            _countHub = countHub;
            _logger = logger;
        }

        /// <summary>
        /// 提醒工位要更新图片了
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdImage(string  id)
        {
            //失败-指定用户
            //await _countHub.Clients.User(id).SendAsync("ReceiveImage", "1111");

            //成功-所有用户
            await _countHub.Clients.All.SendAsync("ReceiveImage", id);

            //失败-指定组
            //await _countHub.Clients.Group(id).SendAsync("ReceiveImage",id);
             
            LogHelper.WriteLogs("UpdImage结束！"); 
        }

        /// <summary>
        /// 提醒所有工位有新消息了
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ReceiveNewMsg(string message)
        {
            await _countHub.Clients.All.SendAsync("ReceiveMsg", message);
             
        }
    }
}
