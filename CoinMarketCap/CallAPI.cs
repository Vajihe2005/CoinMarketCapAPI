using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace CoinMarketCap
{
    public class CallAPI
    {
        private SQLDataLayer sqlDal = new SQLDataLayer();
        /// <summary>
        /// گرفتن اطلاعات و ثبت آن
        /// </summary>
        /// <param name="timeStamp">تاریخ دریافت اطلاعات</param>
        public void AddCoinMarketCapData(DateTime timeStamp)
        {
            var result = makeCoinMarketCapAPICall();
            var resConvert = JsonConvert.DeserializeObject<Root>(result.ToString());
            if (resConvert != null && resConvert.data != null && resConvert.status != null)
            {
                foreach (var item in resConvert.data)
                {
                    var sw = sqlDal.AddMarketData(item, timeStamp);
                    //var sw = sqlDal.AddMarketData(item, resConvert.status.timestamp);
                }
            }
        }
        
        private static string[] CoinMarketCap_API_KEY = {   "a4cfe151-ffe6-41f2-8300-14d12f3193ae",
                                                            "5f18deab-d706-4026-9e3a-7f2ac1bfc784",
                                                            "18b96b07-e787-4cdb-9733-a7abcbc4b05f",
                                                            "81cd896a-6a24-441f-8441-0b14339cf68c",
                                                            "1192bda2-29f0-41de-85b1-55d36fa9ea18",
                                                            "6c73f442-e1fd-46e7-8154-6701a8939599",
                                                            "06772114-5b15-4755-8040-cf4550bbba2b",
                                                            "76a9b636-8c36-4a91-8a76-b3bbae0d4012",
                                                            "18cbd265-7d75-43fd-8991-354bfe1b90a0",
                                                            "12d599ad-9ded-4030-b4fb-529d9da477a1",
                                                            "1e3fb0ae-59ee-4a88-9f51-61bce83ca323",
                                                            "972656ea-044f-4f7c-8bf0-a42221dcfa81",
                                                            "b786e82e-d700-4722-a099-3600e77befb6",
                                                            "f7505c52-3d9f-4ce5-8416-6fb5328651c7",
                                                            "6986f701-31a2-4733-8644-54e9bb22b642",
                                                            "ca4b313c-05ea-4bdb-9f4e-d0a149d4ef39",
                                                            "313ac3d4-f8cd-422a-809d-95d307b932e3",
                                                            "60f12853-e052-41d3-8214-c584452304ee",
                                                            "efb75c93-3f00-4d83-8780-913a11e94812",
                                                            "d0cdfd42-00d8-4f6a-8b7a-5fbc368c41fd",
                                                            "623b7c80-079c-4420-8e60-234a56080662",
                                                            "8f9a33c2-e648-4e61-ad37-6ee55d2c1767",
                                                            "4a3f7627-9c98-402f-823f-056b38beea26"
                                                            };
        private static int ApiNumber = (new Random()).Next(0, CoinMarketCap_API_KEY.Length);
        static string makeCoinMarketCapAPICall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            if (ApiNumber == CoinMarketCap_API_KEY.Length - 1) ApiNumber = 0;
            else ApiNumber++;

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", CoinMarketCap_API_KEY[ApiNumber]);
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(URL.ToString());

        }

        static string makeCoinMarketCapHistoricalAPICall(DateTime d)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/historical");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["date"] = d.ToString("yyyy-MM-dd");
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", CoinMarketCap_API_KEY[0]);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());

        }

        #region Indicators

        private static string EMA_API_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InZhamloZS5qYW1pQGdtYWlsLmNvbSIsImlhdCI6MTY0ODYxODI3NSwiZXhwIjo3OTU1ODE4Mjc1fQ.Ct0w7R5Apyt97RRpd22Difs3OohNjCIOxYw6TRTBNFc";
        static string makeEMAAPICall(string symbol)
        {
            //var URL = new UriBuilder("https://api.taapi.io/ema?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InZhamloZS5qYW1pQGdtYWlsLmNvbSIsImlhdCI6MTY0ODYxODI3NSwiZXhwIjo3OTU1ODE4Mjc1fQ.Ct0w7R5Apyt97RRpd22Difs3OohNjCIOxYw6TRTBNFc&exchange=binance&symbol="+ symbol + "/USDT&interval=1d&optInTimePeriod=200");

            //var queryString = HttpUtility.ParseQueryString(string.Empty);
            //queryString["secret"] = EMA_API_KEY;
            //queryString["exchange"] = "binance";
            //queryString["symbol"] = symbol + "/USDT";
            //queryString["interval"] = "1d";
            //queryString["optInTimePeriod"] = "200";

            //URL.Query = queryString.ToString();

            //var client = new WebClient();
            //client.Headers.Add("X-CMC_PRO_API_KEY", EMA_API_KEY);
            //client.Headers.Add("Accepts", "application/json");

            //return client.DownloadString(URL.ToString());

            var url = "https://api.taapi.io/ema?secret=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InZhamloZS5qYW1pQGdtYWlsLmNvbSIsImlhdCI6MTY0ODYxODI3NSwiZXhwIjo3OTU1ODE4Mjc1fQ.Ct0w7R5Apyt97RRpd22Difs3OohNjCIOxYw6TRTBNFc&exchange=binance&symbol=" + symbol + "/USDT&interval=1d&optInTimePeriod=200";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            System.IO.Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);
            return objReader.ReadLine();
        }

        public void AddIndicatorData(int symbolId, string symbol)
        {

            var result = makeEMAAPICall(symbol);
            var indicatorData = Convert.ToDouble(result.Substring(result.IndexOf(":") + 1).Replace("}", ""));
            if (indicatorData != 0)
            {
                var sw = sqlDal.AddIndicatorData(symbolId, symbol, indicatorData, "EMA200", DateTime.Now);
            }
        }

        public List<KeyValuePair<int, string>> GetSymbols()
        {
            var symbols = sqlDal.GetSymbols();
            return symbols;
        }

        #endregion
    }
}
