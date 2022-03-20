using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core.ApiDto
{
    /// <summary>
    /// 信息显示
    /// </summary>
    public class InfoListDto 
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string FileID { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 下载量
        /// </summary>
        public int DownloadCount { get; set; }

        /// <summary>
        /// 播放次数
        /// </summary>
        public int BoardCount { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public string FileContent { get; set; }

         /// <summary>
         /// 文件标题
         /// </summary>
        public string FileTitle { get; set; }


       

        /// <summary>
        /// 视频时长 
        /// </summary>
        public string  VideoMinLength { get; set; }

        /// <summary>
        /// 下载链接
        /// </summary>
        public string DownloadUrl { get; set; }


        /// <summary>
        /// 封面图片链接
        /// </summary>
        public string CoverImgUrl { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string BgUrl { get; set; }

        /// <summary>
        /// 下载图标Logo
        /// </summary>
        public string DLIconUrl { get; set; }
        
    }
}
