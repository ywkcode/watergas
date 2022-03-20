using MySql.Data.MySqlClient;
using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NetCoreFrame.Service
{
    /// <summary>
    /// 业务层基类 所有业务都继承此基类
    /// 实现数据库操作
    /// </summary>
    public class BaseService<T> where T : TopBaseEntity
    {
        /// <summary>
        /// _repository
        /// </summary>
        protected IRepository<T> _repository;


        /// <summary>
        /// 构造函数 注入仓储接口
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;

        }
        /// <summary>
        /// 按id批量删除
        /// </summary>
        /// <param name="ids"></param>
        public void BatchDelete(string[] ids)
        {
             _repository.BatchDelete(u => ids.Contains(u.ID));
            
        }

       

        public T Get(string id)
        {
            return _repository.FindSingle(u => u.ID == id);
        }
        public void Update(T entity)
        {
            _repository.Update(entity);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        public void BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            _repository.BatchUpdate(where, entity);
        }

     
    }
}
