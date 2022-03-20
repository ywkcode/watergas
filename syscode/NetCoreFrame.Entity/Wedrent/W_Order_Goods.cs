using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Entity.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Entity.Wedrent
{
    /// <summary>
    /// W_Order_Goods
    /// </summary>
    public class W_Order_Goods : CoreBaseEntity
    {
        [Display(Name = "订单Id")]
        [Description("订单Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string OrderId { get; set; }

        [Display(Name = "商品Id")]
        [Description("商品Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsId { get; set; }

        [Display(Name = "是否归还")]
        [Description("是否归还")]
        public bool IsReturnBack { get; set; }
    }
}
