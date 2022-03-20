using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
namespace NetCoreFrame.Repository.Interface
{
    //仓储接口
    public interface IRepository<T> where T : class
    {
        T FindSingle(Expression<Func<T, bool>> exp = null);
      
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null);

        int GetCount(Expression<Func<T, bool>> exp = null);

        #region 新增
        void Add(T entity);

        void BatchAdd(T[] entities);
        #endregion

        #region 删除
        /// <summary>
        /// 单条数据删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        void BatchDelete(Expression<Func<T, bool>> exp);
        #endregion

        #region  修改
        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        void Update(T entity);
        /// <summary>
        /// 批量更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        void BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
        #endregion

        #region 是否存在实例
        bool IsExist(Expression<Func<T, bool>> exp);
        #endregion

        void SaveChanges();

        int ExecuteSql(string sql);
    }
}
