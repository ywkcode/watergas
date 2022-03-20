using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NetCoreFrame.Core.ApiDto.EnumStatus;

namespace NetCoreFrame.Core.ApiDto
{
    /// <summary>
    /// 查询
    /// </summary>
    public class InfoListSearchDto:BaseDto
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public   InfoType InfoType { get;set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string KeyWord { get; set; }
    }
}
