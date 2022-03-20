using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minio;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using NetCoreFrame.WebUI.Filter;

namespace NetCoreFrame.WebUI.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly Frame_UserService _service;
        private readonly Frame_TableInfoService _tableinfoservice; 
        private readonly IHttpContextAccessor _httpContextAccessor;
         
        public AccountController(
            Frame_UserService service,
             Frame_TableInfoService  tableinfoservice,
            IHttpContextAccessor httpContextAccessor,
            
            ILogger<AccountController>  logger )
        {
            _service = service;
            _tableinfoservice = tableinfoservice;
            _httpContextAccessor = httpContextAccessor;
            CurrentUser.Configure(_httpContextAccessor);
           
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Regist()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous] 
        public   IActionResult Login()
        {
            ////测试Minio文件上传
            //_tableinfoservice.AddMinio();
            return View() ;
        }
        [HttpPost]
        [AllowAnonymous]
       
        // public async Task<IActionResult>  Login(string UserName,string Password)
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            try
            {
                LogHelper.WriteLogs("登陆开始：");

                //新建一个当事人
                ClaimsPrincipal principal = null;
                LogHelper.WriteLogs("登陆开始：UserName+Password："+UserName+","+Password);
                var frameuser = _service.CheckLogin(UserName, Password);
                LogHelper.WriteLogs("登陆开始：验证成功");
                if (frameuser != null)
                {
                    //证件原信息
                    Claim name = new Claim(ClaimTypes.Name, UserName);
                    Claim role = new Claim(ClaimTypes.Role, "管理员");
                    Claim Userid = new Claim(ClaimTypes.Sid, frameuser.ID);
                    Claim lastChanged = new Claim("LastChanged", DateTime.Now.ToString());

                    //证件信息
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity("ERP");
                    claimsIdentity.AddClaim(name);
                    claimsIdentity.AddClaim(role);
                    principal = new ClaimsPrincipal(claimsIdentity);
                }

                if (principal == null)
                {
                    ModelState.AddModelError("Error", "");
                    return View();
                }
                LogHelper.WriteLogs("登陆开始：Session处理");
                await SetCurrentUser(frameuser, _httpContextAccessor, principal);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs("登陆异常："+ex.Message);
            }


            LogHelper.WriteLogs("登陆完成开始跳转");
            //_cache.Set<string>("timestamp", DateTime.Now.ToString());
            //string aaa = _cache.Get<string>("timestamp");
            return RedirectToAction("Index", "Home");
        }
        public PageResponse SysLogin(string UserName, string Password)
        {
            PageResponse pageResponse = new PageResponse();
            //新建一个当事人
            ClaimsPrincipal principal = null;

            var frameuser = _service.CheckLogin(UserName, Password);

            if (frameuser != null)
            {
                //证件原信息
                Claim name = new Claim(ClaimTypes.Name, UserName);
                Claim role = new Claim(ClaimTypes.Role, "管理员");
                Claim Userid = new Claim(ClaimTypes.Sid, frameuser.ID);
                Claim lastChanged = new Claim("LastChanged", DateTime.Now.ToString());

                //证件信息
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("ERP");
                claimsIdentity.AddClaim(name);
                claimsIdentity.AddClaim(role);
                principal = new ClaimsPrincipal(claimsIdentity);
                SetSysCurrentUser(frameuser, _httpContextAccessor, principal);
            }
            var productionMol = _service.CheckLogin(UserName);
            if (productionMol != null)
            {
                //证件原信息
                Claim name = new Claim(ClaimTypes.Name, UserName);
                Claim role = new Claim(ClaimTypes.Role, "管理员");
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("ERP");
                claimsIdentity.AddClaim(name);
                claimsIdentity.AddClaim(role);
                principal = new ClaimsPrincipal(claimsIdentity);
                SetSysCurrentUser(new Frame_User() { UserName= UserName }, _httpContextAccessor, principal);
                pageResponse.Message = "Station";
            }
            if (principal == null)
            {
                ModelState.AddModelError("Error", "");
                pageResponse.Status = false;
            } 
            return pageResponse;
           
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            //下线后 更新下线状态
            _service.SignOut(CurrentUser.ID);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public TableData SignOutTest(string userid)
        {
            //下线后 更新下线状态
            _service.SignOut(userid);
            return new TableData
            {
                code = 200
            };

        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public TableData Regist(string username, string pwd)
        {
            return _service.Regist(username, pwd);
        }

        /// <summary>
        /// 提供签名 并设置全局变量
        /// </summary>
        /// <param name="frameUser"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public async Task SetCurrentUser(Frame_User frameUser, IHttpContextAccessor httpContextAccessor, ClaimsPrincipal principal)
        {
            CurrentUser.Configure(httpContextAccessor);

            if (frameUser != null)
            {
                string role = string.Empty;
                CurrentUser.LoginID = frameUser.LoginID;
                CurrentUser.UserName = frameUser.UserName;
                CurrentUser.ID = frameUser.ID;
                CurrentUser.DeptName = frameUser.DeptName;
                CurrentUser.DeptID = frameUser.DeptID;
                CurrentUser.RoleName = frameUser.RoleName;
                CurrentUser.RoleID = frameUser.RoleID;
            }
            //授权-微软提供的登陆签名操作
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                //ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                ExpiresUtc = DateTime.UtcNow.AddSeconds(2),
                 RedirectUri= "~/Account/login",
                 IsPersistent = true,
                AllowRefresh = true
            });
        }

        public void SetSysCurrentUser(Frame_User frameUser, IHttpContextAccessor httpContextAccessor, ClaimsPrincipal principal)
        {
            CurrentUser.Configure(httpContextAccessor);

            if (frameUser != null)
            {
                string role = string.Empty;
                CurrentUser.LoginID = frameUser.LoginID;
                CurrentUser.UserName = frameUser.UserName;
                CurrentUser.ID = frameUser.ID;
                CurrentUser.DeptName = frameUser.DeptName;
                CurrentUser.DeptID = frameUser.DeptID;
                CurrentUser.RoleName = frameUser.RoleName;
                CurrentUser.RoleID = frameUser.RoleID;
            }
           
        }
    }
}