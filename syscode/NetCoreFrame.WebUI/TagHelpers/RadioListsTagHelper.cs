using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.TagHelpers
{
    /// <summary>
    /// 表单单选
    /// </summary>
    [HtmlTargetElement("form:radio")]
    public class RadioListsTagHelper : TagHelper
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public List<SelectListViewModel> Items { set; get; } = new List<SelectListViewModel>();

        public readonly Frame_CodesValueService _codesValueService;

        public RadioListsTagHelper(Frame_CodesValueService codesValueService)
        {
            _codesValueService = codesValueService;
        }
        /// <summary>
        /// Input名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 选中项
        /// </summary>
        public string SelectdValue { get; set; }

        /// <summary>
        /// 代码项名称
        /// </summary>
        public string CodeName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            
          
            output.TagName = "div";
            output.Attributes.Add("class", "layui-input-block ");
            output.Attributes.Add("style", "margin-left:0px");





            int Count = 1;
            foreach (var item in Items)
            {
                var container = new TagBuilder("input");
                container.Attributes.Add("type", "radio");
                container.Attributes.Add("id", Name+"_"+ Count.ToString());
                container.Attributes.Add("name", Name);
                container.Attributes.Add("value", item.Value);
                container.Attributes.Add("title", item.Value); 
                output.Content.AppendHtml(container);
                Count++;
            }
            if (!string.IsNullOrEmpty(CodeName))
            {
                var codevalList = _codesValueService.GetSelectsList(CodeName);
                foreach (var item in codevalList)
                {
                    var container = new TagBuilder("input");
                    container.Attributes.Add("type", "radio");
                    container.Attributes.Add("name", Name);
                    container.Attributes.Add("value", item.Value);
                    container.Attributes.Add("title", item.Text);
                    output.Content.AppendHtml(container);
                } 
            }
           

            if (!string.IsNullOrEmpty(SelectdValue))
            {
                output.PostElement.SetHtmlContent($@"
                     <script >  
                     $(function()
                     {{
                         $('input[name = '{Name}']:checked').val();
                         $('#{Name}').siblings('div.layui-form-select').find('dl').find(""dd[lay-value='{SelectdValue}']"").click();
                     }}) 
                    </script>");
            }
         
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
