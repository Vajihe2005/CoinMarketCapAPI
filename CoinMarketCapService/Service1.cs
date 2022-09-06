using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoinMarketCapService
{
    public partial class Service1 : ServiceBase
    {
        private CoinMarketCap.CallAPI srv = new CoinMarketCap.CallAPI();
        private Timer _timer5m;
        private Timer _timer15s;
        private List<KeyValuePair<int, string>> _symbols;
        private int _symbolIndex = 0;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer5m = new Timer(1000);
            _timer5m.Elapsed += new ElapsedEventHandler(OnTimer5mElapsed);
            _timer5m.Start();
            //srv.AddCoinMarketCapData();
            OnTimer5mElapsed(null, EventArgs.Empty);

            //_symbols = srv.GetSymbols();

            //_timer15s = new Timer(4000);
            //_timer15s.Elapsed += new ElapsedEventHandler(OnTimer15sElapsed);
            //_timer15s.Start();
            ////srv.AddCoinMarketCapData();
            //OnTimer15sElapsed(null, EventArgs.Empty);

        }

        protected override void OnStop()
        {
            _timer5m.Stop();
            _timer5m.Dispose();

            _timer15s.Stop();
            _timer15s.Dispose();
        }

        private void OnTimer5mElapsed(object sender, EventArgs e)
        {
            try
            {
                //if (DateTime.UtcNow.Hour % 2 == 0 && DateTime.UtcNow.Minute == 0)
                if (DateTime.UtcNow.Minute % 5 == 0 && DateTime.UtcNow.Second == 0)
                    srv.AddCoinMarketCapData(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                CoinMarketCap.Common.FilePath = @"C:\Log\ErrorLogCoinMarket.txt";
                CoinMarketCap.Common.WriteToFile(CoinMarketCap.Common.ExceptionToString(ex));
            }
        }

        private void OnTimer15sElapsed(object sender, EventArgs e)
        {
            try
            {
                if (_symbolIndex >= _symbols.Count - 1)
                    _symbolIndex = 0;
                srv.AddIndicatorData(_symbols[_symbolIndex].Key, _symbols[_symbolIndex].Value);
                _symbolIndex++;
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (400) Bad Request.")
                    _symbolIndex++;
                //CoinMarketCap.Common.FilePath = @"C:\Log\ErrorLogCoinMarket.txt";
                //CoinMarketCap.Common.WriteToFile(CoinMarketCap.Common.ExceptionToString(ex));
            }
        }
    }
}
