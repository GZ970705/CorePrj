using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Tools
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppConfigurtaionServices
    {
        //public static IConfiguration Configuration
        //{
        //    get
        //    {
        //        return _configuration;
        //    }
        //}
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载
            //var filename = GetAppsettiongsFileName();
            //var directory = AppContext.BaseDirectory;
            //directory = directory.Replace("\\","/");
            //var filePath = $"{directory}/{filename}";
            //if (!File.Exists(filePath))
            //{
            //    var length = directory.IndexOf("/bin");
            //    filePath = $"{directory.Substring(0,length)}/{filename}";
            //}
            //var builder = new ConfigurationBuilder().AddJsonFile(filePath,false,true);
            //_configuration = builder.Build();
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

        //获取配置
      

    //获取指定节点(二级以上),要定义实体类
    public static T GetAppsettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(Configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>().Value;
            return appconfig;
        }

        //获取指定节点(只有一级情况下调用)
        public static string GetSectionValue(string key)
        {

            return Configuration.GetSection(key).Value;
        }

        //获取配置文件名
        public static string GetAppsettiongsFileName()
        {

            if (IsDebug)
            {
                return "appsettings.Development.json";
            }
            else
            {
                return "appsettings.json";
            }
        }
        public static bool IsDebug {
            get {
                if (_isDebugMode == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        //由于调试的GetFrames的StackTrace实例没有跳过任何帧,所以GetFrames()一定不为null
                        assembly = new StackTrace().GetFrames().Last().GetMethod().Module.Assembly;
                    }
                    var debuggableAttribute = assembly.GetCustomAttribute<DebuggableAttribute>();
                    _isDebugMode = debuggableAttribute.DebuggingFlags.HasFlag(DebuggableAttribute.DebuggingModes.EnableEditAndContinue);
                }
                return _isDebugMode.Value;
            }
        }
        private static bool? _isDebugMode;
    }
}
