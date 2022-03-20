using SqlSugar;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.WebApi.Model
{
    [SugarTable("frame_user")]
    public class Frame_User
    {
        [Column("id")]
        [StringLength(50)]
        public string ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Description("创建时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("createtime")]
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        [Description("创建人")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("createby")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Description("修改时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updatetime")]
        public DateTime? UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Display(Name = "修改人")]
        [Description("修改人")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("updateby")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Description("排序")]
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [Description("状态")]
        [StringLength(50)]
        [Column("status")]
        public string Status { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        [Description("是否删除")]
        [Column("isdeleted")]
        public int IsDeleted { get; set; }

        [Display(Name = "用户名称")]
        [Description("用户名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("username")]
        public string UserName { get; set; }

        [Display(Name = "登陆账号")]
        [Description("登陆账号")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("loginid")]
        public string LoginID { get; set; }


        [Display(Name = "登陆密码")]
        [Description("登陆密码")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("password")]
        public string Password { get; set; }

        [Display(Name = "角色名")]
        [Description("角色名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("rolename")]
        public string RoleName { get; set; }


        [Display(Name = "角色ID")]
        [Description("角色ID")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("roleid")]
        public string RoleID { get; set; }

        [Display(Name = "是否在线")]
        [Description("是否在线")]
        [Column("isonline")]
        //1 在线，0 离线
        public int IsOnline { get; set; }


        [Display(Name = "头像路径")]
        [Description("头像路径")]
        [StringLength(200)]
        [Column("pictureurl")]
        public string PictureUrl { get; set; }



        [Display(Name = "部门名称")]
        [Description("部门名称")]
        [StringLength(50)]
        [Column("deptname")]
        public string DeptName { get; set; }

        [Display(Name = "部门Id")]
        [Description("部门Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("deptid")]
        public string DeptID { get; set; }

        [Display(Name = "设备Id")]
        [Description("设备Id")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("equipid")]
        public string EquipId { get; set; }
    }
}
