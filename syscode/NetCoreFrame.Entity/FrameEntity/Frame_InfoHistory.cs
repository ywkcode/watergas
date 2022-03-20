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
    /// 信息搜索历史
    /// </summary>
    [Table("frame_infohistory")]
    public class Frame_InfoHistory : CoreBaseEntity
    {
        /// <summary>
        /// CWIOS用户ID
        /// </summary>
        [Display(Name = "CWIOS用户ID")]
        [Description("CWIOS用户ID")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("cuserid")]
        public string CUserId { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        [Display(Name = "搜索关键词")]
        [Description("搜索关键词")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("keyword")]
        public string KeyWord { get; set; }
    }
}
