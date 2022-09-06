using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMarketCap
{
    public static class Common
    {
        public static string ExceptionToString(Exception ex, bool withNewLine = false)
        {
            if (ex == null) return "";
            if (ex.InnerException == null) return ex.Message;
            var inner = ex.InnerException;
            string result = ex.Message;
            while (inner != null)
            {
                result += (withNewLine ? "\r\n" : " ") + inner.Message;
                inner = inner.InnerException;
            }
            return result;
        }

        public static string FilePath = AppDomain.CurrentDomain.BaseDirectory + "LogFile.txt";

        public static void WriteToFile(string text)
        {
            try
            {
                System.IO.File.AppendAllText(FilePath, text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
