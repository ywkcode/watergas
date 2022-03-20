using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.WebUI.Views.Hubs;
using Microsoft.AspNetCore.SignalR;
using NetCoreFrame.Entity.Wedrent;
using System.Collections.Generic;

namespace NetCoreFrame.WebUI.Controllers
{
    public class WOrderController : Controller
    {
        private readonly WOrderService _service;
        private readonly WGoodsService _goodservice;
        public WOrderController(WOrderService service, WGoodsService goodservice)
        {
            _service = service;
            _goodservice = goodservice; 
        }
        public IActionResult WOrder_Index()
        {
            return View();
        }
        public string All(PageRequest request,string OrderNumber,string Customer,string GoodsName)
        {
            TableData data = new TableData();
            data = _service.Load(request, OrderNumber,   Customer, GoodsName);
            return JsonHelper.Instance.Serialize(data);
        }

        public IActionResult WOrder_Create()
        {
            ViewBag.Guid = Guid.NewGuid().ToString();
            ViewBag.OrderNumber = DateTime.Now.ToString("yyyyMMddHHmm") + new Random().Next(1000, 9999).ToString();
            return View();
        }

        public IActionResult WOrder_Detail(string ID)
        {
            var model = _service.Get(ID);
            ViewBag.OrderId = model.ID;
            ViewBag.GoodsList = model.GoodsList.TrimStart(',').TrimEnd(',');
            return View(model);
        }
        public IActionResult WOrder_Edit(string ID)
        {
            var model = _service.Get(ID);
            ViewBag.OrderId = model.ID;
            ViewBag.GoodsList = model.GoodsList.TrimStart(',').TrimEnd(',');
            return View(model);
        }
        

        [HttpPost]
        public string Create(W_Order model)
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
                //_goodservice.DeleteGoodsLink(new List<string>(ids)); 
                _service.Delete(ids);
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
        public string Update(W_Order model)
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
