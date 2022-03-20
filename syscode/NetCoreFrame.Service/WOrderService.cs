using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Entity.Wedrent;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Service
{
    public class WOrderService : BaseService<W_Order>
    {

        private NetCoreFrameDBContext _dbContext;
   
        public WOrderService(
            IRepository<W_Order> repository
            , NetCoreFrameDBContext dBContext
            
             ) : base(repository)
        {

            _dbContext = dBContext;
         
        }
        /// <summary>
        /// 新增（下单）
        /// </summary>
        /// <param name="role"></param>
        public void Add(W_Order order)
        {
            order.RentTime = order.Weddingdate;
            _repository.Add(order);
            //关联表
            var goodsids = order.GoodsList.TrimStart(',').TrimEnd(',');
            List<W_Order_Goods> addlist = new List<W_Order_Goods>();
            List<W_GoodsDetail> detaillist = new List<W_GoodsDetail>();
            foreach (string goodid in goodsids.Split(','))
            {
                addlist.Add(new W_Order_Goods() {
                    OrderId = order.ID,
                    GoodsId = goodid,
                    IsReturnBack = false
                });
                ////明细表
                detaillist.Add(new W_GoodsDetail()
                {
                    GoodsId=goodid,
                    OrderId=order.ID,
                    OperateTime=Convert.ToDateTime(order.Weddingdate.ToString("yyyy-MM-dd")),
                    IsReturnBack=false
                });
            }
            _dbContext.W_Order_Goods.AddRange(addlist);
            _dbContext.W_GoodsDetail.AddRange(detaillist);
            _dbContext.SaveChanges();
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string[] ids)
        {
            var detaillistall = (from a in _dbContext.W_GoodsDetail select a).ToList();
            var linklistall = (from a in _dbContext.W_Order_Goods select a).ToList();
            // _repository.BatchDelete(u => ids.Contains(u.ID));
            foreach (string id in ids)
            {
                var order = _dbContext.W_Order.FirstOrDefault(s => s.ID == id);
                _dbContext.W_Order.Remove(order);
               //关联表删除
               var linklist = linklistall.Where(s => s.OrderId == id);
                foreach (var model in linklist)
                {
                    //明细表删除
                    var detaillist = detaillistall.Where(s => s.GoodsId == model.GoodsId && s.OrderId == model.OrderId);
                    foreach (var modelA in detaillist.ToList())
                    {
                        _dbContext.W_GoodsDetail.Remove(modelA);
                    }
                    _dbContext.W_Order_Goods.Remove(model);
                }
                
            }
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request, string OrderNumber, string Customer,string GoodsNames)
        {
            List<WOrderResponse> wOrderResponses = new List<WOrderResponse>();
            var datalist = from a in _dbContext.W_Order select a;
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                datalist = datalist.Where(s => s.OrderNumber.Contains(OrderNumber));
            }
            if (!string.IsNullOrEmpty(Customer))
            {
                datalist = datalist.Where(s => s.Customer.Contains(Customer));
            }
            var resdatalist = datalist.OrderByDescending(s => s.CreateTime).Skip((request.page - 1) * request.limit).Take(request.limit);
            var goodslist =( from a in _dbContext.W_Order_Goods
                            join b in _dbContext.W_Goods on a.GoodsId equals b.ID
                            select new {
                                GoodsInfo = b.GoodsName + "(" + b.GoodsNumber + ")",
                                GoodsId = b.ID,
                                OrderId = a.OrderId
                            }).ToList();
           
            foreach (var dataMol in resdatalist)
            {
              
                wOrderResponses.Add(new WOrderResponse()
                {
                    ID = dataMol.ID,
                    OrderNumber = dataMol.OrderNumber,
                    LinkTel = dataMol.LinkTel,
                    Address = dataMol.Address,
                    Remarks = dataMol.Remarks,
                    Weddingdate=dataMol.Weddingdate,
                    GoodsInfo = goodslist.Where(s => s.OrderId.Equals(dataMol.ID)).Select(s => new W_GoodsInfo()
                    {
                        GoodsId=s.GoodsId,
                        GoodsName=s.GoodsInfo
                    }).ToList()
                });
            }
            if (!string.IsNullOrEmpty(GoodsNames))
            {
                wOrderResponses = wOrderResponses.Where(s=>s.GoodsInfo.Any(s=>s.GoodsName.Contains(GoodsNames))).ToList();
            }


            return new TableData
            {
                count = datalist.Count(),
                data = wOrderResponses
            };
        }

       
    }
}

