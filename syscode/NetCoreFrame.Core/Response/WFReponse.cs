using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NetCoreFrame.Core.Response
{
    /// <summary>
    /// WebApi返回数据格式
    /// </summary>
    public class WFReponse
    {
       
        public string info { get; set; }
     
        public int code { get; set; }
      
        public dynamic data { get; set; }

        public WFReponse()
        {
            code = 200;
            info = "响应成功";
        }

    }
}
