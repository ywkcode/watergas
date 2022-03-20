using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NetCoreFrame.Core.CommonHelper;
using System.Collections.Generic;
using Minio;
using System.Net;

namespace NetCoreFrame.Service
{
    public class Frame_TableInfoService : BaseService<Frame_TableInfo>
    {
        private readonly IHostingEnvironment _host;
        private readonly MinioClient _minioClient;
        private NetCoreFrameDBContext _dbContext;
        public Frame_TableInfoService(
            IRepository<Frame_TableInfo> repository ,
            IHostingEnvironment host,
            NetCoreFrameDBContext dbContext,
            MinioClient minioClient
            ) : base(repository)
        {
            _host = host;
            _dbContext = dbContext;
            _minioClient = minioClient;
        }

        public async void AddMinio()
        {
            var url = (_minioClient.PresignedGetObjectAsync("esop", "27b2e60a-9258-4ef9-9cef-b6d1ebbada9c", 60 * 60 * 24)).Result; 
            Console.WriteLine(url);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public void Add(Frame_TableInfo model)
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

        /// <summary>
        /// 获取Table最大Id
        /// </summary>
        /// <returns></returns>
        public int GetMaxTabId()
        {
            int MaxDeptID = 0;
            var count = _dbContext.Frame_TableInfo.Count();
            MaxDeptID = (count != 0) ? _dbContext.Frame_TableInfo.Max(s => s.TableId) + 1 : (MaxDeptID + 1); 
            return MaxDeptID;
        }

        public void GeneratePage(int TableID)
        {
            #region 临时文件存储
            string TempModulePath = _host.ContentRootPath + "\\Templates\\" + DateTime.Now.ToString("yyyyMMdd");
            FileHelper.CreatePath(TempModulePath);
            #endregion

            var filedInfo = _dbContext.Frame_TableInfo.Where(s => s.TableId == TableID).FirstOrDefault();
            var fieldlist = _dbContext.Frame_TableField.Where(s => s.TableId == TableID);
             
            #region 生成Model

            StreamReader sr = new StreamReader(_host.ContentRootPath + "\\Templates\\Model.cs_", System.Text.Encoding.Default);
            string strContent = sr.ReadToEnd();
            sr.Close();

       
            StringBuilder fieldSb = new StringBuilder();
            var FieldTypeStr = "";
            foreach (var filed in fieldlist)
            {
                fieldSb.Append("/// <summary>\r\n"); 
                fieldSb.Append("/// "+ filed.FieldDisplayName+ "\r\n"); 
                fieldSb.Append("/// <summary>\r\n");
               
                fieldSb.Append("[Display(Name=\""+filed.FieldDisplayName+"\")]");
                fieldSb.Append("\r\n");
                fieldSb.Append("[Description(\"" + filed.FieldDisplayName + "\")]");
                fieldSb.Append("\r\n");
                if (filed.FieldDisplayType.ToLower() == "textbox")
                {
                    fieldSb.Append("[StringLength(" + filed.FieldLength + ",ErrorMessage=\"{0}最多输入{1}个字符\")]");
                    fieldSb.Append("\r\n");
                }
                
                fieldSb.Append(" public " + filed.FieldType + " " + filed.FieldName + " { get; set; }");
                fieldSb.Append("\r\n\r\n");
            }
            strContent = strContent.Replace("{TableField}", fieldSb.ToString()).Replace("{TableName}", filedInfo?.PhysicalName??"");

           
            string ModelPath = TempModulePath + "\\"+ filedInfo?.PhysicalName + ".cs_";
            FileHelper.CreateFile(ModelPath);
            StreamWriter write = new StreamWriter(ModelPath, false, System.Text.Encoding.Default);
            write.Write(strContent);
            write.Close();

            #endregion

            #region 生成Service
            sr = new StreamReader(_host.ContentRootPath + "\\Templates\\Service.cs_", System.Text.Encoding.Default);
            strContent = sr.ReadToEnd();
            sr.Close();
            strContent = strContent.Replace("{TableName}", filedInfo?.PhysicalName ?? "");
            fieldSb = new StringBuilder(); ;
           
            foreach (var filed in fieldlist)
            {
                if (filed.IsSearchCondition == 1)
                {
                    fieldSb.Append("if (!string.IsNullOrEmpty(model."+filed.FieldName+"))");
                    fieldSb.Append("\r\n");
                    fieldSb.Append("{\r\n");
                    fieldSb.Append(" expression = expression.And(t => t." + filed.FieldName + ".Contains(model."+ filed.FieldName + "));");
                    fieldSb.Append("}\r\n");
                } 
            }
            if (fieldSb!=null)
            {
                strContent = strContent.Replace("{SearchField}", fieldSb.ToString());
            }
           
            ModelPath = TempModulePath + "\\" + filedInfo?.PhysicalName + "Service.cs_";
            FileHelper.CreateFile(ModelPath);
            write = new StreamWriter(ModelPath, false, System.Text.Encoding.Default);
            write.Write(strContent);
            write.Close();
            #endregion

            #region 生成Controller
            sr = new StreamReader(_host.ContentRootPath + "\\Templates\\Controller.cs_", System.Text.Encoding.Default);
            strContent = sr.ReadToEnd();
            sr.Close();
            ModelPath = TempModulePath + "\\" + filedInfo?.PhysicalName + "Controller.cs_";
            FileHelper.CreateFile(ModelPath);
            strContent = strContent.Replace("{TableName}", filedInfo?.PhysicalName ?? "");
            write = new StreamWriter(ModelPath, false, System.Text.Encoding.Default);
            write.Write(strContent);
            write.Close();
            #endregion

            #region 生成Create
            sr = new StreamReader(_host.ContentRootPath + "\\Templates\\Create.cshtml_", System.Text.Encoding.Default);
            strContent = sr.ReadToEnd();
            sr.Close();
            strContent = strContent.Replace("{TableName}", filedInfo?.PhysicalName ?? "").Replace("{TableField}", GenerateTableHtml(fieldlist.ToList()));
          
            ModelPath = TempModulePath + "\\" + filedInfo?.PhysicalName + "_Create.cshtml_";
            FileHelper.CreateFile(ModelPath);
            write = new StreamWriter(ModelPath, false, System.Text.Encoding.Default);
            write.Write(strContent);
            write.Close();
            #endregion


            #region 生成Index
            sr = new StreamReader(_host.ContentRootPath + "\\Templates\\Index.cshtml_", System.Text.Encoding.Default);
            strContent = sr.ReadToEnd();
            sr.Close();
            strContent = strContent.Replace("{TableName}", filedInfo?.PhysicalName ?? "").Replace("{SearchColumns}", GenerateIndexHtml(fieldlist.ToList()));

            ModelPath = TempModulePath + "\\" + filedInfo?.PhysicalName + "Index.cshtml_";
            FileHelper.CreateFile(ModelPath);
            write = new StreamWriter(ModelPath, false, System.Text.Encoding.Default);
            write.Write(strContent);
            write.Close();
            #endregion
        }


        public string GenerateTableHtml(List<Frame_TableField> TFs)
        {
            StringBuilder sb = new StringBuilder(); 
         
            int fieldCount = 0;
            foreach (var fieldMol in TFs)
            {
                if (fieldCount % 2 == 0)
                {
                    sb.Append(" <div class=\"layui-form-item\"> \r\n");
                }
                switch (fieldMol.FieldDisplayType.ToLower())
                {
                    case "textbox":
                        sb.Append("<div class=\"layui-inline\">\r\n");
                        sb.Append("<label asp-for=\""+ fieldMol.FieldName+ "\" class=\"layui-form-label\"></label>\r\n");
                        sb.Append("<div class=\"layui-input-block\">\r\n");
                        sb.Append("<input asp-for=\"" + fieldMol.FieldName + "\" type=\"text\" name=\"" + fieldMol.FieldName + "\"   placeholder= \"请输入"+fieldMol.FieldDisplayName+ "\"    class=\"layui-input\">\r\n");
                        sb.Append("<span asp-validation-for=\"" + fieldMol.FieldName + "\" class=\"text-danger\"></span>\r\n");
                        sb.Append("</div>\r\n");
                        sb.Append("</div>\r\n");
                        break;
                }
                if (fieldCount % 2 == 0&& fieldCount!=0)
                {
                    sb.Append("</div>\r\n");
                }
                fieldCount++;
            }
            //for (int i = 0; i < TFs.Count; i++)
            //{
              

            //    #region 根据字段类型写入控件
            //    switch (TFs[i].FieldDisplayType.ToLower())
            //    {
            //        case "textbox":
            //            string strDataType1 = TFs[i].FieldType;
            //            if (strDataType1 == "datetime") //日期类型
            //            {
            //                sb.Append("<input type=\"text\" id=\"" + strFieldName + "\" name=\"" + strFieldName + "\" class=\"laydate-icon\" onclick=\"laydate({format: 'YYYY-MM-DD'})\" />\r\n");
            //            }
            //            else
            //            {
            //                sb.Append(" @Html.EditorFor(model => model." + strFieldName + ",new { htmlAttributes = new { @class = \"ExpTextBox\" }})\r\n");
            //            }
            //            break;

            //        case "datepick":
            //            sb.Append("@Html.DatePickFor(model => model." + strFieldName + ")");
            //            // sb.Append("<input type=\"text\" id=\""+strFieldName+"\" name=\""+strFieldName+"\" class=\"laydate-icon\" onclick=\"laydate({format: 'YYYY-MM-DD'})\" />\r\n");
            //            break;
            //        case "html":
            //            sb.Append("  @Html.KindEditorFor(model => model." + strFieldName + ") ");
            //            break;
            //        case "dropdownlist":
            //            //判断代码的类别，如果是一维代码，还是加载DropDownlist，否则加载代码树
            //            string DataSource_CodeName = TFs[i].DataSource_CodeName;
                       
            //            break;
            //        case "textarea":
                      
            //            break;
            //        case "radio":
                       
            //            break;
            //        case "checkbox":
            //            break;
                  
                  


            //    }
            //    #endregion
             
            //} 

         

            return sb.ToString();

        }

        public string GenerateIndexHtml(List<Frame_TableField> TFs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" var columns = [\r\n");
            sb.Append("{ checkbox: true, fixed: true },\r\n");
            sb.Append("{ field: 'Index', title: '序号', width: 60, templet: '#Index' },\r\n");
            foreach (var fieldMol in TFs)
            {
                sb.Append("{ field: '"+ fieldMol.FieldName+ "', title: '"+fieldMol.FieldDisplayName+"' },\r\n"); 
            }
            sb.Append(" ];\r\n");
            return sb.ToString();
        }
    }
}
