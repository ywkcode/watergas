using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreFrame.Core.ApiDto
{
     public class FlowUserDto
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>		
        [Column("f_USERID")]
        public string f_UserId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>	
        [Column("f_ENCODE")]
        public string f_EnCode { get; set; }
        /// <summary>
        /// 账户
        /// </summary>	
        [Column("f_ACCOUNT")]
        public string f_Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>		
        [Column("f_PASSWORD")]
        public string f_Password { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>	
        [Column("f_SECRETKEY")]
        public string f_Secretkey { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Column("f_REALNAME")]
        public string f_RealName { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>	
        [Column("f_NICKNAME")]
        public string f_NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>	
        [Column("f_HEADICON")]
        public string f_HeadIcon { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>	
        [Column("f_QUICKQUERY")]
        public string f_QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>	
        [Column("f_SIMPLESPELLING")]
        public string f_SimpleSpelling { get; set; }
        /// <summary>
        /// 性别
        /// </summary>	
        [Column("f_GENDER")]
        public int? f_Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>	
        [Column("f_BIRTHDAY")]
        public DateTime? f_Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>	
        [Column("f_MOBILE")]
        public string f_Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        [Column("f_TELEPHONE")]
        public string f_Telephone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>	
        [Column("f_EMAIL")]
        public string f_Email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>		
        [Column("f_OICQ")]
        public string f_OICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>	
        [Column("f_WECHAT")]
        public string f_WeChat { get; set; }
        /// <summary>
        /// MSN
        /// </summary>		
        [Column("f_MSN")]
        public string f_MSN { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>		
        [Column("f_COMPANYID")]
        public string f_CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        [Column("f_DEPARTMENTID")]
        public string f_DepartmentId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>	
        [Column("f_SECURITYLEVEL")]
        public int? f_SecurityLevel { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>		
        [Column("f_OPENID")]
        public int? f_OpenId { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>		
        [Column("f_QUESTION")]
        public string f_Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>	
        [Column("f_ANSWERQUESTION")]
        public string f_AnswerQuestion { get; set; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>		
        [Column("f_CHECKONLINE")]
        public int? f_CheckOnLine { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        [Column("f_ALLOWSTARTTIME")]
        public DateTime? f_AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        [Column("f_ALLOWENDTIME")]
        public DateTime? f_AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        [Column("f_LOCKSTARTDATE")]
        public DateTime? f_LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        [Column("f_LOCKENDDATE")]
        public DateTime? f_LockEndDate { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>	
        [Column("f_SORTCODE")]
        public int? f_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>	
        [Column("f_DELETEMARK")]
        public int? f_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>	
        [Column("f_ENABLEDMARK")]
        public int? f_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        [Column("f_DESCRIPTION")]
        public string f_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        [Column("f_CREATEDATE")]
        public DateTime? f_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        [Column("f_CREATEUSERID")]
        public string f_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>	
        [Column("f_CREATEUSERNAME")]
        public string f_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>	
        [Column("f_MODIFYDATE")]
        public DateTime? f_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        [Column("f_MODIFYUSERID")]
        public string f_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        [Column("f_MODIFYUSERNAME")]
        public string f_ModifyUserName { get; set; }
        #endregion
    }
}
