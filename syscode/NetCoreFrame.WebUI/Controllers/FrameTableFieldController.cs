using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Entity.FrameEntity;

namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameTableFieldController : Controller
    {
        private readonly Frame_TableFieldService _service;
        private readonly Frame_CodesValueService _codevalueservice;
        public FrameTableFieldController(Frame_TableFieldService service, Frame_CodesValueService  codevalueservice)
        {
            _service = service;
            _codevalueservice = codevalueservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(string TableName,string TableId)
        {
          
            ViewBag.TableName = TableName;
            ViewBag.TableId = TableId;
            ViewBag.SourceList = _codevalueservice.GetSourceList();
            //ViewBag.FieldDisplayTypeList = _codevalueservice.GetSelectsList("字段展现名称");
            return View();
        }
        [HttpPost]
        public string Create(Frame_TableField model)
        {
            PageResponse resp = new PageResponse();
            model.CreateTime = DateTime.Now;
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

        public string All(PageRequest request)
        {
            TableData data = new TableData();
            data = _service.Load(request);
            return JsonHelper.Instance.Serialize(data);
        }

        public string LoadFieldList(int page, int limit, int TableId)
        {
            TableData data = new TableData();
            data = _service.LoadFieldList(page, limit, TableId);
            return JsonHelper.Instance.Serialize(data);
        }
    }
}