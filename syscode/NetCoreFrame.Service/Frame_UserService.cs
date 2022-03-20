using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCoreFrame.Core.ApiDto;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.Enum;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
namespace NetCoreFrame.Service
{
    /// <summary>
    /// 用户Service
    /// </summary>
    public class Frame_UserService : BaseService<Frame_User>
    {
        
       
        private NetCoreFrameDBContext _context = null;
        public Frame_UserService( IRepository<Frame_User> repository, NetCoreFrameDBContext context ) : base(repository)
        {
            
            _context = context;
            
        }

        public void Add(Frame_User user)
        {
            _repository.Add(user);
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Frame_User CheckLogin(string LoginID, string Password)
        {
            Password = MD5Helper.Get32MD5(Password);
            return _repository.Find(s => s.LoginID == LoginID && s.Password == Password).FirstOrDefault();
        }

        public ProductionStation CheckLogin(string IPNum)
        {
            return _context.ProductionStation.FirstOrDefault(s => s.IPNum == IPNum && s.IsDeleted == TrueOrFlase.Flase);
             
        }

        public Frame_User GetFrameUser(string UserId)
        {
            var user = _repository.Find(s => s.ID == UserId).FirstOrDefault();
            //var relationList = from a in _context.Frame_Relations.Where(s => s.SecondId == UserId && s.RelationType == "RoleUser")
            //                   join b in _context.Frame_Role on a.FirstId equals b.ID
            //                   select b;
            //string roleNames = "";
            //string roleIds = "";
            //foreach (var model in relationList)
            //{
            //    roleNames += model.RoleName + ",";
            //    roleIds += model.ID + ",";
            //}
            return new Frame_User
            {
                DeptID = user?.DeptID,
                DeptName = user?.DeptName,
                ID = user?.ID,
                LoginID = user?.LoginID,
                UserName = user?.UserName,
                //RoleName = roleNames.TrimEnd(','),
                //RoleID = roleIds.TrimEnd(',')
            };

        }

        public void SignOut(string userid)
        {
            var user = _repository.Find(s => s.ID == userid).FirstOrDefault();
            if (user != null)
            {
                user.IsOnline = 0;
                _repository.Update(user);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request)
        {

            var userlist = from a in _context.Frame_User
                           join b in _context.Frame_Relations on a.ID equals b.SecondId
                           join c in _context.Frame_Role on b.FirstId equals c.ID into grouping
                           from temp in grouping.DefaultIfEmpty()
                           select new
                           {
                               ID = a.ID,
                               UserName = a.UserName,
                               LoginID = a.LoginID,
                               RoleName = temp.RoleName,
                               CreateTime=a.CreateTime,
                               EquipId=a.EquipId
                           };

            userlist = userlist.OrderByDescending(s=>s.RoleName).Skip((request.page - 1) * request.limit).Take(request.limit);
            return new TableData
            {
              
                count = userlist.Count(),
                data = userlist
            };
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public TableData Regist(string name,string pwd)
        {
            TableData data = new TableData();
            var user = _repository.Find(s => s.UserName == name).FirstOrDefault();
            if (user != null)
            {
                _repository.Add(new Frame_User()
                {
                    UserName = name,
                    Password = MD5Helper.Get32MD5(pwd)
                });
            }
            else {
                data.status = false;
                data.msg = "用户名："+ name+"已注册，请勿重复申请";
            }
            return data;
        }
        /// <summary>
        /// 获取在线客服列表
        /// </summary>
        /// <returns></returns>
        public TableData GetOnlineCallerList()
        {
            var callerList = _repository.Find(s => s.IsOnline == 1);
            return new TableData
            {
                code = 200,
                data = callerList
            };
        }
        public void CreateAccount(Frame_User model)
        {
            PageResponse pageResponse = new PageResponse();
          

            #region 新建账号   
            string AccountId = Guid.NewGuid().ToString();
            _repository.Add(new Frame_User()
            {
                ID = AccountId,
                LoginID = model.LoginID,
                UserName=model.UserName,
                RoleID = model.RoleID, 
                Password = MD5Helper.Get32MD5(model.Password), 
                EquipId=model.EquipId
            });
            #endregion

            #region 新建角色用户关系

            _context.Frame_Relations.Add(new Frame_Relations()
            {
                FirstId = model.RoleID,
                SecondId = AccountId,
                RelationType = RelationType.RoleUser
            });
            _context.SaveChanges();
            #endregion

 
        }
        public List<FlowUserDto> GetList(string companyId)
        {
            List<FlowUserDto> list = (from a in _context.Frame_User
                                     select new FlowUserDto
                                     {
                                         f_RealName=a.UserName,
                                         f_UserId=a.ID, 
                                         f_DepartmentId="",
                                         f_NickName=a.UserName,
                                         
                                     }).ToList();
             return list; 
        }

        public string LoadSelectTree() 
        {
            StringBuilder sb = new StringBuilder();
            var DeptList = _context.Frame_Dept.Where(s => s.IsDeleted == 0).ToList();
            var Userlist = _context.Frame_User.Where(s => s.IsDeleted == 0).ToList();
            //获取收个部门信息 PDeptID=0 不能修改
            var TopDept = DeptList.Where(s => s.DeptCode == "0").FirstOrDefault();
            sb.Append("[{name:\"" + TopDept.DeptName + "\",id:0,treecode:\"" + TopDept.DeptCode + "\",open:true,children:[");
            var deptchildlist = DeptList.Where(s => s.PDeptID == 1).ToList();
            //目前假定公司只有一级
            int deptCount = 1;
            int userCount = 1;
            
            foreach (var deptMol in deptchildlist)
            {
                var userchildlist = Userlist.Where(s => s.DeptID == deptMol.ID).OrderBy(s=>s.UserName);
                sb.Append("{name:\"" + deptMol.DeptName + "\",id:"+ deptCount + ",treeid:\""+deptMol.ID+"\",treecode:\"" + deptMol.DeptCode + "\",open:"+(userchildlist.Count()>0?"true":"false")+"");
                if (userchildlist.Count() > 0)
                {
                    sb.Append(",children:[");
                }
                userCount = 1;
                foreach (var userMol in userchildlist)
                {
                    sb.Append("{name:\"" + userMol.UserName + "\",id:"+ userCount + ",treeid:\""+ userMol.ID+"\",treename:\""+deptMol.DeptName+"\",treecode:\"" + deptMol.DeptCode + "\",  open:false}");
                    if (userCount != userchildlist.Count())
                    {
                        sb.Append(",");
                    }
                    userCount++;
                }
                if (userchildlist.Count() > 0)
                {
                    sb.Append("]");
                }
                sb.Append("}");
                if (deptCount != deptchildlist.Count())
                {
                    sb.Append(",");
                }
               
                deptCount++;
            }
            sb.Append("]}]");
            return sb.ToString();
        }

        protected string GetDeptChild(int DeptId, List<Frame_Module> list, string Type)
        {
            StringBuilder sb = new StringBuilder();
            var CheckList = list;
            var OutCheckList = list;
            int deptChildCount = list.Where(s => s.PModuleId == DeptId).Count();

            while (deptChildCount > 0)
            {
                foreach (var dept in list.Where(s => s.PModuleId == DeptId))
                {
                    if (Type == "Single")
                    {
                        sb.Append("{title:\"" + dept.Name + "\",id:" + dept.ModuleId + ",treecode:\"" + dept.ModuleCode + "\",treeid:\"" + dept.ID + "\",spread:true");
                        if (list.Where(s => s.PModuleId == dept.ModuleId).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.ModuleId, list, Type) + "]");
                        }
                    }
                    else
                    {
                        sb.Append("{name:\"" + dept.Name + "\",id:" + dept.ModuleId + ",treecode:\"" + dept.ModuleCode + "\",treeid:\"" + dept.ID + "\",open:true");
                        if (list.Where(s => s.PModuleId == dept.ModuleId).Count() > 0)
                        {
                            sb.Append(",children:[" + GetDeptChild(dept.ModuleId, list, Type) + "]");
                        }
                    }

                    sb.Append("},");
                    deptChildCount--;
                }
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
