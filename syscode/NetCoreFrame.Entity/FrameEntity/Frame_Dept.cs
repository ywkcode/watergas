 
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
    /// 部门管理
    /// </summary>
    [Table("frame_dept")]
    public class Frame_Dept : CoreBaseEntity
    {
        [Display(Name = "部门名称")]
        [Description("部门名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("deptname")]
        public string DeptName { get; set; }

        [Display(Name = "部门编码")]
        [Description("部门编码")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("deptcode")]
        public string DeptCode { get; set; }


        [Display(Name = "部门ID")]
        [Description("部门ID")]
        [Column("deptid")]
        public int DeptID { get; set; }

        [Display(Name = "父部门ID")]
        [Description("父部门ID")]
        [Column("pdeptid")]
        public int PDeptID { get; set; }
    }
}
