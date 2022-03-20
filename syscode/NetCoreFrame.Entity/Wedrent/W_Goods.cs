using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Entity.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreFrame.Entity.Wedrent
{
    /// <summary>
    /// W_Goods
    /// </summary>
    public class W_Goods : CoreBaseEntity
    {
        [Display(Name = "商品名称")]
        [Description("商品名称")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsName { get; set; }

        [Display(Name = "商品编码")]
        [Description("商品编码")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsNumber { get; set; }

        [Display(Name = "品类")]
        [Description("品类")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsType { get; set; }

        [Display(Name = "尺码")]
        [Description("尺码")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsSize { get; set; }


        [Display(Name = "价格")]
        [Description("价格")] 
        public decimal? GoodsPrice { get; set; }

        [Display(Name = "进货商")]
        [Description("进货商")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Supplier { get; set; }

        [Display(Name = "件数")]
        [Description("件数")] 
        public int GoodsCount { get; set; }

        [Display(Name = "商品描述")]
        [Description("商品描述")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string GoodsDescription { get; set; }

        [Display(Name = "采购时间")]
        [Description("采购时间")] 
        public DateTime OrderTime { get; set; }

        [Display(Name = "出租状态")]
        [Description("出租状态")] 
        public string RentStatus { get; set; }

        [Display(Name = "备注")]
        [Description("备注")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Remarks { get; set; }

        public string ImgAttachID { get; set; }







    }
}
