using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Stock;
using StockApi.Models.Defines;

namespace StockApi.Services;

/// <summary>
/// 提供對股市相關資訊的gRPC服務存取。
/// </summary>
public class GrpcService
{
    private readonly ILogger<GrpcService> _logger;
    private readonly IHostEnvironment _env;
    private readonly GrpcOptions _grpcOptions;
    private readonly Lazy<Stock.Stock.StockClient> _clientLazy;

    /// <summary>
    /// 初始化GrpcService實例。
    /// </summary>
    /// <param name="logger">用於記錄日誌的logger。</param>
    /// <param name="go">包含gRPC配置選項的實例。</param>
    /// <param name="env">提供關於當前主機環境的資訊。</param>
    /// <exception cref="ArgumentNullException">如果任一參數為null，則拋出異常。</exception>
    public GrpcService(ILogger<GrpcService> logger, IOptions<GrpcOptions> go, IHostEnvironment env)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _grpcOptions = go?.Value ?? throw new ArgumentNullException(nameof(go));
        _env = env ?? throw new ArgumentNullException(nameof(env));

        _clientLazy = new Lazy<Stock.Stock.StockClient>(CreateGrpcClient, true);
    }

    /// <summary>
    /// 建立與gRPC服務的連接並返回StockClient實例。
    /// </summary>
    /// <returns>初始化完成的Stock.Stock.StockClient實例。</returns>
    private Stock.Stock.StockClient CreateGrpcClient()
    {
        try
        {
            var httpHandler = new HttpClientHandler();
            // 在開發環境中，忽略SSL憑證驗證錯誤
            if (_env.IsDevelopment())
            {
                httpHandler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            }

            var channel =
                GrpcChannel.ForAddress(
                    $"{_grpcOptions.HttpProtocol}{_grpcOptions.RustGrpcTarget}:{_grpcOptions.RustGrpcTargetPort}",
                    new GrpcChannelOptions { HttpHandler = httpHandler });
            return new Stock.Stock.StockClient(channel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize gRPC client: {Message}", ex.Message);
            throw; 
        }
    }

    /// <summary>
    /// 獲取gRPC客戶端實例。
    /// </summary>
    private Stock.Stock.StockClient Client => _clientLazy.Value;

    /// <summary>
    /// 異步獲取指定年份的假期安排。
    /// </summary>
    /// <param name="year">要查詢的年份。</param>
    /// <returns>包含休市日的集合。</returns>
    internal async Task<IEnumerable<HolidaySchedule>> FetchHolidayScheduleAsync(int year)
    {
        var reply = await Client
            .FetchHolidayScheduleAsync(new HolidayScheduleRequest { Year = year })
            .ConfigureAwait(false);
        return reply.Holiday;
    }
}