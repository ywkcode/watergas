using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace NetCoreFrame.Entity.FrameEntity
{
    /// <summary>
    /// Frame_InfoComments
    /// </summary>
    [Table("frame_infocomments")]
    public class Frame_InfoComments : CoreBaseEntity
    {
        /// <summary>
        /// 评论信息
        /// </summary>
        [Display(Name = "评论信息")]
        [Description("评论信息")]
        [StringLength(200, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("comments")]
        public string Comments { get; set; }

        /// <summary>
        /// 信息Id
        /// </summary>
        [Display(Name = "信息Id")]
        [Description("信息Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("fileid")]
        public string FileID { get; set; }

        /// <summary>
        /// 评论人
        /// </summary>
        [Display(Name = "评论人")]
        [Description("评论人")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("commenter")]
        public string Commenter { get; set; }

        /// <summary>
        /// CUserId
        /// </summary>
        [Display(Name = "CUserId")]
        [Description("CUserId")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("cuserid")]
        public string CUserId { get; set; }
        
    }
}
