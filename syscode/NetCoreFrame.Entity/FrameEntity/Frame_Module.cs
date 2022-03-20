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
    /// 模块表
    /// </summary>
    [Table("frame_module")]
    public class Frame_Module : CoreBaseEntity
    {
        [Display(Name = "模块名称")]
        [Description("模块名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "模块Id")]
        [Description("模块Id")]
        [Column("moduleid")]
        public int ModuleId { get; set; }


        [Display(Name = "上级模块ID")]
        [Description("上级模块ID")]
        [Column("pmoduleid")]
        public int PModuleId { get; set; }

        [Display(Name = "模块编号")]
        [Description("模块编号")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("modulecode")]
        public string ModuleCode { get; set; }

        [Display(Name = "上级模块名称")]
        [Description("上级模块名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("pmodulename")]
        public string PModuleName { get; set; }

        [Display(Name = "Url地址")]
        [Description("Url地址")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("url")]
        public string Url { get; set; }

        [Display(Name = "图标")]
        [Description("图标")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("iconname")]
        public string IconName { get; set; }



    }
}
