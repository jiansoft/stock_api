using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using StockApi.Middlewares;
using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions.Services;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    /*option.SwaggerDoc("v1", new OpenApiInfo

    {

        Version = "v1",

        Title = "test api",

    });*/
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
    
});

builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachment"));
    
builder.Services.AddSingleton<CacheDataProvider>();
builder.Services.AddSingleton<StocksDataProvider>();
    
builder.Services.AddSingleton<StockService>();
builder.Services.AddSingleton<TwseService>();
    
builder.Services.AddMemoryCache();
    
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
    
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

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
    
logger.Debug($"{app.Environment.EnvironmentName}");
    
await app.RunAsync();
