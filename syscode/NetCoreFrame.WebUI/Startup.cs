using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minio;
using NetCoreFrame.Entity;
using NetCoreFrame.Repository;
using NetCoreFrame.Repository.Interface;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using NetCoreFrame.WebUI.Filter;
using NetCoreFrame.WebUI.Views.Hubs;

namespace NetCoreFrame.WebUI
{
    public class Startup
    {
        static Socket serverSocket;
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注入Session 
            services.AddSession(options =>
                {
                    //设置Session过期时间
                    options.IdleTimeout = TimeSpan.FromDays(10);
                    options.Cookie.HttpOnly = true;
                }
            );
            services.AddHttpContextAccessor();
            #endregion
            services.AddMvc(
              options =>
              {
                  options.Filters.Add<HttpGlobalExceptionFilter>();//全局注册

              });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;//这里要改为false，默认是true，true的时候session无效
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.AddScheme<MyAuthenticationHandler>("MyScheme", "NetCore认证"); //设计自己的认证方式
            })
            .AddCookie(options =>
            {
                //Cookie的配置项
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = "/home/AccessDenied";
                options.Cookie.Name = "erpCookie";
                //options.SessionStore = new MemoryCacheTicketStore(); //内存存储方案 
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
            });
            //MemoryCache全局设置-应用缓存策略
            services.AddMemoryCache(options =>
            {
                //最大缓存空间大小限制为 1024
                options.SizeLimit = 1024;
                //缓存策略设置为缓存压缩比为 2%
                options.CompactionPercentage = 0.02d;
                //每 5 分钟进行一次过期缓存的扫描
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
            });
            //AddScoped 每次请求获得一个新实例，同一请求多次会得到相同的实例
            //AddTransient：瞬时模式，每次请求获取新的实例
            //AddSingleton:单例模式，每次都获取同一个实例
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(MemoryCacheExtensions));
            services.AddDbContext<NetCoreFrameDBContext>(opt => opt.UseMySql(Configuration.GetConnectionString("CoreFrameContext")));
                  
            services.AddSignalR(huboptions =>
            {
                //显示服务器错误的详细信息
                huboptions.EnableDetailedErrors = true; 
                //间隔时长 1分钟
                huboptions.KeepAliveInterval = TimeSpan.FromSeconds(10);
            });
            services.AddControllersWithViews();

            services.AddSingleton<MinioClient, MinioClient>(sp =>
            {
                
                string endpoint = Configuration.GetValue<string>("MinIoSettings:endpoint");
                string accessKey = Configuration.GetValue<string>("MinIoSettings:accessKey");
                string secretKey = Configuration.GetValue<string>("MinIoSettings:secretKey");
               
                return new MinioClient(endpoint, accessKey, secretKey);
            });
            //使用AutoFac进行注入
            // return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //AutoFac注入
            builder.RegisterModule(new AutofacModuleRegister());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //中间件 把请求交给MVC
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env, 
            ILoggerFactory loggerFactory,
            IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            loggerFactory.AddLog4Net();
             app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//监测有没有登陆
            app.UseAuthorization(); //授权 赋予权限

            //Session
            app.UseSession();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapHub<SignalrHubs>("/hub");
            });

         
        }
        


        
    }
}
