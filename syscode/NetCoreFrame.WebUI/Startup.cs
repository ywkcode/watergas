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
            #region ע��Session 
            services.AddSession(options =>
                {
                    //����Session����ʱ��
                    options.IdleTimeout = TimeSpan.FromDays(10);
                    options.Cookie.HttpOnly = true;
                }
            );
            services.AddHttpContextAccessor();
            #endregion
            services.AddMvc(
              options =>
              {
                  options.Filters.Add<HttpGlobalExceptionFilter>();//ȫ��ע��

              });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;//����Ҫ��Ϊfalse��Ĭ����true��true��ʱ��session��Ч
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.AddScheme<MyAuthenticationHandler>("MyScheme", "NetCore��֤"); //����Լ�����֤��ʽ
            })
            .AddCookie(options =>
            {
                //Cookie��������
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = "/home/AccessDenied";
                options.Cookie.Name = "erpCookie";
                //options.SessionStore = new MemoryCacheTicketStore(); //�ڴ�洢���� 
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
            });
            //MemoryCacheȫ������-Ӧ�û������
            services.AddMemoryCache(options =>
            {
                //��󻺴�ռ��С����Ϊ 1024
                options.SizeLimit = 1024;
                //�����������Ϊ����ѹ����Ϊ 2%
                options.CompactionPercentage = 0.02d;
                //ÿ 5 ���ӽ���һ�ι��ڻ����ɨ��
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
            });
            //AddScoped ÿ��������һ����ʵ����ͬһ�����λ�õ���ͬ��ʵ��
            //AddTransient��˲ʱģʽ��ÿ�������ȡ�µ�ʵ��
            //AddSingleton:����ģʽ��ÿ�ζ���ȡͬһ��ʵ��
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(MemoryCacheExtensions));
            services.AddDbContext<NetCoreFrameDBContext>(opt => opt.UseMySql(Configuration.GetConnectionString("CoreFrameContext")));
                  
            services.AddSignalR(huboptions =>
            {
                //��ʾ�������������ϸ��Ϣ
                huboptions.EnableDetailedErrors = true; 
                //���ʱ�� 1����
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
            //ʹ��AutoFac����ע��
            // return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //AutoFacע��
            builder.RegisterModule(new AutofacModuleRegister());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //�м�� �����󽻸�MVC
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

            app.UseAuthentication();//�����û�е�½
            app.UseAuthorization(); //��Ȩ ����Ȩ��

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
