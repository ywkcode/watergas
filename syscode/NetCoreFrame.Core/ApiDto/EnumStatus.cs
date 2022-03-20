using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core.ApiDto
{  
    /// <summary>
    /// 枚举类
    /// </summary>
public class EnumStatus
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumStatus() { }

        /// <summary>
        /// 信息类型
        /// </summary>
        public enum InfoType
        { 
            /// <summary>
            /// 无
            /// </summary>
            Empty=0,

            /// <summary>
            /// 视频
            /// </summary>
            Video=1,
            /// <summary>
            /// 语音
            /// </summary>
            Audio=2,

            /// <summary>
            /// 文件
            /// </summary>
            File=3
        }

    }
}
