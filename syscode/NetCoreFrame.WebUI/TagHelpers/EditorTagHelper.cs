using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.TagHelpers
{
    /// <summary>
    /// 文本编辑器
    /// </summary>
    [HtmlTargetElement("form:Editor")]
    public class EditorTagHelper : TagHelper
    {
        /// <summary>
        /// 控件Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控件高度
        /// </summary>
        public string EditorHeight { get; set; }

        /// <summary>
        /// 上传服务器路径
        /// </summary>
        public string UploadImgServer { get; set; } = "/FileUpload/FileSave";

        /// <summary>
        /// 选中值
        /// </summary>
        public string SelectdValue { get; set; }

        /// <summary>
        /// 图片大小 默认3M
        /// </summary>
        public int ImgMaxSize { get; set; } = 3 * 1024 * 1024;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "layui-inline"); 
            output.TagMode = TagMode.StartTagAndEndTag;


            //编辑器ID
            string EditorId = Name + "_editor";
            var container2 = new TagBuilder("div"); 
            container2.Attributes.Add("class", "layui-inline");
            container2.Attributes.Add("id", EditorId);
            string EditorHeightStr = (!string.IsNullOrEmpty(EditorHeight)) ? EditorHeight : "300px";
            container2.Attributes.Add("style", "height:"+ EditorHeightStr);
            output.Content.AppendHtml(container2);

            //隐藏域
            var container = new TagBuilder("input");
            container.Attributes.Add("type", "text");
            container.Attributes.Add("name", Name);
            container.Attributes.Add("id", Name);
            container.Attributes.Add("style", "display:none");

            output.Content.AppendHtml(container);
          
            //js引用
            output.PostElement.SetHtmlContent($@"
             
              <script type='text/javascript'>
              var E = window.wangEditor;
               var editor = new E('#{EditorId}');
           editor.customConfig.onchange = function (html) {{
        // html 即变化之后的内容
                 $('#{Name}').val(html);
             }};
           editor.customConfig.uploadImgServer = '{UploadImgServer}';
           editor.customConfig.uploadFileName ='file';   //文件名称 也就是你在后台接受的 参数值
           editor.customConfig.uploadImgMaxSize ={ImgMaxSize}  //默认为3M
             //header头信息
            editor.customConfig.uploadImgHeaders = {{ 'Accept': 'text/x-json'  }}
            // 使用 base64 保存图片
            editor.customConfig.uploadImgShowBase64 = false;  

            editor.customConfig.uploadImgHooks = {{
           error: function (xhr, editor) {{
                  alert( xhr +'请查看你的json格式是否正确，图片并没有上传');
                // 图片上传出错时触发 如果是这块报错 就说明文件没有上传上去，直接看自己的json信息。是否正确
                // xhr 是 XMLHttpRequst 对象，editor 是编辑器对象
                }},
             fail: function(xhr, editor, result)
        {{
            // 如果在这出现的错误 就说明图片上传成功了 但是没有回显在编辑器中，我在这做的是在原有的json 中添加了
            // 一个url的key（参数）这个参数在 customInsert也用到
            //
            alert(  xhr + '请查看你的json格式是否正确，图片上传了，但是并没有回显');
        }},
             success: function(insertImg, editor, result)
        {{
           
            //成功 不需要alert 当然你可以使用console.log 查看自己的成功json情况
            //console.log(result)
            var imgAttr = '<img src=\''+ result.savepath +'\' style=\'width:100px;height:100px\'/>';
            editor.txt.append(imgAttr); 
        }},
        customInsert: function(insertImg, result, editor)
        {{
            //console.log(result);
            // 图片上传并返回结果，自定义插入图片的事件（而不是编辑器自动插入图片！！！）
            // insertImg 是插入图片的函数，editor 是编辑器对象，result 是服务器端返回的结果
            // 举例：假如上传图片成功后，服务器端返回的是 url:... 这种格式，即可这样插入图片：
            //insertImg(result.url);
        }}
    }};

             editor.create();
             debugger;
             editor.txt.html('{SelectdValue}');
              </script>
           ");
        }
    }
}
