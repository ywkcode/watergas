using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.TagHelpers
{
    [HtmlTargetElement("form:DateTime")]
    /// <summary>
    /// 日期控件
    /// </summary>
    public class DateTimeTagHelper : TagHelper
    {
        /// <summary>
        /// 控件Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示当前时间
        /// </summary>
        public bool IsNow { get; set; }

        /// <summary>
        /// 选中值
        /// </summary>
        public string SelectedValue { get; set; }

        /// <summary>
        /// 是否显示时分秒
        /// </summary>
        public bool IsShowMinute { get; set; }

        public int WidthPx { get; set; } = 0;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("type", "text");
            output.Attributes.Add("class", "layui-input");
            output.Attributes.Add("id", Name);
            output.Attributes.Add("name", Name);
            string StyleStr = "display:inline-block;";
            if (WidthPx > 0)
            {
                StyleStr += "width:"+WidthPx.ToString() + "px;";
            }

            output.Attributes.Add("style", StyleStr);
            if (IsShowMinute)
            {
                output.Attributes.Add("placeholder", "yyyy-MM-dd HH:mm:ss");
            }
            else {
                output.Attributes.Add("placeholder", "yyyy-MM-dd");
            }
            
            if (IsNow)
            {
              
                output.Attributes.Add("value", IsShowMinute==true? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"): DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else if (!string.IsNullOrEmpty(SelectedValue))
            {
                output.Attributes.Add("value", SelectedValue);
            }
            output.TagMode = TagMode.StartTagAndEndTag;
            var type = IsShowMinute == true ? ",type: 'datetime'" : "";
            output.PostElement.SetHtmlContent($@" 
           <script>
            layui.use('laydate', function(){{
                  var laydate = layui.laydate;
  
                  //执行一个laydate实例
                  laydate.render({{
                    elem: '#{Name}' //指定元素
                ,change:function(value){{
                      $('#{Name}').val(value);   
                }}{type}
                  }});
                }});
            </script>");
        }
    }
}
