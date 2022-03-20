using Microsoft.Extensions.Configuration;
using Minio;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.Enum;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NetCoreFrame.Service
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class Frame_FileUploadService : BaseService<Frame_FileUpload>
    {
        private NetCoreFrameDBContext _dbContext;
        private readonly MinioClient _minioClient;
        private string bucketName = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="dBContext"></param>
        public Frame_FileUploadService(IRepository<Frame_FileUpload> repository
            , NetCoreFrameDBContext dBContext
            , MinioClient minioClient
            , IConfiguration config) : base(repository)
        {

            _dbContext = dBContext;
            _minioClient = minioClient;
            bucketName = config.GetValue<string>("MinIoSettings:bucketName");
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(Frame_FileUpload model)
        {
            _repository.Add(model);
        }

        public TableData Load(PageRequest request, Frame_FileUpload model)
        {
            var infolist = from a in _dbContext.Frame_FileUpload
                            
                           select new Frame_FileUpload
                           {
                               ID = a.ID,
                               FileName=a.FileName,
                               CreateBy=a.CreateBy,
                               CreateTime=a.CreateTime,
                               FileImgUrl= a.FileImgUrl,
                               IsDeleted=a.IsDeleted
                           };
            //var endpoint = "127.0.0.1:9000";
            //var accessKey = "GD4O6PLLR22K8DK99P2O";
            //var secretKey = "+rrx00RyANjyoV7XAwt8oEvi3KSgVq30wnpYqjCS";
            //var minioclient = new MinioClient(endpoint, accessKey, secretKey);
            //var bucketName = "esop";
           
            foreach (var datamodel in infolist)
            {
                datamodel.FileImgUrl =(string) (_minioClient.PresignedGetObjectAsync(bucketName, datamodel.ID, 60 * 60 * 24).Result);
            } 
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.FileName))
                {
                    infolist = infolist.Where(s => s.FileName == model.FileName);
                }
            }
            return new TableData
            {
                count = infolist.Count(),
                data = infolist.OrderByDescending(s => s.CreateTime).Skip((request.page - 1) * request.limit).Take(request.limit)
            };
        }


        public void ChangeStatus(string[] FileID,string status)
        {
            foreach (string FileIDStr in FileID)
            {
                var File = _repository.FindSingle(s => s.ID == FileIDStr);
                if (File != null)
                {
                    File.IsDeleted = (status.Equals("InUse"))? TrueOrFlase.Flase: TrueOrFlase.Null;
                    _repository.SaveChanges();
                }
            }
           
        }
    }
}
