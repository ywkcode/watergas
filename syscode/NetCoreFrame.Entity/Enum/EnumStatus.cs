using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity.Enum
{
    /// <summary>
    /// 枚举类
    /// </summary>
    public class EnumStatus
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumStatus() { }
    }

    /// <summary>
    /// 关系类型
    /// </summary>
    public enum RelationType {
        /// <summary>
        /// 角色—用户
        /// </summary>
        RoleUser = 1,
        /// <summary>
        /// 角色-模块 
        /// </summary>
        RoleModule = 2,

        /// <summary>
        /// 主表-附件表
        /// </summary>
        Attach=3,

        /// <summary>
        /// 工位-文件
        /// </summary>
        Station=4
    }

    /// <summary>
    /// 真或假
    /// </summary>
    public enum TrueOrFlase { 
        /// <summary>
        /// 真
        /// </summary>
        True=1,     
        /// <summary>
        /// 假
        /// </summary>
        Flase=0,

        /// <summary>
        /// null
        /// </summary>
        Null=-1
    }
    /// <summary>
    /// 奖品类型
    /// </summary>
    public enum PrizeType { 
        /// <summary>
        /// 分享豆
        /// </summary>
        ShareDou=1,
        /// <summary>
        /// 红包
        /// </summary>
        RedPaper=2
    }
      
    /// <summary>
    /// Cache策略类型  永久/绝对过期/滑动过期
    /// </summary>
    public enum ObsloteType
    {
        /// <summary>
        /// 永久
        /// </summary>
        Never,
        /// <summary>
        /// 绝对过期
        /// </summary>
        Absolutely,
        /// <summary>
        /// 滑动过期 如果期间查询或更新，就再次延长
        /// </summary>
        Relative
    }

    /// <summary>
    /// 数据状态
    /// </summary>
    public enum DataStatus 
    { 
        /// <summary>
        /// 禁用
        /// </summary>
        Forbiden=-1,
        /// <summary>
        /// 保存
        /// </summary>
        Save=0,
        /// <summary>
        /// 发布
        /// </summary>
        Publish=1,

    }


    public enum RentStatus
    { 
        /// <summary>
        /// 可租用 
        /// </summary>
        CanRent=0,

        /// <summary>
        /// 租用中  
        /// </summary>
        InRent=1
    }

    
}
