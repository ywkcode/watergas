using System;
using System.Linq;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core.CommonHelper;

namespace NetCoreFrame.WebUI.Controllers
{
    public class FrameUserController : Controller
    {
        private readonly Frame_UserService _service;
        private readonly Frame_RoleService _roleservice;
        private readonly Frame_DeptService _deptservice;
        public FrameUserController(Frame_UserService service, Frame_RoleService roleservice, Frame_DeptService deptservice)
        {
            _service = service;
            _roleservice = roleservice;
            _deptservice = deptservice;
        }
        #region 查询
        public IActionResult Index()
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
        public IActionResult Create()
        {
            ViewBag.Rolelist = _roleservice.GetSelects();
            return View();
        }

     
        [HttpPost]
        public string Create(Frame_User model)
        {
            PageResponse resp = new PageResponse();
            _service.CreateAccount(model);
            return JsonHelper.Instance.Serialize(resp);
        }
        #endregion

        #region 修改
        public IActionResult Edit(string id)
        {
            var model = _service.Get(id);
            return View(model);
        }
        [HttpPost]
        public string Edit(Frame_User model)
        {
            PageResponse resp = new PageResponse();
            try
            {
                var updMol = _service.Get(model.ID);
                updMol.LoginID = model.LoginID;
                updMol.Password = MD5Helper.Get32MD5(model.Password);
                _service.Update(updMol);
            }
            catch (Exception e)
            {
                resp.Status = false;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }
       
        #endregion

        #region 删除
        [HttpPost]
        public string Delete(string[] ids)
        {
            PageResponse resp = new PageResponse();
            _service.BatchDelete(ids);
            return JsonHelper.Instance.Serialize(resp);
        }
        #endregion

        public WFReponse GetTree(string departmentId)
        {
            WFReponse wFReponse = new WFReponse();

            wFReponse.data = _service.GetList("");
            return wFReponse;
        }

        #region 树显示
        [HttpPost]
        public string LoadSelectTree()
        {
            PageResponse resp = new PageResponse();
            resp.Message = _service.LoadSelectTree();
            //Json字符串解析成数组
            return JsonHelper.Instance.Serialize(Newtonsoft.Json.Linq.JArray.Parse(resp.Message));
        }
        #endregion
    }
}