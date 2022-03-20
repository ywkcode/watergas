using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Minio;
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
    public class productionstationService : BaseService<ProductionStation>
    {
       
        private NetCoreFrameDBContext _dbContext;
        private readonly MinioClient _minioClient;
        private string bucketName = "";
        public productionstationService(
            IRepository<ProductionStation> repository
            , NetCoreFrameDBContext dBContext
            , IConfiguration config
            , MinioClient minioClient) : base(repository)
        {

            _dbContext = dBContext; 
            _minioClient = minioClient;
             bucketName= config.GetValue<string>("MinIoSettings:bucketName");
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(ProductionStation role)
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

        public List<string> GetProductionImgList(string IpNum)
        {
            

            var Guid = _repository.FindSingle(s => s.IPNum == IpNum)?.ID ?? "127.0.0.1";
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
    }
}

