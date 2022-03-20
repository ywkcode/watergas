using NetCoreFrame.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreFrame.Entity.FrameEntity
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Table("frame_fileuload")]
    public class Frame_FileUpload : CoreBaseEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [Display(Name = "文件名称")]
        [Description("文件名称")]
        [StringLength(100)]
        [Column("filename")]
        public string FileName { get; set; }

        /// <summary>
        /// 文件物理路径
        /// </summary>
        [Display(Name = "文件物理路径")]
        [Description("文件物理路径")]
        [StringLength(200)]
        [Column("filepath")]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件图片地址
        /// </summary>
        [Display(Name = "文件图片地址")]
        [Description("文件图片地址")]
        [StringLength(2000)]
        [Column("fileimgurl")]
        public string FileImgUrl { get; set; }

        /// <summary>
        /// 文件类别
        /// </summary>
        [Display(Name = "文件类别")]
        [Description("文件类别")]
        [StringLength(50)]
        [Column("filetype")]
        public string FileType { get; set; }

       
    }
}
