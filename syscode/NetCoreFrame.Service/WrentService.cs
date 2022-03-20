using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.Enum;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Entity.Wedrent;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Service
{
    public class WrentService : BaseService<W_Goods>
    {
       
        private NetCoreFrameDBContext _dbContext;
    
        public WrentService(
            IRepository<W_Goods> repository
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
        public TableData Load(PageRequest request, string OrderNumber, string GoodsNumber)
        {
            var datalist = (from a in _dbContext.W_Order_Goods
                            join b in _dbContext.W_Order on a.OrderId equals b.ID into lefttemp
                            // join c in _dbContext.W_Goods on a.GoodsId equals c.ID into lefttemp
                            from temp in lefttemp.DefaultIfEmpty()
                            select new
                            { 
                                ID=a.ID,
                                GoodsId = a.GoodsId,
                                OrderId=a.OrderId,
                                GoodsName ="",
                                GoodsNumber = "",
                                OrderNumber = temp.OrderNumber,
                                RentTime = temp.RentTime,
                                Customer = temp.Customer,
                                LinkTel = temp.LinkTel,
                                IsReturnBack = a.IsReturnBack == true ? "已归还" : "待归还"
                            }).OrderBy(s=>s.RentTime).ThenBy(s=>s.OrderNumber).ToList();

            var datalist2 = (from a in datalist
                             join b in _dbContext.W_Goods on a.GoodsId equals b.ID into lefttemp
                             from temp in lefttemp.DefaultIfEmpty()
                             select new
                             {
                                 ID = a.ID,
                                 GoodsId = a.GoodsId,
                                 OrderId=a.OrderId,
                                 GoodsName = temp?.GoodsName,
                                 GoodsNumber = temp?.GoodsNumber,
                                 OrderNumber = a.OrderNumber,
                                 RentTime = a.RentTime,
                                 Customer = a.Customer,
                                 LinkTel = a.LinkTel,
                                 IsReturnBack = a.IsReturnBack
                             }).ToList();
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                datalist2 = datalist2.Where(s => s.OrderNumber.Contains(OrderNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(GoodsNumber))
            {
                datalist2 = datalist2.Where(s => s.GoodsNumber.Contains(GoodsNumber)).ToList();
            }
            return new TableData
            {
                count = datalist2.Count(),
                data = datalist2.Skip((request.page - 1) * request.limit).Take(request.limit)
            };
        }

        public TableData DetailLoad(PageRequest request,string ID)
        {
            var datalist = from a in _dbContext.W_GoodsDetail.Where(s => s.GoodsId == ID) select a;
            var datallist2 = (from a in datalist
                              join b in _dbContext.W_Goods on a.GoodsId equals b.ID
                              join c in _dbContext.W_Order on a.OrderId equals c.ID into lefttemp
                              from temp in lefttemp.DefaultIfEmpty()
                              select new
                              {
                                  GoodsName = b.GoodsName,
                                  GoodsNumber = b.GoodsNumber,
                                  OrderNumber = temp.OrderNumber,
                                  Customer = temp.Customer,
                                  IsReturnBack = a.IsReturnBack == true ? "归还" : "出租",
                                  OperateTime = a.OperateTime,
                                  CreateTime = a.CreateTime
                              }).OrderByDescending(s=>s.CreateTime).ToList();
            return new TableData
            {
                count = datallist2.Count(),
                data = datallist2.Skip((request.page - 1) * request.limit).Take(request.limit)
            };
        }

        /// <summary>
        /// 归还/取消归还 操作更新
        /// </summary>
        /// <param name="id">关联表的Id</param>
        /// <param name="IsReturnBack"></param>
        public void UpdateReturnStatus(string id, bool IsReturnBack)
        {
            RentStatus rentStatus = IsReturnBack ? RentStatus.CanRent : RentStatus.InRent;
            var model = _dbContext.W_Order_Goods.FirstOrDefault(s => s.ID == id);
            if (model != null)
            {
                model.IsReturnBack = IsReturnBack;
                _dbContext.W_Order_Goods.Update(model);

                //更新商品操作明细  归还添加操作记录 取消归还 删除操作记录
                if (IsReturnBack)
                {
                    _dbContext.W_GoodsDetail.Add(new W_GoodsDetail()
                    {
                        GoodsId = model.GoodsId,
                        OrderId = model.OrderId,
                        IsReturnBack = true,
                        OperateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                    });
                }
                else
                {
                    var detailMol = _dbContext.W_GoodsDetail.FirstOrDefault(s => s.OrderId == model.OrderId && s.GoodsId == model.GoodsId && s.IsReturnBack == true);
                    _dbContext.W_GoodsDetail.Remove(detailMol);
                }
                
            }

            _dbContext.SaveChanges();
        }
    }
}

