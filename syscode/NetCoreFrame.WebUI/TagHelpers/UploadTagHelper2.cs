using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreFrame.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NetCoreFrame.WebUI.TagHelpers
{
    [HtmlTargetElement("Upload")]
    public class UploadTagHelper2 : TagHelper
    {
        public string Name { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("type", "button");
            output.Attributes.Add("name", Name + "_button");
            output.Attributes.Add("class", "layui-btn layui-icon-picture-fine");
            output.Attributes.Add("onclick", "openFile()");
            output.Attributes.Add("value", "上传");
            output.TagMode = TagMode.StartTagAndEndTag; 
            output.PostElement.SetHtmlContent($@"
             
             <a href='' id='file_a' style='display:none'></a>
             <div style='display:none'> 
             <input id='{Name}' name='{Name}'/>
             <input type='file' id='ajaxfile' name='{Name}file' onchange='doUpload()' /></div>
             <script>
             function openFile() 
            {{
               $('#ajaxfile').click();
             }}
               function doUpload()
            {{
            var formData = new FormData($('#uploadForm')[0]);
            $.ajax({{
                url: '/FileUpload/FileSave',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (returndata) {{
                    if(returndata.filename!=''&&returndata.filename!=undefined)
                    {{
                        debugger;
                        var imgUrl =  returndata.savepath;
                        $('#file_a').attr('href',imgUrl);
                        $('#file_a').text(returndata.filename+'.'+returndata.filetype);
                        $('#file_a').show();
                        $('#{Name}').val(returndata.attachid);
                     }}
                   
                }},
                error: function (returndata) {{
                    console.log(returndata);
                    layer.msg('上传失败！');
                }}
            }});
        }}
             </script>
");
 
        }
    }
}
