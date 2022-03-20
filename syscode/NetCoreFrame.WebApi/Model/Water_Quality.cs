using SqlSugar;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.WebApi.Model
{
    [SugarTable("water_quality")]
    public class Water_Quality:BaseModel
    {
        [Display(Name = "设备Id")]
        [Description("设备Id")] 
        public string EquipId { get; set; }


        [Display(Name = "Toc")]
        [Description("Toc")]
        public decimal TOC { get; set; }

        [Display(Name = "氨氯")]
        [Description("氨氯")]
        public decimal AD { get; set; }

        [Display(Name = "总磷")]
        [Description("总磷")]
        public decimal ZL { get; set; }

        [Display(Name = "PH")]
        [Description("PH")]
        public decimal PH { get; set; }

        [Display(Name = "流量")]
        [Description("流量")]
        public decimal LL { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
