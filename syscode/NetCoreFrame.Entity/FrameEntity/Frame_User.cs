using NetCoreFrame.Entity.BaseEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Entity.FrameEntity
{
    [Table("frame_user")]
    public class Frame_User : CoreBaseEntity
    {
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
