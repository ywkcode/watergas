using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    /// <summary>
    /// 当前用户（全局对象）
    /// 2019-09-26 ywk
    /// </summary>
    public static class CurrentUser
    {
     

        private static IHttpContextAccessor _httpContextAccessor;

        private static ISession _session => _httpContextAccessor.HttpContext.Session;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
         
        /// <summary>
        /// 用户登录账户
        /// </summary>
        public static string LoginID
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_LoginID");
            set => _session.SetString("CurrentUser_LoginID", !string.IsNullOrEmpty(value) ? value : "");
        }

        
        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_UserName");
            set => _session.SetString("CurrentUser_UserName", !string.IsNullOrEmpty(value) ? value : "");
        }
         
        /// <summary>
        /// 用户角色
        /// </summary>
        public static string RoleName
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_RoleName");
            set => _session.SetString("CurrentUser_RoleName", !string.IsNullOrEmpty(value) ? value : "");
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public static string RoleID
        {
            //get => Convert.ToInt32(_session.GetString("CurrentUser_RoleID"));
            //set => _session.SetInt32("CurrentUser_RoleID", value);
            get => _session == null ? "" : _session.GetString("CurrentUser_RoleID");
            set => _session.SetString("CurrentUser_RoleID", !string.IsNullOrEmpty(value) ? value : "");
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public static string DeptName
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_DeptName");
            set => _session.SetString("CurrentUser_DeptName", !string.IsNullOrEmpty(value) ? value : "");
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public static string DeptID
        {
            //get => Convert.ToInt32(_session.GetString("CurrentUser_DeptID"));
            //set => _session.SetInt32("CurrentUser_DeptID", value);
            get => _session == null ? "" : _session.GetString("CurrentUser_DeptID");
            set => _session.SetString("CurrentUser_DeptID", !string.IsNullOrEmpty(value) ? value : "");
        }
        public static string SignalrId
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_SignalrId");
            set => _session.SetString("CurrentUser_SignalrId", !string.IsNullOrEmpty(value) ? value : "");
        }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public static string ID
        {
            get => _session == null ? "" : _session.GetString("CurrentUser_ID");
            set => _session.SetString("CurrentUser_ID", !string.IsNullOrEmpty(value) ? value : "");
        }
    }
}
