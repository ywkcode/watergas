using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.Water;
using NetCoreFrame.Entity.Wedrent;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using System;

namespace NetCoreFrame.WebUI.Controllers
{
    public class WaterEquipmentController : Controller
    {
        private readonly WaterEquipmentService _service;

        public WaterEquipmentController(WaterEquipmentService service)
        {
            _service = service;

        }
        #region 列表
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult WaterEquipment_Index()
        {
            return View();
        }

        public string All(PageRequest request)
        {
            TableData data = new TableData();
            data = _service.Load(request);
            return JsonHelper.Instance.Serialize(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public IActionResult WaterEquipment_Create()
        {
            ViewBag.Guid = Guid.NewGuid().ToString();
            return View();
        }

        [HttpPost]
        public string Create(Water_Equipment model)
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
        #endregion

        #region 批量删除
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
        #endregion
    }
}
