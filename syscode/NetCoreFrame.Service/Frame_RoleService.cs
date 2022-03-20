using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Repository.Interface;


namespace NetCoreFrame.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class Frame_RoleService : BaseService<Frame_Role>
    {
      
        private NetCoreFrameDBContext _dbContext;
        public Frame_RoleService(IRepository<Frame_Role> repository 
            , NetCoreFrameDBContext dBContext) : base(repository)
        {
           
            _dbContext = dBContext;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(Frame_Role role)
        {
            _repository.Add(role);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request)
        {

          
            return new TableData
            { 
                count = _repository.GetCount(null),
                data = _repository.Find(request.page, request.limit, "CreateTime desc") 
            };
        }
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <returns></returns>
        public List<SelectListViewModel> GetSelects()
        {
            var list = from a in _dbContext.Frame_Role.Where(s => s.IsDeleted == 0).OrderByDescending(s => s.Sort)
                       select new SelectListViewModel
                       {
                           Text = a.RoleName,
                           ID = a.ID.ToString()
                       }; 
            return list.ToList<SelectListViewModel>();
        }

    }
}
