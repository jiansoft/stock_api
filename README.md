### stock_api
Service︰https://github.com/jiansoft/stock_crawler   
UI Demo︰https://jiansoft.mooo.com/stock/revenues  
API Demo︰https://jiansoft.freeddns.org/swagger  

### API清單
1. 股票基本資料 [/api/stock/details](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/Details/DetailDto.cs)
2. 股票產業分類 [/api/stock/industry](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/Industry/IndustryDto.cs)
3. 股利發放記錄 [/api/stock/dividend/{stockSymbol}](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/Dividend/DividendDto.cs)
4. 最後收盤數據 [/api/stock/last_daily_quote](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/DtoBase/DailyQuoteDtoBase.cs)
5. 歷史收盤數據 [/api/stock/historical_daily_quote/{date}](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/DtoBase/DailyQuoteDtoBase.cs)
6. 每月營收數據 [/api/stock/revenue_on/{monthOfYear}](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/Revenue/RevenueDto.cs)
7. 股票營收數據 [/api/stock/revenue_by/{stockSymbol}](https://github.com/jiansoft/stock_api/blob/main/StockApi/Models/HttpTransactions/Stock/Revenue/RevenueDto.cs)


### 免責聲明
本網提供之所有資訊內容均僅供參考，不涉及買賣投資之依據。使用者在進行投資決策時，務必自行審慎評估，
並自負投資風險及盈虧，如依本網提供之資料交易致生損失，本網不負擔任何賠償及法律責任。您自行負責依據
自身投資目標及個人、財務狀況，確定任何投資、證券或任何其他投資產品服務是否適合自身的需要。 
本網站所載或本網站上、通過本網站提供的任何服務、內容、資訊及/或資料在任何情況下均不得被解釋為提供投資、
法律意見或提供投資服務。特請訪問此類網頁的人士就有關任何本網資料是否適合其投資需求徵詢適當獨立專業意見。

