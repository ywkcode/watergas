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
    /// 数据字典
    /// </summary>
    [Table("frame_codes")]
    public class Frame_Codes : CoreBaseEntity
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [Display(Name = "字典名称")]
        [Description("字典名称")]
        [StringLength(100)]
        [Column("codename")]
        public string CodeName { get; set; }

        /// <summary>
        /// CodeID
        /// </summary>
        //[ScaffoldColumn(false)]
        [Display(Name = "CodeID")]
        [Description("CodeID")]
        [Column("codeid")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodeID { get; set; }
    }
}
