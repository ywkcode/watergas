using Autofac;
using NetCoreFrame.Entity.BaseEntity;
using NetCoreFrame.Repository;
using NetCoreFrame.Repository.Interface;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Module = Autofac.Module;
namespace NetCoreFrame.Service
{
    public   class AutofacModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            ////拦截器
            ////builder.Register(c => new AOPTest());
            ////注入类
            ////builder.RegisterType<UsersService>().As<UsersIService>().PropertiesAutowired().EnableInterfaceInterceptors();

            ////程序集注入
            //var IRepository = Assembly.Load("DL.IRepository");
            //var Repository = Assembly.Load("DL.Repository");

            ////根据名称约定（仓储层的接口和实现均以Repository结尾），实现服务接口和服务实现的依赖
           
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
              
        }
    }
}
 
