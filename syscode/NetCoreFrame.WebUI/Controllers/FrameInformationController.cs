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
using Microsoft.AspNetCore.SignalR;
using NetCoreFrame.WebUI.Views.Hubs;


namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameInformationController : Controller
    {
        private readonly Frame_InformationService _service;
        private readonly IHubContext<SignalrHubs> _countHub;

        public FrameInformationController(Frame_InformationService service, IHubContext<SignalrHubs> countHub)
        {
            _service = service;
            _countHub = countHub;


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FrameInformation_Index()
        {
            return View();
        }
        public IActionResult FrameInformation_Create()
        {
            return View();
        }
        public IActionResult FrameInformation_Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        public IActionResult LiveIndex()
        {
            return View();
        }


        public string All(PageRequest request)
        {
            TableData data = new TableData();

            data = _service.Load(request,new Frame_Information() { });
            return JsonHelper.Instance.Serialize(data);
        }
        public string LiveAll(PageRequest request)
        {
            TableData data = new TableData();
            data = _service.Load(request, new Frame_Information() { CategoryName="方寸直播"});
            return JsonHelper.Instance.Serialize(data);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create2()
        {
            return View();
        }

        [HttpPost]
        public string FrameInformation_Create(
            string CategoryName,string CreateTime,string FileContent,string FileContentEn,string FileTitle,string FileTitleEn,string ImgAttachID)
        {
            PageResponse resp = new PageResponse();
            Frame_Information model = new Frame_Information();
            model.CategoryName = CategoryName;
            model.CreateTime = Convert.ToDateTime(CreateTime);
            model.FileContent = FileContent;
            model.FileContentEn = FileContentEn;
            model.FileTitle = FileTitle;
            model.FileTitleEn = FileTitleEn;
            model.ImgAttachID = ImgAttachID;
            model.CreateBy = CurrentUser.UserName;

            if (model != null)
            {
                _service.Add(model);
            } 
            return JsonHelper.Instance.Serialize(resp);
        }

        [HttpPost]
        public string FrameInformation_Edit(
            string ID,
            string CategoryName, string CreateTime, string FileContent, string FileContentEn, string FileTitle, string FileTitleEn, string ImgAttachID)
        {
            PageResponse resp = new PageResponse();
            Frame_Information model =_service.Get(ID);
            model.CategoryName = CategoryName;
            model.CreateTime = Convert.ToDateTime(CreateTime);
            model.FileContent = FileContent;
            model.FileContentEn = FileContentEn;
            model.FileTitle = FileTitle;
            model.FileTitleEn = FileTitleEn;
            model.ImgAttachID = ImgAttachID;
            model.UpdateBy = CurrentUser.UserName;
            model.UpdateTime = DateTime.Now;
            _service.Update(model);
            return JsonHelper.Instance.Serialize(resp);
        }
         
        [HttpPost]
        public string Delete(string[] ids)
        {

            _service.BatchDelete(ids);
            return JsonHelper.Instance.Serialize(new PageResponse());
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public string Push(string[] ids)
        {
            foreach (var idstr in ids)
            {
                var info = _service.Get(idstr);
              
                if (info != null)
                {
                    info.DataStatus = Entity.Enum.DataStatus.Publish;
                    _service.Update(info);
                    _countHub.Clients.All.SendAsync("ReceiveMessage", info.FileContent,info.FileTitle);
                }
               
            }
            return JsonHelper.Instance.Serialize(new PageResponse());
        }

        public IActionResult Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        [HttpPost]
        public string Update(Frame_Information model)
        {
            PageResponse resp = new PageResponse();
            _service.Update(model);
            return JsonHelper.Instance.Serialize(resp);
        }

    }
}