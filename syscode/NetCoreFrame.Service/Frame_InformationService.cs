using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
namespace NetCoreFrame.Service
{
    /// <summary>
    /// Frame_Information
    /// </summary>
    public class Frame_InformationService : BaseService<Frame_Information>
    {

        private NetCoreFrameDBContext _dbContext;
       
        public Frame_InformationService(IRepository<Frame_Information> repository
            , NetCoreFrameDBContext dBContext) : base(repository)
        {

            _dbContext = dBContext;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(Frame_Information model)
        {
            if (!string.IsNullOrEmpty(model.CategoryName))
            {
                var codeval = _dbContext.Frame_CodesValue.FirstOrDefault(s => s.ItemValue == model.CategoryName);
                model.CategoryName = codeval.ItemName;
                model.CategoryCode = codeval.ItemValue;
            }
            _repository.Add(model);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request,Frame_Information model)
        {
            var infolist = from a in _dbContext.Frame_Information
                           join  b in _dbContext.Frame_CodesValue on a.CategoryName equals b.ItemValue
                          
                            into grouping
                           from temp in grouping.DefaultIfEmpty()
                           select new Frame_Information
                           {
                               ID=a.ID,
                               BoardCount=a.BoardCount, 
                               DownloadCount=a.DownloadCount,
                               CreateTime=a.CreateTime,
                               CreateBy=a.CreateBy,
                               FileTitle=a.FileTitle,
                               FileContent=a.FileContent,
                               VideoMinLength=a.VideoMinLength,
                               FileType=a.FileType,
                              CategoryName=a.CategoryName
                           };
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.CategoryName))
                {
                    infolist=infolist.Where(s => s.CategoryName == model.CategoryName);
                }
            }
            return new TableData
            {
                count = infolist.Count(),
                data = infolist.OrderByDescending(s=>s.CreateTime).Skip((request.page-1) * request.limit).Take(request.limit)
            };
        }



     
    }
}
