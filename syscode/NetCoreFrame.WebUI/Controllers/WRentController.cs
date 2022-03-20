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

namespace NetCoreFrame.WebUI.Controllers
{
    public class WRentController : Controller
    {
        private readonly WrentService _service; 
        public WRentController(WrentService service )
        {
            _service = service;
          
        }
        public IActionResult WRent_Index()
        {
            return View();
        }

        public IActionResult WRent_DetailIndex(string ID)
        {
            ViewBag.ID = ID;
            return View();
        }
        public string All(PageRequest request,string OrderNumber,string GoodsNumber)
        {
            TableData data = new TableData();
            data = _service.Load(request,   OrderNumber,   GoodsNumber);
            return JsonHelper.Instance.Serialize(data);
        }
        public string DetailAll(PageRequest request,string ID)
        {

            TableData data = new TableData();
            data = _service.DetailLoad(request, ID);
            return JsonHelper.Instance.Serialize(data);
        }
        
        [HttpPost]
        public string Rent(string[] ids,bool IsReturnBack)
        {
            PageResponse resp = new PageResponse();
            try
            {
                foreach (string id in ids)
                {
                    _service.UpdateReturnStatus(id, IsReturnBack);
                }
              
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }



        public IActionResult WRent_Create()
        {
            ViewBag.Guid = Guid.NewGuid().ToString();
            return View();
        }
        public IActionResult WRent_Edit(string ID)
        {
            var model = _service.Get(ID);
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

    }
}
