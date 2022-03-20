using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Service
{
    public class Frame_ModuleService : BaseService<Frame_Module>
    {
      
        private NetCoreFrameDBContext _dbContext;
        public Frame_ModuleService(IRepository<Frame_Module> repository ,NetCoreFrameDBContext dBContext) : base(repository)
        {
           
            _dbContext = dBContext;
        }

        public void Add(Frame_Module dept)
        {
            _repository.Add(dept);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageDeptRequest request)
        {

           
            //var propertyStr = string.Join(",", properties.Select(u => u.Key));
            if (string.IsNullOrEmpty(request.ModuleCode))
            {
                request.ModuleCode = ".0.1";
            }
            int ModuleCodeLen = request.ModuleCode.Length;
            return new TableData
            {

                count = _repository.GetCount(null),
                data = (!string.IsNullOrEmpty(request.ModuleCode)) ? _repository.Find(request.page, request.limit, "ModuleId desc", s => s.ModuleCode.Contains(request.ModuleCode)).Where(s => s.ModuleCode.Length >= ModuleCodeLen) : _repository.Find(request.page, request.limit, "ModuleId desc")
                //data= _repository.Find(request.page, request.limit, "ModuleId desc")
            };
        }
        public TableData Load(PageRequest request)
        {
             
            return new TableData
            {

                count = _repository.GetCount(null),
                data = _repository.Find(request.page, request.limit, "DeptID desc")

            };
        }
        public string LoadDeptTree()
        {
            StringBuilder sb = new StringBuilder();
            var DeptList = _dbContext.Frame_Module.Where(s => s.IsDeleted == 0).ToList();
            //获取收个部门信息 PDeptID=0 不能修改
            var TopDept = DeptList.Where(s => s.PModuleId == 0).FirstOrDefault();
            sb.Append("[{title:\"" + TopDept.Name + "\",id:" + TopDept.ModuleId + ",treecode:\"" + TopDept.ModuleCode + "\",treeid:\"" + TopDept.ID + "\",spread:true,children:[");
            sb.Append(GetDeptChild(TopDept.ModuleId, DeptList, "Single"));
            sb.Append("]}]");
            return sb.ToString();
        }

        protected string GetDeptChild(int DeptId, List<Frame_Module> list, string Type)
        {
            StringBuilder sb = new StringBuilder();
            var CheckList = list;
            var OutCheckList = list;
            int deptChildCount = list.Where(s => s.PModuleId == DeptId).Count();

            while (deptChildCount > 0)
            {
                foreach (var dept in list.Where(s => s.PModuleId == DeptId))
                {
                    if (Type == "Single")
                    {
                        sb.Append("{title:\"" + dept.Name + "\",id:" + dept.ModuleId + ",treecode:\"" + dept.ModuleCode + "\",treeid:\"" + dept.ID + "\",spread:true");
                        if (list.Where(s => s.PModuleId == dept.ModuleId).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.ModuleId, list, Type) + "]");
                        }
                    }
                    else
                    {
                        sb.Append("{name:\"" + dept.Name + "\",id:" + dept.ModuleId + ",treecode:\"" + dept.ModuleCode + "\",treeid:\"" + dept.ID + "\",open:true");
                        if (list.Where(s => s.PModuleId == dept.ModuleId).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.ModuleId, list, Type) + "]");
                        }
                    }

                    sb.Append("},");
                    deptChildCount--;
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        public string LoadSelectDeptTree()
        {
            StringBuilder sb = new StringBuilder();
            var DeptList = _dbContext.Frame_Module.Where(s => s.IsDeleted == 0).ToList();
            //获取收个部门信息 PDeptID=0 不能修改
            var TopDept = DeptList.Where(s => s.PModuleId == 0).FirstOrDefault();
            sb.Append("[{name:\"" + TopDept.Name + "\",id:" + TopDept.ModuleId + ",treecode:\"" + TopDept.ModuleCode + "\",open:true,children:[");
            sb.Append(GetDeptChild(TopDept.ModuleId, DeptList, "Select"));
            sb.Append("]}]");
            return sb.ToString();
        }

        /// <summary>
        /// 获取当前部门的最大DeptID
        /// </summary>
        /// <returns></returns>
        public int GetMaxDeptId()
        {
            int MaxDeptID = 1;
            var DeptList = _dbContext.Frame_Module.Max(s => s.ModuleId);
            return (DeptList != 1) ? DeptList + 1 : MaxDeptID;
        }

        /// <summary>
        /// 角色模块加载  需要获取当前登录人的角色
        /// </summary>
        /// <returns></returns>
        public string LoadRoleMenu()
        {
            LogHelper.WriteLogs("进入 LoadRoleMenu");
           //StringBuilder sb = new StringBuilder();
            string moduleStr = string.Empty;
            var MenuList = _repository.Find(s => s.IsDeleted == 0).ToList();
            //获取首个模块信息 PModuleId=0 不能修改 二级目录 .0.1.*
            var TopModuleList = MenuList.Where(s => s.ModuleCode.Length == 6).ToList();
            //sb.Append("[");
            //sb.Append("[{title:\"" + TopMenu.Name + "\",icon:\"" + TopMenu.IconName + "\",href:\"" + TopMenu.Url + "\",spread:true,children:[");
            //sb.Append(GetMenuChild(TopMenu.ModuleId, MenuList));
            //sb.Append("]}]");

            moduleStr += "[";
            foreach (var module in TopModuleList)
            {
                moduleStr += "{title:\"" + module.Name + "\",icon:\"" + module.IconName + "\",href:\"" + module.Url + "\",spread:true,children:[";
                moduleStr += GetMenuChild(module.ModuleId, MenuList);
                moduleStr += "]},";
            }
            moduleStr = moduleStr.TrimEnd(',');
            moduleStr += "]";
            LogHelper.WriteLogs("moduleStr:"+ moduleStr);
            return moduleStr;
        }

        /// <summary>
        /// 子模块递归
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetMenuChild(int ModuleId, List<Frame_Module> list)
        {
            StringBuilder sb = new StringBuilder();
            var CheckList = list;
            var OutCheckList = list;
            int menuChildCount = list.Where(s => s.PModuleId == ModuleId).Count();

            while (menuChildCount > 0)
            {
                foreach (var module in list.Where(s => s.PModuleId == ModuleId))
                {
                    sb.Append("{title:\"" + module.Name + "\",icon:\"" + module.IconName + "\",href:\"" + module.Url + "\",spread:true");
                    if (list.Where(s => s.PModuleId == module.ModuleId).Count() > 0)
                    {
                        sb.Append(",children:[" + GetMenuChild(module.ModuleId, list) + "]");
                    }
                    sb.Append("},");
                    menuChildCount--;
                }
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
