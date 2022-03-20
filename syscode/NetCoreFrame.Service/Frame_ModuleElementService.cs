using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System.Linq;

namespace NetCoreFrame.Service
{
   public class Frame_ModuleElementService : BaseService<Frame_ModuleElement>
    {
        
        public Frame_ModuleElementService(IRepository<Frame_ModuleElement> repository ) : base(repository)
        {
           
           
        }

        public void Add(Frame_ModuleElement user)
        {
            _repository.Add(user);
        }

        /// <summary>
        /// 加载菜单 
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <returns></returns>
        public TableData LoadMenus(int ModuleId)
        { 
            return new TableData
            { 
                count = _repository.Find(s=>s.ModuleId== ModuleId).Count(),
                data = _repository.Find(s => s.ModuleId == ModuleId)
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request)
        {

          
            return new TableData
            {
                
                count = _repository.GetCount(null),
                data = _repository.Find(request.page, request.limit, "ID desc")
            };
        }
    }
}
 