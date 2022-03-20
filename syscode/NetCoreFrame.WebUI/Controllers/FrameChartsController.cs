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
using Newtonsoft.Json;

namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameChartsController : Controller
    {
        private readonly Frame_ChartsService _service;

        public FrameChartsController(Frame_ChartsService service)
        {
            _service = service;
        }
        public IActionResult Frame_Charts_Index()
        {
            return View();
        }
        public string All(PageRequest request, Frame_Charts model)
        {
            TableData data = new TableData();
            data = _service.Load(request, model);
            return JsonHelper.Instance.Serialize(data);
        }
        public IActionResult Frame_Charts_Create()
        {
            return View();
        }

        #region 报表图明细
        public IActionResult ChartsShow(string chartid)
        {
            ViewBag.chartid = chartid;
            var model = _service.Get(chartid);
            return View(model);
        }
        public IActionResult ChartsLineShow(string chartid)
        {
            ViewBag.chartid = chartid;
            var model = _service.Get(chartid);
            return View(model);
        }
        public IActionResult ChartsColumnShow(string chartid)
        {
            ViewBag.chartid = chartid;
            var model = _service.Get(chartid);
            return View(model);
        }
        [HttpPost]
        public string ToDataTable(string chartid)
        { 
            return JsonHelper.Instance.Serialize(_service.ToDataTable(chartid));
        }

        public string PieChart(string chartid)
        {
            return JsonHelper.Instance.Serialize(_service.PieChart(chartid));
        }
        #endregion

        [HttpPost]
        public string Create(Frame_Charts model)
        {
            PageResponse resp = new PageResponse();
           
            model.CreateBy = CurrentUser.UserName;
            _service.Add(model);
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string Delete(string[] ids)
        {

            _service.BatchDelete(ids);
            return JsonHelper.Instance.Serialize(new PageResponse());
        }

        public IActionResult Frame_Charts_Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        [HttpPost]
        public string Update(Frame_Charts model)
        {
            PageResponse resp = new PageResponse();
            _service.Update(model);
            return JsonHelper.Instance.Serialize(resp);
        }

    }
}