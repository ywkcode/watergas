using Microsoft.AspNetCore.SignalR;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.WebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Views.Hubs
{
    public class SignalrHubs:Hub
    {
        /// <summary>
        /// 客户连接成功时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
         
            LogHelper.WriteLogs("已连接！");
            await Groups.AddToGroupAsync(CurrentUser.UserName, CurrentUser.UserName);
            await base.OnConnectedAsync();
        }
        public async Task Login(string userid)
        {
            
            await Groups.AddToGroupAsync(userid, "Group1");
            await base.OnConnectedAsync();
        }

        public async Task LoginOut(string userid)
        {
           
            await Groups.RemoveFromGroupAsync(userid, "Group1");
            await base.OnConnectedAsync();
        }
     

        public override async Task OnDisconnectedAsync(Exception exception)
        { 
            await Groups.RemoveFromGroupAsync(Context.UserIdentifier, "Group1");
            await base.OnDisconnectedAsync(exception);
        }
        


        public async Task SignOut(string userid, string ConnectionId)
        {
           
            await Groups.RemoveFromGroupAsync(userid, "Group1");

        }
    }
}
