 using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.WebUI.Filter;
using Microsoft.Extensions.Logging;

namespace NetCoreFrame.WebUI.Controllers
{
    
    public class FrameDeptController : Controller
    {
        private readonly Frame_DeptService _service;
        private readonly ILogger<FrameDeptController> _logger;
        public FrameDeptController(Frame_DeptService service, ILogger<FrameDeptController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
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
        public string Create(Frame_Dept model)
        {
            PageResponse resp = new PageResponse();
            model.CreateTime = DateTime.Now;
            model.DeptID = _service.GetMaxDeptId();
            model.DeptCode = model.PDeptID.ToString() + "." + model.DeptID.ToString();
            _service.Add(model);
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string Delete(string[] ids)
        {
            PageResponse resp = new PageResponse();
            _service.BatchDelete(ids);
            return JsonHelper.Instance.Serialize(resp);
        }

        public IActionResult Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        [HttpPost]
        public string Update(Frame_Dept model)
        {
            PageResponse resp = new PageResponse();
            _service.Update(model);
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
      
        public string LoadDeptTree()
        {
            PageResponse resp = new PageResponse(); 
            resp.Message = _service.LoadDeptTree();
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost] 
        public string LoadSelectDeptTree()
        {
            PageResponse resp = new PageResponse();
            resp.Message = _service.LoadSelectDeptTree();
            //Json字符串解析成数组
            return JsonHelper.Instance.Serialize(Newtonsoft.Json.Linq.JArray.Parse(resp.Message)); 
        }

      
        public WFReponse GetTree(string companyId)
        {
            WFReponse wFReponse = new WFReponse();
        
            if (string.IsNullOrEmpty(companyId))
            {
                wFReponse.data = _service.GetTree("","");
              
            }
            return wFReponse;
        }
    }
}