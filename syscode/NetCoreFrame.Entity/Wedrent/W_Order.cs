using NetCoreFrame.Entity.BaseEntity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace NetCoreFrame.Entity.Wedrent
{
    /// <summary>
    /// W_Order
    /// </summary>
    public class W_Order : CoreBaseEntity
    {
        [Display(Name = "订单编号")]
        [Description("订单编号")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string OrderNumber { get; set; } 

        [Display(Name = "品类")]
        [Description("品类")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsType { get; set; }

        [Display(Name = "租赁时间")]
        [Description("租赁时间")]
        public DateTime RentTime { get; set; }

        [Display(Name = "客户姓名")]
        [Description("客户姓名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Customer { get; set; }

        [Display(Name = "婚期")]
        [Description("婚期")] 
        public DateTime Weddingdate { get; set; }

        [Display(Name = "联系方式")]
        [Description("联系方式")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string LinkTel { get; set; }

        [Display(Name = "地址")]
        [Description("地址")]
        [StringLength(200, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Address { get; set; }

        [Display(Name = "配饰")]
        [Description("配饰")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string PeiShi { get; set; }

        [Display(Name = "备注")]
        [Description("备注")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Remarks { get; set; }

        [Display(Name = "商品列表")]
        [Description("商品列表")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsList { get; set; }
    }
}
