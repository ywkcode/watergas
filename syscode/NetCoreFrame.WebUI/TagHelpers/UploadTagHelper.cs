using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreFrame.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NetCoreFrame.WebUI.TagHelpers
{
    /// <summary>
    /// 上传控件
    /// </summary>
    [HtmlTargetElement("form:Upload")]
    public class UploadTagHelper : TagHelper
    {
        /// <summary>
        /// 控件Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否上传图片
        /// </summary>
        public bool IsUploadImg { get; set; }

        /// <summary>
        /// 上传路径
        /// </summary>
        //public string UploadUrl { get; set; } = "/FileUpload/FileMinIoSave";
        public string UploadUrl { get; set; } = "/FileUpload/FileSave";

        protected string ShowList { get; set; }

        /// <summary>
        /// 选中值
        /// </summary>
        public string SelectdValue { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string btnuploadId = Name + "_btnupload";
            string aId = Name + "_a";
            output.TagName = "div";
            output.Attributes.Add("class", "layui-upload");
            output.TagMode = TagMode.StartTagAndEndTag;
            ShowList = "none";
            if (IsUploadImg)
            {
                var container = new TagBuilder("button");
                container.InnerHtml.Append("图片");
                container.Attributes.Add("type", "button");
                container.Attributes.Add("class", "layui-btn");
                container.Attributes.Add("id", btnuploadId);
                if (!string.IsNullOrEmpty(SelectdValue))
                {
                    ShowList = "block";
                }

                output.Content.AppendHtml(container);

                output.Content.AppendHtml($@"
             <div style='display: none'>
                <input type='text' name='{Name}' id='{Name}' class='layui-input'>
            </div>
            <div class='layui-upload-list' style='display:{ShowList};text-align:center'>
                <img class='layui-upload-img' id='{Name}_img' style='width:200px;height:200px'>
                <p id='demoText'></p>
            </div>
              ");
               
                output.PostElement.SetHtmlContent($@"
 
                 <script> 
            layui.use('upload', function() {{
                //var $ = layui.jquery
                  var $ = layui.$
                   , upload = layui.upload;
                //普通图片上传
                var uploadInst = upload.render({{
                elem: '#{btnuploadId}'
                , url: '{UploadUrl}' //改成您自己的上传接口
                //,multiple:true
                , before: function(obj) {{

                        obj.preview(function(index, file, result) {{
                          $('#{Name}_img').attr('src', result); //图片链接（base64）
                        }});
                        $('#{ Name}_img').attr('src','{SelectdValue}');
                    }}
            , done: function(res) {{
                        //如果上传失败

                        if (res.code > 0)
                        {{
                            return layer.msg('上传失败');
                        }}
                        else
                        {{
                    $('.layui-upload-list').show();
                    $('#{Name}').val(res.savepath);
              }} }}
                        //上传成功
                
            , error: function() {{
                        //演示失败状态，并实现重传
                        //var demoText = $('#demoText');
                        //demoText.html('<span style=\'color:#FF5722;\'>上传失败</span> <a class=\'layui-btn layui-btn-xs demo-reload\'>重试</a>\');
                        //demoText.find('.demo-reload').on('click', function() {{
                        //    uploadInst.upload();
                        //}});
                    }}
                }});
           }});
             //$('#{ Name}_img').attr('src','{SelectdValue}');
           </script> ");
            }
            else {
                var container = new TagBuilder("button"); 
                container.Attributes.Add("type", "button");
                container.InnerHtml.Append("文件");
                container.Attributes.Add("class", "layui-btn layui-bg-orange");
                container.Attributes.Add("id", btnuploadId);
                output.Content.AppendHtml(container);
             

                output.Content.AppendHtml($@"
             <a href='' id='{aId}'></a>
             <div style='display:none'>
                <input type='text' name='{Name}' id='{Name}' class='layui-input'/>
            </div>
              ");
                output.PostElement.SetHtmlContent($@"<script>
               layui.use('upload', function(){{
                        var $ = layui.jquery
                        ,upload = layui.upload;
                          upload.render({{
                            elem: '#{btnuploadId}'
                            , url: '{UploadUrl}'  
                            ,accept: 'file' //普通文件
                            ,multiple:true
                            ,done: function(res){{ 
                               $('#{Name}').val(res.savepath);
                                $('#{aId}').text(res.filename).attr('href',res.savepath);
                               ShowNotice();
                               layer.msg('上传成功');
                            }}
                          }});
                  }});  </script>");
            }

         

        
        }
    }
}
