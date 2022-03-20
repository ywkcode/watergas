using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Entity.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Entity.Wedrent
{
    public class  W_GoodsDetail : CoreBaseEntity
    {
        [Display(Name = "订单Id")]
        [Description("订单Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string OrderId { get; set; }



        [Display(Name = "商品编码")]
        [Description("商品编码")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsId{ get; set; }

        [Display(Name = "操作日期")]
        [Description("操作日期")]
     
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// true 归还  false是出租
        /// </summary>
        [Display(Name = "是否归还")]
        [Description("是否归还")]
        
        public bool IsReturnBack { get; set; }

        [Display(Name = "备注")]
        [Description("备注")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Remarks { get; set; }


       
    }
}
