using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NetCoreFrame.Core.Response
{
    /// <summary>
    /// WebApi返回数据格式
    /// </summary>
    public class ApiReturnValue
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [DataMember(Name = "msg")]
        public string Msg { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        [DataMember(Name = "result")]
        public dynamic Result { get; set; }

        public ApiReturnValue() {
            Success = true;
            Msg = "操作成功"; 
        }

    }
}
