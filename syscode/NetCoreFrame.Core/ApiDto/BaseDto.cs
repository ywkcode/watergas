using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core.ApiDto
{
    /// <summary>
    /// BaseDto
    /// </summary>
    public class BaseDto
    {

        public BaseDto()
        {
            this.PageIndex = 1;
            this.PageSize = 4;
        }
        /// <summary>
        /// 当前页 默认1
        /// </summary>
        public int PageIndex { get; set; }  

        /// <summary>
        /// 每页个数 默认4
        /// </summary>
        public int PageSize { get; set; }  
    }
}
