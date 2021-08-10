using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PrjWebJWTandSwwage.JWT;
using PrjWebJWTandSwwage.TokenFile;
using Microsoft.EntityFrameworkCore;
using PrjWebJWTandSwwage.SqlTools;
using PrjWebJWTandSwwage.IDAL;
using PrjWebJWTandSwwage.DAL;
using PrjWebJWTandSwwage.IBLL;
using PrjWebJWTandSwwage.BLL;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.RabbitMQTools;
using PrjWebJWTandSwwage.InterfaceTools;
using PrjWebJWTandSwwage.Tools;
using ServiceStack.Redis;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using PrjWebJWTandSwwage.Log4net;

namespace PrjWebJWTandSwwage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log4NetConfig.Init(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //配置json返回数据类型
            services.AddControllers().AddNewtonsoftJson();
            //配置跨域问题
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });
            //注册swagger服务
            services.AddSwaggerGen(c=> {
                //添加注释文档信息
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "接口文档", Version = "v1.0" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #region JWT配置
            var jwtconfig = Configuration.GetSection("JWTConfig").Get<JWTConfig>();          
            //读取配置文件配置的jwt相关配置
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));
            services.AddAuthentication(Options => {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerEvents()
                {
                   
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Query["Token"];
                        return Task.CompletedTask;
                    },
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();
                        //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                        var payload = JsonConvert.SerializeObject(new { message = "授权未通过，Token无效", status = false, code = 401 });
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //输出Json数据结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,

                    ValidIssuer = jwtconfig.Issuer,
                    ValidAudience = jwtconfig.Audience,
                    // 缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfig.IssuerSigningKey)),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                    ClockSkew = TimeSpan.FromSeconds(5)

                };
            });
            services.AddOptions().Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));
            services.AddMvc(option =>
            {
                option.Filters.Add<Log4NetHelper>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion
            //配置token特性
            services.AddScoped<TokenFilter>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddMvc();
            //配置RabbitMQ服务
            //services.AddHostedService<RabbitListener>();
            //services.AddSingleton<MessageRepository, MessageRepository>();

            //注册Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            services.Configure<RedisConfig>(Configuration.GetSection("RedisCaching"));
            //配置数据交互Bll
            services.AddSingleton<IUserModelBLL, UserModelBLL>();
            services.AddSingleton<IUserBLL, UserBLL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("test"))
                {
                    throw new Exception("中间件异常测试");
                }
                await next();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //配置swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;

                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My SwaggerAPI");
               
            });

            app.UseRouting();
            //启用认证中间件 要写在授权UseAuthorization()的前面
            app.UseAuthentication();

            app.UseAuthorization();
            //配置跨域
            app.UseCors("cors");
            
            app.UseEndpoints(endpoints =>
            {   //跨域需添加RequireCors方法，cors是在ConfigureServices方法中配置的跨域策略名称
                endpoints.MapControllers().RequireCors("cors");
            });
            
        }
    }

}
