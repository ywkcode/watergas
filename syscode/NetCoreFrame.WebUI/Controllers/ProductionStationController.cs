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

namespace NetCoreFrame.WebUI.Controllers
{
    public class ProductionStationController : Controller
    {
        private readonly productionstationService _service;
        private readonly IHubContext<SignalrHubs> _countHub;
        public ProductionStationController(productionstationService service, IHubContext<SignalrHubs> countHub)
        {
            _service = service;
            _countHub = countHub;
        }
        public IActionResult ProductionStation_Index()
        {
            return View();
        }
        public string All(PageRequest request)
        {
            TableData data = new TableData();
            data = _service.Load(request);
            return JsonHelper.Instance.Serialize(data);
        }

        public IActionResult ProductionStation_Create()
        {
            ViewBag.Guid = Guid.NewGuid().ToString();
            return View();
        }
        public IActionResult ProductionStation_Edit(string ID)
        {
            var model = _service.Get(ID);
            return View(model);
        }
        

        [HttpPost]
        public string Create(ProductionStation model)
        {
            PageResponse resp = new PageResponse();
            try
            {

                model.CreateBy = CurrentUser.UserName;
                _service.Add(model);
                _countHub.Clients.All.SendAsync("ReceiveImage", model.IPNum);
                 
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
        public string Update(ProductionStation model)
        {
            PageResponse resp = new PageResponse();
            try
            {
                _service.Update(model);
                _countHub.Clients.All.SendAsync("ReceiveImage", model.IPNum);
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
