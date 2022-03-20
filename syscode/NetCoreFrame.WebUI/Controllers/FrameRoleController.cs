using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameRoleController : Controller
    {
        private readonly Frame_RoleService _service;

        public FrameRoleController(Frame_RoleService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AuthorityIndex()
        {
            return View();
        }
        public string All(PageRequest request)
        {
            TableData data = new TableData();
            data = _service.Load(request);
            return JsonHelper.Instance.Serialize(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public string Create(Frame_Role model)
        {
            PageResponse resp = new PageResponse();
            try
            {
               
                model.CreateBy = CurrentUser.UserName; 
                _service.Add(model);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string Delete(string[] ids)
        {
            PageResponse resp = new PageResponse();
            try
            {
                _service.BatchDelete(ids);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

        public IActionResult Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        [HttpPost]
        public string Update(Frame_Role model)
        {
            PageResponse resp = new PageResponse();
            try
            {
                _service.Update(model);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

    }
}