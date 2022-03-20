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
    public class WGoodsService : BaseService<W_Goods>
    {

        private NetCoreFrameDBContext _dbContext;
        private readonly MinioClient _minioClient;
        private string bucketName = "";
        public WGoodsService(
            IRepository<W_Goods> repository
            , NetCoreFrameDBContext dBContext
            , IConfiguration config
            , MinioClient minioClient) : base(repository)
        {

            _dbContext = dBContext;
            _minioClient = minioClient;
            bucketName = config.GetValue<string>("MinIoSettings:bucketName");
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(W_Goods role)
        {
            _repository.Add(role);
         
        }

        public void UpdateStatus(string id, RentStatus rentStatus)
        {
            var model = _repository.FindSingle(s => s.ID == id);
            if (model != null)
            {
                
                _repository.Update(model);
            }
        }

        public void DeleteGoodsLink(List<string> orderids)
        {
            var data = (from a in _dbContext.W_Order_Goods.Where(s => orderids.Contains(s.OrderId))
                        join b in _dbContext.W_Goods on a.GoodsId equals b.ID
                        select a).Select(s => s.GoodsId).ToList();
            _repository.BatchDelete(s => data.Contains(s.ID)); 
                      
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request,string GoodsName,string GoodsNumber)
        {
            var detaillist = from a in _dbContext.W_GoodsDetail select a;
            var datalist = (from a in _dbContext.W_Goods.OrderBy(s => s.GoodsNumber)
                           select new W_Goods
                           {
                               ID = a.ID,
                               GoodsName = a.GoodsName,
                               GoodsNumber = a.GoodsNumber,
                               GoodsSize = a.GoodsSize,
                               GoodsType = a.GoodsType,
                               GoodsPrice = a.GoodsPrice,
                               Supplier = a.Supplier,
                               Remarks = a.Remarks,
                               RentStatus="",
                               OrderTime=a.OrderTime
                           }).ToList();
            if (!string.IsNullOrEmpty(GoodsName))
            {
                datalist = datalist.Where(s => s.GoodsName.Contains(GoodsName)).ToList();
            }
            if (!string.IsNullOrEmpty(GoodsNumber))
            {
                datalist = datalist.Where(s => s.GoodsNumber.Contains(GoodsNumber)).ToList();
            }
            DateTime Nowdata = DateTime.Now;
            foreach (var dataMol in datalist)
            {
                var temp = detaillist.OrderByDescending(s => s.CreateTime).FirstOrDefault(s => s.CreateTime <= Nowdata && s.GoodsId == dataMol.ID);
                dataMol.RentStatus = "可租";
                if (temp != null)
                {
                    if (temp.IsReturnBack == false)
                    {
                        if (Nowdata.ToString("yyyy-MM-dd").Equals(temp.OperateTime.ToString("yyyy-MM-dd")))
                        {
                            dataMol.RentStatus = "出租中";
                        }
                        else
                        {
                            dataMol.RentStatus = "待归还";
                        }
                    }
                    
                }
            }
            return new TableData
            {
                count = datalist.Count(),
                data = datalist.Skip((request.page-1)*request.limit).Take(request.limit)
            };
        }

        public List<string> GetProductionImgList(string IpNum)
        {


            var Guid = _repository.FindSingle(s => s.ID == IpNum)?.ID ?? "127.0.0.1";
            List<string> imgList = new List<string>();
            var datalist = from a in _dbContext.Frame_Relations
                                     .Where(s => s.FirstId == Guid && s.RelationType == Entity.Enum.RelationType.Station)
                           join b in _dbContext.Frame_FileUpload
                           on a.SecondId equals b.ID
                           select new
                           {
                               FileID = a.SecondId
                           };
            foreach (var dataMol in datalist)
            {
                imgList.Add((_minioClient.PresignedGetObjectAsync(bucketName, dataMol.FileID, 60 * 60 * 24)).Result);
            }
            return imgList;
        }

        /// <summary>
        /// 婚期当天可供选择的商品 不在租赁期间的婚纱 暂时不考虑归还的情况
        /// </summary>
        /// <param name="weddingDate"></param>
        /// <returns></returns>
        public List<MultiSelectlistViewModel> GetNotRentGoods(DateTime weddingDate, string OrderId,string GoodsList)
        {
            GoodsList = (!string.IsNullOrEmpty(GoodsList)) ? GoodsList.TrimStart(',').TrimEnd(','):"";
            //已出租的商品
            var selectlist = _dbContext.W_GoodsDetail.Where(s => s.OperateTime == weddingDate&&s.IsReturnBack==false&&s.OrderId!=OrderId).Select(s=>s.GoodsId);
           
            var goodslist =( from a in _dbContext.W_Goods where !(selectlist).Contains(a.ID) 
                                                        select new MultiSelectlistViewModel { 
                                                        name=a.GoodsName+"("+a.GoodsNumber+")",
                                                        value=a.ID,
                                                        selected="" 
                                                        }).OrderBy(s=>s.name).ToList();
            foreach (var goodMol in goodslist)
            {
                foreach (string goodid in GoodsList.Split(','))
                {
                    if (goodMol.value == goodid) goodMol.selected = "selected";
                }
            }
            return goodslist ;
        }
    }
}

