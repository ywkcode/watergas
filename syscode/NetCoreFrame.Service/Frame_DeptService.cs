using NetCoreFrame.Core.ApiDto;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Core.Utils;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreFrame.Service
{

    public class Frame_DeptService : BaseService<Frame_Dept>
    {

        private NetCoreFrameDBContext _dbContext;
        public Frame_DeptService(IRepository<Frame_Dept> repository
            , NetCoreFrameDBContext dBContext) : base(repository)
        {

            _dbContext = dBContext;
        }

        public void Add(Frame_Dept dept)
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


            return new TableData
            {

                count = _repository.GetCount(null),
                data = (request.DeptId > 0) ? _repository.Find(request.page, request.limit, "DeptID desc", s => s.DeptID == request.DeptId || s.PDeptID == request.DeptId) :
                _repository.Find(request.page, request.limit, "DeptID desc")
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
            var DeptList = _dbContext.Frame_Dept.Where(s => s.IsDeleted == 0).ToList();
            //获取收个部门信息 PDeptID=0 不能修改
            var TopDept = DeptList.Where(s => s.PDeptID == 0).FirstOrDefault();
            sb.Append("[{title:\"" + TopDept.DeptName + "\",id:" + TopDept.DeptID + ",spread:true,children:[");
            sb.Append(GetDeptChild(TopDept.DeptID, DeptList, "Single"));
            sb.Append("]}]");
            return sb.ToString();
        }

        protected string GetDeptChild(int DeptId, List<Frame_Dept> list, string Type)
        {
            StringBuilder sb = new StringBuilder();
            var CheckList = list;
            var OutCheckList = list;
            int deptChildCount = list.Where(s => s.PDeptID == DeptId).Count();

            while (deptChildCount > 0)
            {
                foreach (var dept in list.Where(s => s.PDeptID == DeptId))
                {
                    if (Type == "Single")
                    {
                        sb.Append("{title:\"" + dept.DeptName + "\",id:" + dept.DeptID + ",spread:true");
                        if (list.Where(s => s.PDeptID == dept.DeptID).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.DeptID, list, Type) + "]");
                        }
                    }
                    else
                    {
                        sb.Append("{name:\"" + dept.DeptName + "\",id:" + dept.DeptID + ",open:true");
                        if (list.Where(s => s.PDeptID == dept.DeptID).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.DeptID, list, Type) + "]");
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
            var DeptList = _dbContext.Frame_Dept.Where(s => s.IsDeleted == 0).ToList();
            //获取收个部门信息 PDeptID=0 不能修改
            var TopDept = DeptList.Where(s => s.PDeptID == 0).FirstOrDefault();
            sb.Append("[{name:\"" + TopDept.DeptName + "\",id:" + TopDept.DeptID + ",open:true,children:[");
            sb.Append(GetDeptChild(TopDept.DeptID, DeptList, "Select"));
            sb.Append("]}]");
            return sb.ToString();
            //return "{name:\"部门1\",id:1,open:true}";
        }

        /// <summary>
        /// 用于流程图的部门下拉选择
        /// </summary>
        /// <returns></returns>

        //public string GetTreeInFlow()
        //{
        //    //StringBuilder sb = new StringBuilder();
        //    //var DeptList = _dbContext.Frame_Dept.Where(s => s.IsDeleted == 0).ToList();
        //    ////获取收个部门信息 PDeptID=0 不能修改
        //    //var TopDept = DeptList.Where(s => s.PDeptID == 0).FirstOrDefault();
        //    //sb.Append("[{\"text\":\"" + TopDept.DeptName + "\",\"values\":\"" + TopDept.DeptName+ "\", \"id\":" + TopDept.DeptID + ",\"open\":true,\"hasChildren\":true,\"isexpand\":true,\"complete\":true,\"checkstate\":0, \"ChildNodes\":[");
        //    //sb.Append(GetDeptChildInFlow(TopDept.DeptID, DeptList, "Single"));
        //    //sb.Append("]}]");
        //    //return sb.ToString();

        //}
        protected string GetDeptChildInFlow(int DeptId, List<Frame_Dept> list, string Type)
        {
            StringBuilder sb = new StringBuilder();
            var CheckList = list;
            var OutCheckList = list;
            int deptChildCount = list.Where(s => s.PDeptID == DeptId).Count();

            while (deptChildCount > 0)
            {
                foreach (var dept in list.Where(s => s.PDeptID == DeptId))
                {
                    if (Type == "Single")
                    {
                        sb.Append("{\"text\":\"" + dept.DeptName + "\",\"id\":" + dept.DeptID + ",\"isexpand\":true");
                        if (list.Where(s => s.PDeptID == dept.DeptID).Count() > 0)
                        {
                            sb.Append(",\"ChildNodes\":[" + GetDeptChildInFlow(dept.DeptID, list, Type) + "]");
                        }
                    }
                    else
                    {
                        sb.Append("{name:\"" + dept.DeptName + "\",id:" + dept.DeptID + ",open:true");
                        if (list.Where(s => s.PDeptID == dept.DeptID).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChildInFlow(dept.DeptID, list, Type) + "]");
                        }
                    }

                    sb.Append("},");
                    deptChildCount--;
                }
            }
            return sb.ToString().TrimEnd(',');
        }


        public List<TreeModel> GetTree(string companyId, string parentId)
        {

           
            List<DepartmentDto> list = (from a in _dbContext.Frame_Dept.Where(s => s.PDeptID == 1)
                                        select new DepartmentDto
                                        {
                                            F_CompanyId = "1111",
                                            F_DepartmentId = a.ID,
                                            F_FullName = a.DeptName,
                                            F_ParentId = a.PDeptID.ToString(),
                                            F_ShortName = a.DeptName,


                                        }).ToList();

            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel
                {
                    id = item.F_DepartmentId,
                    text = item.F_FullName,
                    value = item.F_DepartmentId,
                    showcheck = false,
                    checkstate = 0,
                    isexpand = true,
                    parentId = item.F_ParentId
                };

                treeList.Add(node);
            }
            return treeList.ToTree(parentId);

        }
        /// <summary>
        /// 获取当前部门的最大DeptID
        /// </summary>
        /// <returns></returns>
        public int GetMaxDeptId()
        {

            var DeptList = _dbContext.Frame_Dept.OrderByDescending(s => s.DeptID).FirstOrDefault();
            return DeptList.DeptID + 1;
        }
    }
}
