using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.Wedrent;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using System;

namespace NetCoreFrame.WebUI.Controllers
{
    public class WGoodsController : Controller
    {
        private readonly WGoodsService _service;

        public WGoodsController(WGoodsService service)
        {
            _service = service;

        }
        public IActionResult WGoods_Index()
        {
            return View();
        }
        public string All(PageRequest request, string GoodsName, string GoodsNumber)
        {
            TableData data = new TableData();
            data = _service.Load(request, GoodsName, GoodsNumber);
            return JsonHelper.Instance.Serialize(data);
        }

        public IActionResult WGoods_Create()
        {
            ViewBag.Guid = Guid.NewGuid().ToString();
            return View();
        }
        public IActionResult WGoods_Edit(string ID)
        {
            var model = _service.Get(ID);
            return View(model);
        }
        public IActionResult WGoods_Detail(string ID)
        {
            var model = _service.Get(ID);
            return View(model);
        }

        [HttpPost]
        public string Create(W_Goods model)
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
        public string Update(W_Goods model)
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

        [HttpGet]
        public string GetNotRentGoods(string weddingDate, string OrderId, string GoodsList)
        {
            PageResponse resp = new PageResponse();
            try
            {
                resp.Result = _service.GetNotRentGoods(Convert.ToDateTime(weddingDate), OrderId, GoodsList);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp.Result);
        }
    }
}
