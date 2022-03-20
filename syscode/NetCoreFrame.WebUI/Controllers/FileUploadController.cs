using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc; 
using Newtonsoft.Json.Linq;
using NetCoreFrame.Service;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Entity.Enum;
using Microsoft.Extensions.Configuration;
using Minio;
using NetCoreFrame.Core.Request;

namespace NetCoreFrame.WebUI.Controllers
{
   
 
    public class FileUploadController : Controller
    {
        protected readonly Frame_FileUploadService _service;
        protected readonly Frame_RelationsService _relationservice;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string httphost;
        private readonly MinioClient _minioClient;
        private string bucketName = "";
        public FileUploadController(
            IHostingEnvironment hostingEnvironment,
            Frame_FileUploadService frame_FileUploadService,
            Frame_RelationsService relationservice,
             IConfiguration _configuration,
             MinioClient minioClient)
        {
            _hostingEnvironment = hostingEnvironment;
            _service = frame_FileUploadService;
            _relationservice = relationservice;
            httphost = _configuration.GetValue<string>("CommonKeys:RequestHost");
            _minioClient = minioClient;
            bucketName = _configuration.GetValue<string>("MinIoSettings:bucketName");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> FileMinIoSave()
        {
            //获取Form提交的文件
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath; //物理路径
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string showfilePath = "";
            string filename = "";
            string filetype = "";

            string FileId = Guid.NewGuid().ToString();
           
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //var endpoint = "127.0.0.1:9000";
                    //var accessKey = "GD4O6PLLR22K8DK99P2O";
                    //var secretKey = "+rrx00RyANjyoV7XAwt8oEvi3KSgVq30wnpYqjCS";
                    //var minioclient = new MinioClient(endpoint, accessKey, secretKey);
                    //var bucketName = "esop";
                    var location = "us-east-1";
                    int count = formFile.FileName.Split('.').Length;
                    filename = formFile.FileName.Split('.')[0];
                    string fileExt = formFile.FileName.Split('.')[count - 1]; //文件扩展名，不含“.”
                    filetype = fileExt;
                    try
                    {
                        bool found = await _minioClient.BucketExistsAsync(bucketName);
                        if (!found)
                        {
                            await _minioClient.MakeBucketAsync(bucketName, location);
                        }
                        await _minioClient.PutObjectAsync(bucketName, FileId, formFile.OpenReadStream(), formFile.Length, formFile.ContentType);
                    
                        filename = formFile.FileName;
                        //Minio路径
                        showfilePath = await _minioClient.PresignedGetObjectAsync(bucketName, FileId, 60 * 60 * 24);
                        _service.Add(new Frame_FileUpload()
                        {
                            ID = FileId,
                            FileName = filename,
                            FilePath = "minio",
                            FileType = filetype,
                            FileImgUrl = showfilePath
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            return Ok(new { count = files.Count, attachid = FileId, savepath = showfilePath, filename = filename + '.' + filetype, filetype = filetype, FileId = FileId });
        }

        [HttpPost]
       
        public async Task<IActionResult> FileSave()
        {
            //获取Form提交的文件
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath; //物理路径
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string showfilePath = "";
            string filename = "";
            string filetype = "";
         
            string FileId = Guid.NewGuid().ToString();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    int count = formFile.FileName.Split('.').Length;
                    filename = formFile.FileName.Split('.')[0];
                    string fileExt = formFile.FileName.Split('.')[count - 1]; //文件扩展名，不含“.”
                    filetype = fileExt;
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    #region 文件夹不存在则创建
                    var filePath = webRootPath + "/upload";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    #endregion
                    #region 文件不存在则新建
                    filePath = webRootPath + "/upload/" + newFileName;
                    showfilePath = "/upload/" + newFileName;
                    FileHelper.CreateFile(filePath);
                    #endregion 
                    //把上传的图片复制到指定的文件中
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    { 
                        await formFile.CopyToAsync(stream);
                    }

                    #region 建立图
                    _service.Add(new Frame_FileUpload()
                    {
                        ID= FileId,
                        FileName = filename,
                        FilePath= filePath,
                        FileType= filetype,
                        FileImgUrl= httphost+showfilePath
                    });
                    //附件关联表去掉 增加了额外的存储
                    //_relationservice.Add("", FileId, "Attach");
                    #endregion
                }
            }

            return Ok(new { count = files.Count,attachid= FileId, savepath = showfilePath, filename= filename + '.' + filetype, filetype = filetype, FileId= FileId });
        }

        [AllowAnonymous]
        [HttpPost] 
        public JsonResult FileSave2()
        {
            //获取Form提交的文件

            var files = Request.Form.Files;
            long size = 11;
            string webRootPath = _hostingEnvironment.WebRootPath; //物理路径
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string showfilePath = "";
            string filename = "";
            string filetype = "";

            string FileId = Guid.NewGuid().ToString();
 

            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        int count = formFile.FileName.Split('.').Length;
            //        filename = formFile.FileName.Split('.')[0];
            //        string fileExt = formFile.FileName.Split('.')[count - 1]; //文件扩展名，不含“.”
            //        filetype = fileExt;
            //        long fileSize = formFile.Length; //获得文件大小，以字节为单位
            //        string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
            //        #region 文件夹不存在则创建
            //        var filePath = webRootPath + "/upload";
            //        if (!Directory.Exists(filePath))
            //        {
            //            Directory.CreateDirectory(filePath);
            //        }
            //        #endregion
            //        #region 文件不存在则新建
            //        filePath = webRootPath + "/upload/" + newFileName;
            //        showfilePath = "upload/" + newFileName;
            //        FileHelper.CreateFile(filePath);
            //        #endregion 
            //        //把上传的图片复制到指定的文件中
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //              formFile.CopyToAsync(stream);
            //        }

            //        #region 建立图
            //        _service.Add(new Frame_FileUpload()
            //        {
            //            ID = FileId,
            //            FileName = filename,
            //            FilePath = filePath,
            //            FileType = filetype,
            //            FileImgUrl = httphost + showfilePath
            //        });
            //        _relationservice.Add("", FileId, "Attach");
            //        #endregion
            //    }
            //}

            return Json(new { count = 1, attachid = FileId, savepath = showfilePath, filename = filename, filetype = filetype, FileId = FileId });
        }

        [AllowAnonymous]
        [HttpPost]
        
        public string  UploadBase64(string fileBase64,string fileExt)
        {
            TableData data = new TableData();
            byte[] bytes = ToBytes_FromBase64Str(fileBase64);
            //var fileExtension = Path.GetExtension(fileName);
          
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
            
            var filePath = webRootPath + "/upload";
            var RetfilePath =  "upload/" + newFileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = webRootPath + "/upload/" + newFileName; 
            try
            {
                data.code = 200;
                FileStream fs = new FileStream(filePath, FileMode.CreateNew);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                data.msg = RetfilePath;
            }
            catch (Exception ex)
            {
                data.code = 500;
                data.msg = "newFileName:"+ newFileName+"Error:"+ex.Message;
            }

            return JsonHelper.Instance.Serialize(data);
        }

        [AllowAnonymous]
        [HttpPost]

        public string UploadBase64(string fileBase64)
        {
            TableData data = new TableData();
            byte[] bytes = ToBytes_FromBase64Str(fileBase64);
            //var fileExtension = Path.GetExtension(fileName);

            string webRootPath = _hostingEnvironment.WebRootPath;
            string newFileName = System.Guid.NewGuid().ToString() + "." ; //随机生成新的文件名

            var filePath = webRootPath + "/upload";
            var RetfilePath = "upload/" + newFileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = webRootPath + "/upload/" + newFileName;
            try
            {
                data.code = 200;
                FileStream fs = new FileStream(filePath, FileMode.CreateNew);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                data.msg = RetfilePath;
            }
            catch (Exception ex)
            {
                data.code = 500;
                data.msg = "newFileName:" + newFileName + "Error:" + ex.Message;
            }

            return JsonHelper.Instance.Serialize(data);
        }
        [AllowAnonymous]
        [HttpPost]

        public string UploadBase64_2(string fileBase64)
        {
           
            //var fileName = data["file_name"].ToObject<string>();
            //if (!string.IsNullOrWhiteSpace(imageBase64))
            //{
            //    var reg = new Regex("data:image/(.*);base64,");
            //    imageBase64 = reg.Replace(imageBase64, "");
            //    byte[] imageByte = Convert.FromBase64String(imageBase64);
            //    var stream = new MemoryStream(imageByte);
            //}
            var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
            var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                                                                        //var saveName = strDateTime + strRan + fileExtension;
            var saveName = strDateTime + strRan;
            var savePath = "Upload/Img/" + DateTime.Now.ToString("yyyyMMdd") + "/" + saveName;
            string filePath = "http://192.168.3.23/" + savePath;
            return filePath;
        }
        public static byte[] ToBytes_FromBase64Str(  string base64Str)
        {
            return Convert.FromBase64String(base64Str);
        }


        public string All(PageRequest request)
        {
            TableData data = new TableData();

            data = _service.Load(request, new Frame_FileUpload() { });
            return JsonHelper.Instance.Serialize(data);
        }


        [HttpPost]
        public string Delete(string[] ids)
        {

            _service.BatchDelete(ids);
            return JsonHelper.Instance.Serialize(new PageResponse());
        }
        [HttpPost]
        public string ChangeStatus(string[] ids,string status)
        {

            _service.ChangeStatus(ids, status);
            return JsonHelper.Instance.Serialize(new PageResponse());
        }

        
    }
}