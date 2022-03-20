using NetCoreFrame.Entity.BaseEntity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Entity.Water
{
    /// <summary>
    /// 水质
    /// </summary>
    [Table("water_quality")]
    public class Water_Quality 
    {
        [StringLength(50)]
        [Column("id")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "设备Id")]
        [Description("设备Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("equipid")]
        public string EquipId { get; set; }


        [Display(Name = "Toc")]
        [Description("Toc")]
        [Column("toc")]
        public decimal TOC { get; set; }

        [Display(Name = "氨氯")]
        [Description("氨氯")]
        [Column("ad")]
        public decimal AD { get; set; }

        [Display(Name = "总磷")]
        [Description("总磷")]
        [Column("zl")]
        public decimal ZL { get; set; }

        [Display(Name = "PH")]
        [Description("PH")]
        [Column("ph")]
        public decimal PH { get; set; }

        [Display(Name = "流量")]
        [Description("流量")]
        [Column("ll")]
        public decimal LL { get; set; }

        [Display(Name = "创建日期")]
        [Column("createtime")]
        public DateTime CreateTime { get; set; }

        [Column("iserror")]
        public bool IsError { get; set; }
    }
}
