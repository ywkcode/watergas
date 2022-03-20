using System;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameCodesController : Controller
    {
        private readonly Frame_CodesService _service;
        private readonly Frame_CodesValueService _valueservice;

        public FrameCodesController(Frame_CodesService service, Frame_CodesValueService valueservice)
        {
            _service = service;
            _valueservice = valueservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 字典明细设置
        /// </summary>
        /// <returns></returns>
        public IActionResult ValueIndex(int CodeID)
        {
            ViewBag.CodeID = CodeID;
            return View();
        }
        public string All(PageRequest request,Frame_Codes codesMol)
        {
            TableData data = new TableData();
            data = _service.Load(request, codesMol);
            return JsonHelper.Instance.Serialize(data);
        }

        public string ValueAll(int CodeID, PageRequest request)
        {
            TableData data = new TableData();
            data = _valueservice.Load(CodeID);
            return JsonHelper.Instance.Serialize(data);
        }
        [HttpPost]
        public string UpdateCodeValue(string ZDName,string ZDCode,string ID )
        {
            PageResponse resp = new PageResponse();
            try
            {
                var model = _valueservice.Get(ID);
                if (model != null)
                {
                    switch (ZDName)
                    {
                        case "ItemName":
                            model.ItemName = ZDCode;
                            break;
                        case "ItemValue":
                            model.ItemValue = ZDCode;
                            break;
                        case "Sort":
                            model.Sort = Convert.ToInt32(ZDCode);
                            break;
                        default:break;
                    }
                }
                _valueservice.Update(model);
            }
            catch (Exception e)
            {
                resp.Status = false;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public string Create(Frame_Codes model)
        {
            PageResponse resp = new PageResponse();
            _service.Add(model);
            return JsonHelper.Instance.Serialize(resp);
        }
        [HttpPost]
        public string CreateValue(int PID)
        {
            PageResponse resp = new PageResponse();
            try
            {
                _valueservice.Add(new Frame_CodesValue
                {
                    PID = PID,
                    ItemName = "",
                    ItemValue = ""
                });
                resp.Message = PID.ToString();
            }
            catch (Exception e)
            {
                resp.Status = false;
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

                foreach (string id in ids)
                {
                  
                    _service.Delete(id);
                }
               
            }
            catch (Exception e)
            {
                resp.Status = false;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }
        [HttpPost]
        public string DeleteValue(string[] ids)
        {
            PageResponse resp = new PageResponse();
            try
            {

                _valueservice.BatchDel(ids);
            }
            catch (Exception e)
            {
                resp.Status = false;
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
        public string Update(Frame_Codes model)
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