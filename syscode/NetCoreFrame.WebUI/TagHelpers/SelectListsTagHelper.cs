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
    /// 下拉框正式版
    /// </summary>
    [HtmlTargetElement("SelectLists")]
    public class SelectListsTagHelper : TagHelper
    {
        public readonly Frame_CodesValueService  _codesValueService;
        public SelectListsTagHelper(Frame_CodesValueService codesValueService) {
            _codesValueService = codesValueService;
    }
        public List<SelectListViewModel> Items { set; get; } = new List<SelectListViewModel>();
        
        /// <summary>
        /// 数据字典名称
        /// </summary>
        public  string CodeName { set; get; } 
        /// <summary>
        /// 字段Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 首先显示名称
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 选中名称显示
        /// </summary>
        public string SelectdValue { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            //当前节点的标签名
            //output.TagName = "select";
            //output.Attributes.SetAttribute("name", Name);
            //output.Attributes.SetAttribute("lay-verify", "");
            //output.Attributes.SetAttribute("lay-search", "");
            //output.TagMode = TagMode.StartTagAndEndTag;

            //foreach (var item in Items)
            //{
            //    var listItem = $"<option value=\"{item.ID}\"  >{item.Text}</option>";
            //    output.Content.AppendHtml(listItem);
            //}


            //var container = new TagBuilder("div");
            //container.Attributes["class"] = "layui-input-block";
            //output.PreElement.SetHtmlContent(container);

            var codevalList = _codesValueService.GetSelectsList(CodeName);
            output.TagName = "div";
            output.Attributes.Add("class", "layui-input-block"); 
            output.Attributes.Add("style", "margin-left:0px");


            var container = new TagBuilder("select");
            container.Attributes.Add("name", Name);
            container.Attributes.Add("id", Name); 
            container.Attributes.Add("lay-verify", "");
            container.Attributes.Add("lay-search", "");

            if (codevalList.Count() == 0)
            {
                codevalList = Items;
            }
            if (!string.IsNullOrEmpty(FirstName))
            {
                var listItem = "<option value=\""+ FirstName + "\"  >无</option>";
                container.InnerHtml.AppendHtml(listItem);
            }
            foreach (var item in codevalList)
            {
                var listItem = $"<option value=\"{item.Value}\"  >{item.Text}</option>";
                container.InnerHtml.AppendHtml(listItem);
            }

            output.Content.AppendHtml(container);
            if (!string.IsNullOrEmpty(SelectdValue))
            {
                output.PostElement.SetHtmlContent($@"
                     <script >  
                     $(function()
                     {{
                        
                         $('#{Name}').siblings('div.layui-form-select').find('dl').find(""dd[lay-value='{SelectdValue}']"").click();
                     }}) 
                    </script>");
            }

            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
