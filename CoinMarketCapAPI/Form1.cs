using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Timers;
using System.Collections.Generic;

namespace CoinMarketCapAPI
{
    public partial class Form1 : Form
    {
        private CoinMarketCap.CallAPI srv = new CoinMarketCap.CallAPI();
        private System.Timers.Timer _timer;
        private System.Timers.Timer _timer5m;
        private List<KeyValuePair<int, string>> _symbols;
        private int _symbolIndex = 0;
        public Form1()
        {
            InitializeComponent();
            int ApiNumber = (new Random()).Next(0, 2);

            //_timer = new System.Timers.Timer(60000);
            //_timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            //_timer.Start();
            //OnTimerElapsed(null, EventArgs.Empty);

            _symbols = srv.GetSymbols();

            _timer5m = new System.Timers.Timer(1000);
            _timer5m.Elapsed += new ElapsedEventHandler(OnTimer5mElapsed);
            _timer5m.Start();
            //srv.AddCoinMarketCapData();
            OnTimer5mElapsed(null, EventArgs.Empty);


        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            try
            {
                srv.AddCoinMarketCapData(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                CoinMarketCap.Common.FilePath = @"C:\Log\ErrorLogCoinMarket.txt";
                CoinMarketCap.Common.WriteToFile(CoinMarketCap.Common.ExceptionToString(ex));
            }
        }

        private void OnTimer5mElapsed(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.UtcNow.Minute % 5 == 0 && DateTime.UtcNow.Second == 0)
                    srv.AddCoinMarketCapData(DateTime.UtcNow);
                //if (_symbolIndex >= _symbols.Count - 1)
                //    _symbolIndex = 0;
                //if ((_symbolIndex == 0 && DateTime.UtcNow.Minute == 0) || _symbolIndex > 0)
                //{
                //    srv.AddIndicatorData(_symbols[_symbolIndex].Key, _symbols[_symbolIndex].Value);
                //    _symbolIndex++;
                //}
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (400) Bad Request.")
                    _symbolIndex++;
                //CoinMarketCap.Common.FilePath = @"C:\Log\ErrorLogCoinMarket.txt";
                //CoinMarketCap.Common.WriteToFile(CoinMarketCap.Common.ExceptionToString(ex));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
