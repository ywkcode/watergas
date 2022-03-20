using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Core.WorkFlow
{
    /// <summary>
    /// 流程节点明细
    /// </summary>
    public class NodeDetailListDto
    {
        /// <summary>
        /// 处理日期
        /// </summary>
        public string HandleDate { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string Handler { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string NodeName { get; set; }


        /// <summary>
        ///   处理结果（同意/不同意）1/0   
        /// </summary>
        public string HandleResult { get; set; }

        /// <summary>
        ///   处理意见
        /// </summary>
        public string HandleOpnion { get; set; }
    }
}
