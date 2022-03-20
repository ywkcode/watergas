using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCoreFrame.Core.CommonHelper
{
    /// <summary>
    /// 正则表达式 
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string GetValue(string str, string s, string e)
        {
             
            if (str.IndexOf(s) > -1 && str.IndexOf(e) > -1)
            {
                Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                return rg.Match(str).Value;
            }
            else
            {
                return "0";
            }
        }
    }
}
