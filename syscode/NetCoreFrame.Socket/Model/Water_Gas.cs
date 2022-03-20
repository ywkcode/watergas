using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.SocketConsole.Model
{
    public class Water_Gas:BaseModel
    {
        [Display(Name = "设备Id")]
        [Description("设备Id")]
        public string EquipId { get; set; } = "G001";


        [Display(Name = "硫化氢")]
        [Description("硫化氢")]
        public decimal H2S { get; set; }

        [Display(Name = "氯化氢")]
        [Description("氯化氢")]
        public decimal HCL { get; set; }

        [Display(Name = "氯气")]
        [Description("氯气")]
        public decimal CL2 { get; set; }

        [Display(Name = "氨气")]
        [Description("氨气")]
        public decimal NH3 { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
