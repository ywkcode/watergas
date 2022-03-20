using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.Enum;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Service
{
    public class Frame_RelationsService : BaseService<Frame_Relations>
    {
        
        private NetCoreFrameDBContext _dbContext;
        public Frame_RelationsService(IRepository<Frame_Relations> repository,
           NetCoreFrameDBContext dBContext) : base(repository)
        {
           
            _dbContext = dBContext;
        }

        public void Add(Frame_Relations model)
        {
            _repository.Add(model);
        }
        #region 关系表删除
        public void Delete(string FirstId, string SecondId, string RelationType)
        {
            if (RelationType == "RoleModule")
            {
                DeleteInner(FirstId, SecondId);
                //如果当前模块下有子节点，删除所有子节点下的数据 直到count=0
                var modulelist = _dbContext.Frame_Module.ToList();
                var currMol = modulelist.Find(s => s.ID == SecondId);
                var childlist = modulelist.Where(s => s.PModuleId == currMol.ModuleId).ToList();
                DelEach(childlist, FirstId);
            }
            else
            {
                DeleteInner(FirstId, SecondId);
            }

        }


        /// <summary>
        /// 递归删除
        /// </summary>
        /// <param name="list"></param>
        /// <param name="FirstId"></param>
        protected void DelEach(List<Frame_Module> list, string FirstId)
        {
            foreach (var child in list)
            {
                DeleteInner(FirstId, child.ID);
                var childlist = _dbContext.Frame_Module.Where(s => s.PModuleId == child.ModuleId).ToList();
                while (childlist.Count() > 0)
                {
                    DelEach(childlist, FirstId);
                }
            }
        }



        public void DeleteInner(string FirstId, string SecondId)
        {
            var model = _repository.FindSingle(s => s.FirstId == FirstId && s.SecondId == SecondId);
            if (model != null)
            {
                _repository.Delete(model);
            }
        }
        #endregion

        #region 关系表新增
        /// <summary>
        /// 关系表新增
        /// </summary>
        /// <param name="FirstId"></param>
        /// <param name="SecondId"></param>
        /// <param name="RelationType"></param>
        public void Add(string FirstId, string SecondId, string RelationType)
        {
            if (RelationType == "RoleModule")
            {
                var modulelist = _dbContext.Frame_Module.ToList();
                var Relationlist = _dbContext.Frame_Relations.ToList();

                //找当前节点的父节点  父节点不等于0 循环
                var currModule = modulelist.Where(s => s.ID == SecondId).FirstOrDefault();
                var PModuleId = currModule.PModuleId;
                AddInner(FirstId, SecondId, RelationType);

                while (PModuleId != 0)
                {
                    var PModule = modulelist.Where(s => s.ModuleId == PModuleId).FirstOrDefault();
                    PModuleId = PModule.PModuleId;
                    AddInner(FirstId, PModule.ID, RelationType);
                }
            }

            else
            {
                AddInner(FirstId, SecondId, RelationType);
            }


        }
        /// <summary>
        /// 添加关系表
        /// </summary>
        /// <param name="FirstId"></param>
        /// <param name="SecondId"></param>
        /// <param name="RelationType"></param>
        public void AddInner(string FirstId, string SecondId, string RelationTypeStr)
        {
            var model = _repository.FindSingle(s => s.FirstId == FirstId && s.SecondId == SecondId);
            RelationType type = (RelationType)Enum.Parse(typeof(RelationType), RelationTypeStr) ;
            if (model == null)
            {
                this.Add(new Frame_Relations
                {
                    FirstId = FirstId,
                    SecondId = SecondId,
                    RelationType = type
                });
            }
        }
        #endregion

        public TableData LoadUserByRoleId(string roleid)
        {
            var userlist = from a in _dbContext.Frame_User
                           join b in _dbContext.Frame_Relations.Where(s => s.RelationType == RelationType.RoleUser && s.FirstId == roleid)

                           on a.ID equals b.SecondId into temp
                           from tt in temp.DefaultIfEmpty()
                           select new
                           {
                               UserName = a.UserName,
                               LoginID = a.LoginID,
                               IsCheck = (tt.SecondId != null ? "1" : "0"),
                               CreateTime = a.CreateTime,
                               ID = a.ID
                           };
            return new TableData
            {

                count = userlist.Count(),
                data = userlist.OrderByDescending(s => s.CreateTime)
            };

        }

        public TableData LoadFileByStationId(string StationId)
        {
            var userlist = from a in _dbContext.Frame_FileUpload
                           join b in _dbContext.Frame_Relations.Where(s => s.RelationType == RelationType.Station && s.FirstId == StationId)

                           on a.ID equals b.SecondId into temp
                           from tt in temp.DefaultIfEmpty()
                           select new
                           {
                               FileName = a.FileName,
                               FileImgPath = a.FileImgUrl,
                               IsCheck = (tt.SecondId != null ? "1" : "0"),
                               CreateTime = a.CreateTime,
                               ID = a.ID
                           };
            return new TableData
            {

                count = userlist.Count(),
                data = userlist.OrderByDescending(s => s.CreateTime)
            };

        }

        public TableData LoadModuleByRoleId(string roleid)
        {
            var userlist = from a in _dbContext.Frame_Module
                           join b in _dbContext.Frame_Relations.Where(s => s.RelationType == RelationType.RoleModule && s.FirstId == roleid)

                           on a.ID equals b.SecondId into temp
                           from tt in temp.DefaultIfEmpty()
                           select new
                           {
                               IsCheck = (tt.SecondId != null ? "1" : "0"),
                               ModuleId = a.ModuleId,
                               IsThird = (_dbContext.Frame_Module.Where(s => s.PModuleId == a.ModuleId).Count() > 0 ? 0 : 1)
                           };
            return new TableData
            {
                code = 200,
                count = userlist.Count(),
                data = userlist
            };

        }

        /// <summary>
        /// 根据登录人角色ID分配模块查看权限
        /// </summary>
        /// <returns></returns>
        public string LoadRoleMenu(string RoleID)
        {
            
            string moduleStr = string.Empty;

            var baselist = from a in _dbContext.Frame_Relations.Where(s => s.RelationType == RelationType.RoleModule && s.FirstId == RoleID)
                           join b in _dbContext.Frame_Module.Where(s => s.IsDeleted == 0).OrderBy(s => s.ModuleId) on
                           a.SecondId equals b.ID
                           select b;
            //LogHelper.WriteLogs("LoadRoleMenu baselist：" + baselist.ToString());
            //获取首个模块信息 PModuleId=0 不能修改 二级目录 .0.1.*
            var TopModuleList = baselist.Where(s => s.PModuleId==1).ToList();
            moduleStr += "[";
            foreach (var module in TopModuleList)
            {
                moduleStr += "{title:\"" + module.Name + "\",icon:\"" + module.IconName + "\",href:\"" + module.Url + "\",spread:true,children:[";
                moduleStr += GetMenuChild(module.ModuleId, baselist.ToList());
                moduleStr += "]},";
            }
            moduleStr = moduleStr.TrimEnd(',');
            moduleStr += "]";
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
