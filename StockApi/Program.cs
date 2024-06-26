using MapsterMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using StockApi.Middlewares;
using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.Defines;
using StockApi.Services;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1.0.1",
            Title = "股票資訊服務",
            Description = "本平臺提供歷年台股資訊服務，歡迎各位介接使用。",
            Contact = new OpenApiContact
            {
                Name = "jIAn",
                Email = "eddiea.chen@gmail.com",
                Url = new Uri("https://github.com/jiansoft/stock_api")
            }
        });
        
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        option.IncludeXmlComments(xmlPath);
        var xmlFileCaller = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
        var xmlPathCaller = Path.Combine(AppContext.BaseDirectory, xmlFileCaller);
        option.IncludeXmlComments(xmlPathCaller);

        option.AddEnumsWithValuesFixFilters(o =>
        {
            // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
            o.ApplySchemaFilter = true;
            // alias for replacing 'x-enumNames' in swagger document
            o.XEnumNamesAlias = "x-enum-varnames";
            // alias for replacing 'x-enumDescriptions' in swagger document
            o.XEnumDescriptionsAlias = "x-enum-descriptions";
            // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
            o.ApplyParameterFilter = true;
            // add document filter to fix enums displaying in swagger document
            o.ApplyDocumentFilter = true;
            // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
            o.IncludeDescriptions = true;
            // add remarks for descriptions from xml-comments
            o.IncludeXEnumRemarks = true;
            // get descriptions from DescriptionAttribute then from xml-comments
            o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
            // new line for enum values descriptions
            // o.NewLine = Environment.NewLine;
            o.NewLine = "\n";
            // get descriptions from xml-file comments on the specified path
            // should use "options.IncludeXmlComments(xmlFilePath);" before
            o.IncludeXmlCommentsFrom(xmlPath);
        });

        option.DescribeAllParametersInCamelCase();
        option.CustomSchemaIds(x => x.FullName);
    });

    builder.Services.Configure<GrpcOptions>(builder.Configuration.GetSection("GRPC"));
    builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("DatabaseContext:Stock"));

    builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
    builder.Services.AddDbContext<StockContext>(opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("StockContext"))
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactory.Create(configure =>
            {
                configure
                    .AddConsole()
                    .AddNLog()
                    .AddFilter(
                        DbLoggerCategory.Database.Command.Name,
                        Microsoft.Extensions.Logging.LogLevel.Information);
            }));
    });

    builder.Services.AddScoped<StockContext>();

    builder.Services.AddSingleton<CacheDataProvider>();
    builder.Services.AddSingleton<StocksDataProvider>();

    builder.Services.AddSingleton<GrpcService>();
    builder.Services.AddSingleton<StockService>();
    builder.Services.AddSingleton<TwseService>();

    builder.Services.AddMemoryCache();

    builder.Services.AddSingleton(MapsterConfig.GetConfig());
    builder.Services.AddSingleton<IMapper, ServiceMapper>();
    
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy",
            policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
    });

    var app = builder.Build();

    //app.UseExceptionHandler();
    app.UseMiddleware<ExceptionMiddleware>();
    
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    /*app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");*/
    app.MapControllers();

    app.UseCors("CorsPolicy");

    logger.Debug($"Environment:{app.Environment.EnvironmentName}");
   
    await app.RunAsync();
}
catch (Exception exception)
{
    logger.Error(exception, exception.Message);
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}


      
       