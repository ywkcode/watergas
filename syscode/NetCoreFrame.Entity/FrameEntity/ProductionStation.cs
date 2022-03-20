using NetCoreFrame.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreFrame.Entity.FrameEntity
{
    [Table("productionstation")]
    public class ProductionStation : CoreBaseEntity
    {
        [Display(Name = "产线名称")]
        [Description("产线名称")]
        [StringLength(100)]
        [Column("productionname")]
        public string ProductionName { get; set; }

        [Display(Name = "工位名称")]
        [Description("工位名称")]
        [StringLength(100)]
        [Column("stationname")]
        public string StationName { get; set; }

        [Display(Name = "IP")]
        [Description("IP")]
        [StringLength(100)]
        [Column("ipnum")]
        public string IPNum { get; set; }

        [Display(Name = "是否工位")]
        [Description("是否工位")]
        [Column("isstation")]
        public bool IsStation { get; set; }


    }
}
