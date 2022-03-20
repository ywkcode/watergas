using NetCoreFrame.Core;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;
 
using NetCoreFrame.Entity.Enum;

namespace NetCoreFrame.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : CoreBaseEntity
    {
        private NetCoreFrameDBContext _context;

        public BaseRepository(NetCoreFrameDBContext context)
        {
            _context = context;
        }
        #region 新增
        /// <summary>
        ///添加
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            #region 初始化  避免在构造中重复添加
            entity.CreateTime = DateTime.Now;
            //entity.ID = Guid.NewGuid().ToString();
            //entity.CreateTime = DateTime.Now;
            //entity.IsDeleted = TrueOrFlase.Flase;
            //entity.Sort = 0;
            #endregion
            //通过EFCore的Data Annotations实现自动生成

            _context.Set<T>().Add(entity);
            SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }
        /// <summary>
        /// 批量添加 List<T>
        /// </summary>
        /// <param name="entities"></param>
        public void BatchAdd(T[] entities)
        {
            _context.Set<T>().AddRange(entities);
            SaveChanges();
        }
        #endregion

        #region 删除

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            SaveChanges();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp"></param>
        public virtual void BatchDelete(Expression<Func<T, bool>> exp)
        {

            //_context.Set<T>().Where(exp).BatchDelete();
            //Z.EntityFramework.Extensions 控件可用
            _context.Set<T>().Where(exp).DeleteFromQuery();
           
            //SaveChanges();
        }
        #endregion

        #region 修改
        public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            //设置数据的状态为Modified
            entry.State = EntityState.Modified;
            entry.Entity.UpdateTime = DateTime.Now;
            //数据没有发生变化 返回
            if (!this._context.ChangeTracker.HasChanges())
            {
                return;
            }
            SaveChanges();
        }
        /// <summary>
        /// 批量更新
        /// 如：Update(u =>u.Id==1,u =>new User{Name="ok"});
        /// </summary>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        public void BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            _context.Set<T>().Where(where).Update(entity);
        }
        #endregion

        #region 查询
       
        /// <summary>
        /// 单个查询，且不被上下文跟踪
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
        {
            var dbset = _context.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbset = dbset.Where(exp);
            return dbset;
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageindex">当前页数</param>
        /// <param name="pagesize">每页显示个数</param>
        /// <param name="orderby"> 排序，格式如："Id"/"Id descending"</param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> Find(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            if (pageindex < 1) pageindex = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "CreateTime descending";

            return Filter(exp).OrderBy(orderby).Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }
        
        #endregion 

        #region 执行SQL
        public int ExecuteSql(string sql)
        {
           
            return _context.Database.ExecuteSqlRaw(sql);
        }

        #endregion


        #region 获得记录个数
        /// <summary>
        /// 获得记录个数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }
        #endregion

        #region 是否存在
        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            
            return _context.Set<T>().Any(exp);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion



    }
}
