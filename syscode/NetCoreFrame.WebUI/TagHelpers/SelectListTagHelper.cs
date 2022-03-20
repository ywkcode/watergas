using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreFrame.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.TagHelpers
{
    [HtmlTargetElement("SelectList")]
    public class SelectListTagHelper : TagHelper
    {
        public List<SelectListViewModel> Items { set; get; } = new List<SelectListViewModel>();

        public string Name { get; set; }
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

            output.TagName = "div";
            output.Attributes.Add("class", "layui-input-block");
            output.Attributes.Add("style", "margin-left:0px");


            var container = new TagBuilder("select");
            container.Attributes.Add("name", Name);
            container.Attributes.Add("lay-verify", "");
            container.Attributes.Add("lay-search", "");
            foreach (var item in Items)
            {
                var listItem = $"<option value=\"{item.ID}\"  >{item.Text}</option>";
                container.InnerHtml.AppendHtml(listItem);
            }

            output.Content.AppendHtml(container); 
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
