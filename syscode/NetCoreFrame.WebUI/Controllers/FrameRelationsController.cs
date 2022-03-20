using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameRelationsController : Controller
    {
        private readonly Frame_RelationsService _service;
        private readonly MemoryCacheExtensions _memoryCacheExtensions;
        public FrameRelationsController(Frame_RelationsService service, MemoryCacheExtensions  memoryCacheExtensions)
        {
            _service = service;
            _memoryCacheExtensions = memoryCacheExtensions;
        }

        [HttpPost]
        public string AddRelations(string FirstId, string SecondId,string RelationType)
        {
            PageResponse resp = new PageResponse();
            string KeyName = "Menu_" + CurrentUser.ID;
            if (_memoryCacheExtensions.Contains(KeyName))
            {
                _memoryCacheExtensions.Remove(KeyName);
            }
            _service.Add(FirstId, SecondId, RelationType);
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string DelRelations(string FirstId, string SecondId,string RelationType)
        {
            PageResponse resp = new PageResponse();
            string KeyName = "Menu_" + CurrentUser.ID;
            if (_memoryCacheExtensions.Contains(KeyName))
            {
                _memoryCacheExtensions.Remove(KeyName);
            } 
            _service.Delete(FirstId, SecondId, RelationType);
            return JsonHelper.Instance.Serialize(resp);
        }
        public string LoadFileByStationId(int page, int limit, string StationId)
        {
            TableData data = new TableData();
            data = _service.LoadFileByStationId(StationId);
            return JsonHelper.Instance.Serialize(data);
        }

        public string LoadUserByRoleId(int page, int limit, string RoleId)
        {
            TableData data = new TableData();
            data = _service.LoadUserByRoleId(RoleId);
            return JsonHelper.Instance.Serialize(data);
        }
        public string LoadModuleByRoleId(int page, int limit, string RoleId)
        {
            TableData data = new TableData();
            data = _service.LoadModuleByRoleId(RoleId);
            return JsonHelper.Instance.Serialize(data);
        }
        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string LoadRoleMenu( )
        {
            PageResponse resp = new PageResponse();

            string KeyName = "Menu_" + CurrentUser.ID;
            if (_memoryCacheExtensions.Contains(KeyName))
            {
                resp.Message = _memoryCacheExtensions.Get<string>(KeyName);
            }
            else
            {
                resp.Message = _service.LoadRoleMenu(CurrentUser.RoleID);
                _memoryCacheExtensions.Add<string>(KeyName, resp.Message);
            }
           
            //Json字符串解析成数组
            return JsonHelper.Instance.Serialize(Newtonsoft.Json.Linq.JArray.Parse(resp.Message));
        }
    }
}