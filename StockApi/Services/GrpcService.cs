using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Stock;
using StockApi.Models.Defines;


namespace StockApi.Services;

/// <summary>
/// 
/// </summary>
public class GrpcService
{
    private readonly Stock.Stock.StockClient? _client;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="logger"></param>
    /// <param name="env"></param>
    /// <param name="mapper">物件對應器，用於在不同的數據模型之間進行轉換。</param>
    public GrpcService(ILogger<GrpcService> logger, IOptions<GrpcOptions> go, IMapper mapper, IHostEnvironment env)
    {
        try
        {
            var httpHandler = new HttpClientHandler();

            if (env.IsDevelopment())
            {
                httpHandler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            }

            var channel =
                GrpcChannel.ForAddress(
                    $"{go.Value.HttpProtocol}{go.Value.RustGrpcTarget}:{go.Value.RustGrpcTargetPort}",
                    new GrpcChannelOptions { HttpHandler = httpHandler });
            _client = new Stock.Stock.StockClient(channel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize gRPC client: {Message}", ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public async Task<IEnumerable<HolidaySchedule>> FetchHolidayScheduleAsync(int year)
    {
        if (_client is null)
        {
            return Array.Empty<HolidaySchedule>();
        }

        var reply = await _client.FetchHolidayScheduleAsync(new HolidayScheduleRequest { Year = year });

        return reply.Holiday;
    }
}