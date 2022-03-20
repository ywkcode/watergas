using NetCoreFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity.ViewModel
{
    /// <summary>
    /// 流程提交实体类
    /// </summary>
    public class WorkFLowSubmitViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
       
        /// <summary>
        /// 备注
        /// </summary>
        public string HandleOpnion { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public TrueOrFlase HandleResult { get; set; }
    }
}
