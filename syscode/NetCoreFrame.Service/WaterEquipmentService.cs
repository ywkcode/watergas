using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.Water;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Service
{
     
    public class WaterEquipmentService : BaseService<Water_Equipment>
    {

        private NetCoreFrameDBContext _dbContext;

        public WaterEquipmentService(
            IRepository<Water_Equipment> repository
            , NetCoreFrameDBContext dBContext
        ) : base(repository)
        {

            _dbContext = dBContext;

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
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(Water_Equipment model)
        {
            _repository.Add(model); 
        }


    }
}
