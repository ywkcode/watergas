using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreFrame.Core.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.TagHelpers
{
    /// <summary>
    /// 流程审批
    /// </summary>
    [HtmlTargetElement("flow:check")]
    public class FlowCheckTagHelper : TagHelper
    {
        public List<NodeDetailListDto> Items { set; get; } = new List<NodeDetailListDto>();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "layui-collapse");

            //审批结果
            var container = new TagBuilder("div");
            if (Items.Count()== 0)
            {
                container.Attributes.Add("style", "display:none");
            }
            container.Attributes.Add("class", "layui-colla-item");

            var listItem = "<h2 class='layui-colla-title'>审核明细</h2>"; 
            listItem += " <div class='layui-show layui-colla-content'>";
            listItem += " <div class='layui-form-item'>";
            listItem += " <label class='layui-form-label'>审核结果</label>";
            listItem += "<div class='layui-input-block'>";
            listItem += " <input type= 'radio' name='opnion'   value='同意' title='同意' checked>";
            listItem += " <input type= 'radio' name='opnion'   value='不同意' title='不同意' checked>";
            listItem += " </div>";
            listItem += " </div>";

            listItem += " <div class='layui-form-item'>";
            listItem += " <label class='layui-form-label'>备注</label>";
            listItem += " <div class='layui-input-block'>";
            listItem += "  <input placeholder = '请输入内容' class='layui-input' name='remarks' id='remarks' type='text' ></input>";
            listItem += " </div>";
            listItem += " </div>";
            listItem += " </div>";  
            container.InnerHtml.AppendHtml(listItem);


            var container2 = new TagBuilder("div");
            container2.Attributes.Add("class", "layui-colla-item");

            var listItem2 = "<h2 class='layui-colla-title'>流程明细</h2>"; 
            listItem2 += "<div class='layui-show layui-colla-content'>";
            listItem2 += " <table class='layui-table'>";
            listItem2 += " <thead>";
            listItem2 += "<tr> <th>序号</th> <th>日期</th> <th>审批人</th><th>步骤</th><th>审核结果</th><th>备注</th>  </tr>";
            listItem2 += " </thead>";
            listItem2 += " <tbody>";
            for (int i = 1; i <= Items.Count(); i++)
            {
                listItem2 += "<tr><td>"+(i.ToString())+ "</td><td>" + Items[i-1].HandleDate + "</td><td>" + Items[i-1].Handler + "</td><td>" + Items[i-1].NodeName + "</td><td>" + Items[i-1].HandleResult + "</td><td>" + Items[i-1].HandleOpnion + "</td></tr>";
            }
            listItem2 += " </tbody> </table> </div></div>";
            container2.InnerHtml.AppendHtml(listItem2);

            output.Content.AppendHtml(container);
            output.Content.AppendHtml(container2);
             

        }
    }
}
