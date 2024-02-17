using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockApi.Models.Defines;
using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 
/// </summary>
public class StockContext : DbContext
{
    private string Connection { get; set; }

    /// <inheritdoc />
    public StockContext(string connString)
    {
        Connection = connString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="dbOption"></param>
    public StockContext(DbContextOptions<StockContext> options, IOptions<DbOptions> dbOption) : base(options)
    {
        Connection = dbOption.Value.Connection;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(Connection)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            }));
    }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<StockExchangeMarketEntity> ExchangeMarkets { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<StockIndustryEntity> Industries { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<IndexEntity> Indexes { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<StockEntity> Stocks { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IndexEntity>(entity =>
        {
            entity.ToTable("index");

            entity.HasKey(e => e.Serial).HasName("index_pkey");
            entity.Property(e => e.Serial).HasColumnName("serial").HasComment("流水號");
            entity.Property(e => e.Category).HasColumnName("category").HasComment("分類");
            entity.Property(e => e.Date).HasColumnName("date").HasComment("資料屬於那一天");
            entity.Property(e => e.TradingVolume).HasColumnName("trading_volume").HasComment("成交股數");
            entity.Property(e => e.Transaction).HasColumnName("transaction").HasComment("成交筆數");
            entity.Property(e => e.TradeValue).HasColumnName("trade_value").HasComment("成交金額");
            entity.Property(e => e.Change).HasColumnName("change").HasComment("漲跌點數");
            entity.Property(e => e.Index).HasColumnName("index").HasComment("指數");
            
            entity.HasIndex(s =>new {s.Date,s.Category}).HasDatabaseName("index-date_category-uidx");
        });
        
        modelBuilder.Entity<StockExchangeMarketEntity>(entity =>
        {
            entity.ToTable("stock_exchange_market");

            entity.HasKey(e => e.StockExchangeMarketId).HasName("stock_exchange_market_pkey");
            entity.Property(e => e.StockExchangeMarketId).HasColumnName("stock_exchange_market_id")
                .HasComment("交易所的市場編號");
            entity.Property(e => e.StockExchangeId).HasColumnName("stock_exchange_id")
                .HasComment("交易所的編號參考 stock_exchange");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(24)
                .HasComment("交易所的市場代碼 TAI:上市 TWO:上櫃 TWE:興櫃");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(24).HasComment("權值佔比");
            
            // 組態一對多關係
            entity.HasMany(e => e.Stocks) // StockExchangeMarketEntity 有多個 StockEntity
                .WithOne(s => s.ExchangeMarket) // 每個 StockEntity 屬於一個 StockExchangeMarketEntity
                .HasForeignKey(s => s.ExchangeMarketId) // 外部索引鍵設定
                .OnDelete(DeleteBehavior.ClientSetNull); // 刪除行為
          
        });

        modelBuilder.Entity<StockIndustryEntity>(entity =>
        {
            entity.ToTable("stock_industry");

            entity.HasKey(e => e.IndustryId).HasName("stock_industry_pkey");

            entity.Property(e => e.IndustryId).HasColumnName("stock_industry_id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(24);
            
            // 組態一對多關係
            entity.HasMany(e => e.Stocks) 
                .WithOne(s => s.Industry) 
                .HasForeignKey(s => s.IndustryId) // 外部索引鍵設定
                .OnDelete(DeleteBehavior.ClientSetNull); // 刪除行為
        });

        modelBuilder.Entity<StockEntity>(entity =>
        {
            entity.ToTable("stocks");

            entity.HasKey(e => e.StockSymbol).HasName("stocks_pkey");

            entity.Property(e => e.StockSymbol)
                .HasColumnName("stock_symbol")
                .HasMaxLength(24);
            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(255);
            entity.Property(e => e.ExchangeMarketId)
                .HasColumnName("stock_exchange_market_id")
                .HasComment("交易所的市場編號參考 stock_exchange_market");
            entity.Property(e => e.IndustryId)
                .HasColumnName("stock_industry_id")
                .HasComment("股票的產業分類編號 stock_industry");
            entity.Property(e => e.LastOneEps).HasColumnName("last_one_eps")
                .HasComment("近一季EPS");
            entity.Property(e => e.LastFourEps).HasColumnName("last_four_eps")
                .HasComment("近四季EPS");
            entity.Property(e => e.Weight).HasColumnName("weight")
                .HasComment("權值佔比");

            entity.HasOne(d => d.Industry).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            // .HasConstraintName("FK_QuotedPrice_Product_Product");
            entity.HasOne(d => d.ExchangeMarket).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ExchangeMarketId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasIndex(s => new { s.ExchangeMarketId, s.IndustryId })
                .HasDatabaseName("stocks-stock_exchange_market_id-stock_industry_id-idx");
            entity.HasAlternateKey(s =>s.IndustryId)
                .HasName("stocks-stock_industry_id-idx");
        });
    }
}