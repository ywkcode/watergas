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
using NetCoreFrame.WebUI.Filter;
using NetCoreFrame.WebUI.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace NetCoreFrame.WebUI.Controllers
{
    [CustomActionCheckFilter]
    public class FrameTableInfoController : Controller
    {
        private readonly Frame_TableInfoService _service;
        
        private readonly ILogger<FrameTableInfoController> _logger;

        private readonly IMemoryCache _cache;

        private readonly MemoryCacheExtensions _memoryCacheExtensions;
        public FrameTableInfoController(Frame_TableInfoService service ,
            ILogger<FrameTableInfoController> logger, IMemoryCache cache, MemoryCacheExtensions memoryCacheExtensions)
        {
            _service = service;
            _cache = cache;
             _logger = logger;
            _memoryCacheExtensions =  memoryCacheExtensions;
        }
        public IActionResult Index()
        {
            //if (_memoryCacheExtensions.Get<Frame_User>("Test2") != null)
            //{

             
            //    var test2 = _memoryCacheExtensions.Get<Frame_User>("Test2");
            //    _logger.LogError($"读取Cache:" + test2.UserName);
            //}
            //else
            //{
            //    _logger.LogError($"创建Cache:");
              
            //    _memoryCacheExtensions.Add<Frame_User>("Test2", new Frame_User() { UserName = "张三" });
            //    var test = _cache.Get<Frame_User>("Test");
            //}

            return View();
        }

        public IActionResult Create()
        {
          
            var test2 = _memoryCacheExtensions.Get<Frame_User>("Test2");
            return View();
        }
        [HttpPost]
        public string Create(Frame_TableInfo model)
        {
            PageResponse resp = new PageResponse();
            model.CreateTime = DateTime.Now;
            model.TableId = _service.GetMaxTabId();
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
        [HttpPost]
        public string GeneratePage(int TableID)
        {
            PageResponse resp = new PageResponse();
            _service.GeneratePage(TableID);
            return JsonHelper.Instance.Serialize(resp);
        }
    }
}