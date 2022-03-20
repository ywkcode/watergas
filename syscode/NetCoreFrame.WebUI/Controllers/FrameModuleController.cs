using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Entity.ViewModel;
using System.Collections.Generic;
using NetCoreFrame.WebUI.Filter;
using NetCoreFrame.WebUI.Extensions;

namespace NetCoreFrame.WebUI.Controllers
{
    
    public class FrameModuleController : Controller
    {
        private readonly Frame_ModuleService _service;
        private readonly MemoryCacheExtensions _memoryCacheExtensions;
        public FrameModuleController(Frame_ModuleService service, MemoryCacheExtensions memoryCacheExtensions)
        {
            _service = service;
            _memoryCacheExtensions = memoryCacheExtensions;
        }

        public IActionResult Index()
        {
            List<SelectListViewModel> items = new List<SelectListViewModel> {
                new SelectListViewModel{    Text="北京" },
                new SelectListViewModel{    Text="上海" },
                new SelectListViewModel{    Text="广州" }
            };
            ViewBag.CityList = items;
            return View( );
        }

        public string All(PageDeptRequest request)
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
        public string Create(Frame_Module model)
        {
            PageResponse resp = new PageResponse();
            try
            {
                model.CreateTime = DateTime.Now;
                model.ModuleId = _service.GetMaxDeptId();
                model.ModuleCode = model.ModuleCode + "." + model.ModuleId.ToString();
                _service.Add(model);
                string KeyName = "Menu_" + CurrentUser.ID;
                if (_memoryCacheExtensions.Contains(KeyName))
                {
                    _memoryCacheExtensions.Remove(KeyName);
                }
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
                string KeyName = "Menu_" + CurrentUser.ID;
                if (_memoryCacheExtensions.Contains(KeyName))
                {
                    _memoryCacheExtensions.Remove(KeyName);
                }
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
        public string Update(Frame_Module model)
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

        [HttpPost]
        public string LoadDeptTree()
        {
            PageResponse resp = new PageResponse();
            try
            {
                resp.Message = _service.LoadDeptTree();
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string LoadSelectDeptTree()
        {
            PageResponse resp = new PageResponse();
            try
            {
                resp.Message = _service.LoadSelectDeptTree();
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            //Json字符串解析成数组
            return JsonHelper.Instance.Serialize(Newtonsoft.Json.Linq.JArray.Parse(resp.Message));
        }

        [AllowAnonymous]
        public ActionResult Icons()
        {
            return PartialView("Icons");
        }



        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
         [AllowAnonymous]
        public string LoadRoleMenu()
        {
            PageResponse resp = new PageResponse();
         
            resp.Message = _service.LoadRoleMenu();
            //Json字符串解析成数组
            return JsonHelper.Instance.Serialize(Newtonsoft.Json.Linq.JArray.Parse(resp.Message));
        }
    }
}