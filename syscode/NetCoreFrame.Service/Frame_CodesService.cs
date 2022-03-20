using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Core.Utils;
using NetCoreFrame.Entity.Enum;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;
 
namespace NetCoreFrame.Service
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class Frame_CodesService : BaseService<Frame_Codes>
    {
       
        public Frame_CodesService(IRepository<Frame_Codes> repository) : base(repository)
        {
             
        }

        public void Add(Frame_Codes model)
        {
            model.CodeID = GetMaxId();
            _repository.Add(model);
        }
        public void Update(string ID)
        {
            _repository.BatchUpdate(s => s.ID == ID, s => new Frame_Codes { IsDeleted = Entity.Enum.TrueOrFlase.Flase });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(string ID)
        {
            var model = _repository.FindSingle(s => s.ID == ID);
            if (model != null)
            {
                _repository.Delete(model);
            } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="codename"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request,Frame_Codes codesMol)
        {
            
            Expression<Func<Frame_Codes, bool>> expression = t => true;  
            if (!string.IsNullOrEmpty(codesMol.CodeName))
            {
                expression = expression.And(t => t.CodeName.Contains(codesMol.CodeName)); 
            }
            var datalist = _repository.Find(request.page, request.limit, "ID desc",expression);
            return new TableData
            {

                count = datalist.Count(),
                data = datalist
            };
        }

        /// <summary>
        /// 
        /// </summaryGetMaxId>
        /// <returns></returns>
        public int GetMaxId()
        {
            int MaxDeptID = 1;
            var List = _repository.Find(s=>s.IsDeleted==0).OrderByDescending(s => s.CodeID).FirstOrDefault();
            
            return (List != null) ? List.CodeID + 1 : MaxDeptID;
        }
    }
}