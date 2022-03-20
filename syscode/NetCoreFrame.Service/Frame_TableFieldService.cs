using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using NetCoreFrame.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NetCoreFrame.Service
{
    public class Frame_TableFieldService : BaseService<Frame_TableField>
    {
        private readonly IHostingEnvironment _host;
        public Frame_TableFieldService(IRepository<Frame_TableField> repository, IHostingEnvironment host) : base(repository)
        {
            _host = host;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public void Add(Frame_TableField model)
        {
            _repository.Add(model);
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
        public TableData LoadFieldList(int page,int limit, int TableId)
        {
         
            var fieldlist = _repository.Find(s => s.TableId == TableId);
            return new TableData
            {
                count = _repository.GetCount(null),
                data = fieldlist.OrderByDescending(s=>s.CreateTime).Skip((page-1)*limit).Take(limit)
            };
        }

     
    }
}
