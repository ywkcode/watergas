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
    /// Frame_Charts
    /// </summary>
    [Table("frame_charts")]
    public class Frame_Charts : CoreBaseEntity
    {
        /// <summary>
        /// 报表简介
        /// <summary>
        [Display(Name = "报表简介")]
        [Description("报表简介")]
        [StringLength(200, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("chartinfo")]
        public string ChartInfo { get; set; }

        /// <summary>
        /// 连接字符串
        /// <summary>
        [Display(Name = "连接字符串")]
        [Description("连接字符串")]
        [StringLength(200, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("database")]
        public string DataBase { get; set; }

        /// <summary>
        /// 报表类型
        /// <summary>
        [Display(Name = "报表类型")]
        [Description("报表类型")]
        [Column("charttype")]
        public string ChartType { get; set; }

        /// <summary>
        /// 图标SQL
        /// <summary>
        [Display(Name = "图标SQL")]
        [Description("图标SQL")]
        [StringLength(1000, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("datasql")]
        public string DataSQL { get; set; }

        /// <summary>
        /// 报表标题
        /// <summary>
        [Display(Name = "报表标题")]
        [Description("报表标题")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("charttitle")]
        public string ChartTitle { get; set; }

        /// <summary>
        /// 报表风格
        /// <summary>
        [Display(Name = "报表风格")]
        [Description("报表风格")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string ChartStyle { get; set; }

        /// <summary>
        /// 报表风格2
        /// <summary>
        [Display(Name = "报表风格")]
        [Description("报表风格")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string ChartStyle2 { get; set; }
    }
}
