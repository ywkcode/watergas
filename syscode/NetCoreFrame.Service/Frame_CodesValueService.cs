 
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Service
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class Frame_CodesValueService : BaseService<Frame_CodesValue>
    {
        /// <summary>
        /// 
        /// </summary>
        protected NetCoreFrameDBContext _dbcontext;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="dbcontext"></param>
        public Frame_CodesValueService(IRepository<Frame_CodesValue> repository, NetCoreFrameDBContext dbcontext) : base(repository)
        {
            _dbcontext = dbcontext;
        }

        public void Add(Frame_CodesValue model)
        {
            model.CodeValueID = GetMaxId();
            _repository.Add(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public void BatchDel(string[] ids)
        {
            List<Frame_CodesValue> list = new List<Frame_CodesValue>();
            foreach (string id in ids)
            {

                _repository.Delete(_repository.FindSingle(s => s.ID == id));
            }
            //_dbcontext.BulkDelete(list);
            // _dbcontext.Frame_CodesValue.Where(s => ids.Contains(s.ID)).BatchDelete();
            //_dbcontext.BulkSaveChanges();
        }

      /// <summary>
      /// 加载
      /// </summary>
      /// <param name="PID"></param>
      /// <returns></returns>
        public TableData Load(int PID)
        {

            return new TableData
            {

                count = _repository.GetCount(null),
                data = (from a in _dbcontext.Frame_Codes.Where(s=>s.CodeID==PID) 
                       join b in _dbcontext.Frame_CodesValue 
                       on a.CodeID equals b.PID 
                       select new { 
                           ID=b.ID,
                           PCodeName=a.CodeName,
                           ItemName = b.ItemName,
                           ItemValue = b.ItemValue,
                           Sort=b.Sort
                       }).OrderByDescending(s=>s.Sort)
            };
        }
        /// <summary>
        /// GetMaxId
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            int MaxDeptID = 1;
            var DeptList = _repository.Find(s => s.IsDeleted == 0).OrderByDescending(s => s.CodeValueID).FirstOrDefault();
            return (DeptList != null) ? DeptList.CodeValueID + 1 : MaxDeptID;
        }
        /// <summary>
        /// 数据字典下拉框
        /// </summary>
        /// <returns></returns>
        public List<SelectListViewModel> GetSelects(string CodeName)
        {
            var list = from a in _dbcontext.Frame_CodesValue.OrderByDescending(s => s.Sort).ThenByDescending(s=>s.CreateTime)
                       join b in _dbcontext.Frame_Codes.Where(s=>s.CodeName== CodeName) on a.PID equals b.CodeID
                     
                       select new SelectListViewModel
                       {
                           Text = a.ItemName,
                           ID = a.ID
                       };
            return list.ToList<SelectListViewModel>();

        }

        /// <summary>
        /// 数据字典下拉框
        /// </summary>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public List<SelectListViewModel> GetSelectsList(string CodeName)
        {
            var list = from a in _dbcontext.Frame_CodesValue.OrderByDescending(s => s.Sort).ThenByDescending(s => s.CreateTime)
                       join b in _dbcontext.Frame_Codes.Where(s => s.CodeName == CodeName) on a.PID equals b.CodeID

                       select new SelectListViewModel
                       {
                           Text = a.ItemName,
                           Value=a.ItemValue
                       };
            return list.ToList<SelectListViewModel>();

        }

        public List<SelectListViewModel> GetSourceList()
        {
            var list = from a in _dbcontext.Frame_Codes.OrderByDescending(s => s.Sort)
                       select new SelectListViewModel
                       {
                           Text = a.CodeName,
                           Value = a.CodeName
                       }; 
            return list.ToList<SelectListViewModel>();

        }
    }
}