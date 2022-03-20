using NetCoreFrame.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCoreFrame.Entity.ViewModel
{
    /// <summary>
    /// 商家入驻-申请
    /// </summary>
    public class BusinessApplyViewModel  
    {
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Display(Name = "联系人姓名")]
        [Description("联系人姓名")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [Display(Name = "联系方式")]
        [Description("联系方式")]
        [StringLength(50)]
        public string LinkTel { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Description("店铺名称")]
        [StringLength(50)]
        public string ShopName { get; set; }


        /// <summary>
        /// 店铺地址
        /// </summary>
        [Display(Name = "店铺地址")]
        [Description("店铺地址")]
        [StringLength(50)]
        public string ShopAddress { get; set; }
    }
}
