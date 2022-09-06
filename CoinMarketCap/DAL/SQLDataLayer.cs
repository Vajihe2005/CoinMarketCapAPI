using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMarketCap
{
    public class SQLDataLayer
    {
        private SqlConnection mssqlDb = new SqlConnection(Configuration.SQLConnectionString);

        public bool AddMarketData(Datum data, DateTime timestamp)
        {
            if (data == null || data.quote == null) return false;
            OpenConnection();
            var trans = mssqlDb.BeginTransaction();
            try
            {
                var q = "INSERT INTO [CoinMarketData](Status_timestamp,Datum_id";
                if (!string.IsNullOrEmpty(data.name))
                    q += ",Datum_name";
                if (!string.IsNullOrEmpty(data.symbol))
                    q += ",Datum_symbol";
                if (!string.IsNullOrEmpty(data.slug))
                    q += ",Datum_slug";
                q += ",Datum_num_market_pairs";
                if (data.date_added != null)
                    q += ",Datum_date_added";
                if (data.max_supply != null)
                    q += ",Datum_max_supply";
                q += ",Datum_circulating_supply";
                q += ",Datum_total_supply";
                q += ",Datum_cmc_rank";
                if (data.last_updated != null)
                    q += ",Datum_last_updated";
                if (data.quote != null && data.quote.USD != null)
                {
                    q += ",Quote_price";
                    q += ",Quote_volume_24h";
                    q += ",Quote_volume_change_24h";
                    q += ",Quote_percent_change_1h";
                    q += ",Quote_percent_change_24h";
                    q += ",Quote_percent_change_7d";
                    q += ",Quote_percent_change_30d";
                    q += ",Quote_percent_change_60d";
                    q += ",Quote_percent_change_90d";
                    q += ",Quote_market_cap";
                    q += ",Quote_market_cap_dominance";
                    q += ",Quote_fully_diluted_market_cap";
                    if (data.quote.USD.last_updated != null)
                        q += ",Quote_last_updated";
                }
                q += ") VALUES(@Status_timestamp,@Datum_id";
                if (!string.IsNullOrEmpty(data.name))
                    q += ",@Datum_name";
                if (!string.IsNullOrEmpty(data.symbol))
                    q += ",@Datum_symbol";
                if (!string.IsNullOrEmpty(data.slug))
                    q += ",@Datum_slug";
                q += ",@Datum_num_market_pairs";
                if (data.date_added != null)
                    q += ",@Datum_date_added";
                if (data.max_supply != null)
                    q += ",@Datum_max_supply";
                q += ",@Datum_circulating_supply";
                q += ",@Datum_total_supply";
                q += ",@Datum_cmc_rank";
                if (data.last_updated != null)
                    q += ",@Datum_last_updated";
                if (data.quote != null && data.quote.USD != null)
                {
                    q += ",@Quote_price";
                    q += ",@Quote_volume_24h";
                    q += ",@Quote_volume_change_24h";
                    q += ",@Quote_percent_change_1h";
                    q += ",@Quote_percent_change_24h";
                    q += ",@Quote_percent_change_7d";
                    q += ",@Quote_percent_change_30d";
                    q += ",@Quote_percent_change_60d";
                    q += ",@Quote_percent_change_90d";
                    q += ",@Quote_market_cap";
                    q += ",@Quote_market_cap_dominance";
                    q += ",@Quote_fully_diluted_market_cap";
                    if (data.quote.USD.last_updated != null)
                        q += ",@Quote_last_updated";
                }
                q += ")";
                var p = new List<SqlParameter>();
                p.Add(new SqlParameter
                {
                    ParameterName = "@Status_timestamp",
                    Value = timestamp,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.DateTime
                });
                p.Add(new SqlParameter
                {
                    ParameterName = "@Datum_id",
                    Value = data.id,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32
                });
                if (!string.IsNullOrEmpty(data.name))
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_name",
                        Value = data.name,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    });
                }
                if (!string.IsNullOrEmpty(data.symbol))
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_symbol",
                        Value = data.symbol,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    });
                }
                if (!string.IsNullOrEmpty(data.slug))
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_slug",
                        Value = data.slug,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    });
                }
                p.Add(new SqlParameter
                {
                    ParameterName = "@Datum_num_market_pairs",
                    Value = data.num_market_pairs,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32
                });
                if (data.date_added != null)
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_date_added",
                        Value = data.date_added,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.DateTime
                    });
                }
                if (data.max_supply != null)
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_max_supply",
                        Value = data.max_supply,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Int64
                    });
                }
                p.Add(new SqlParameter
                {
                    ParameterName = "@Datum_circulating_supply",
                    Value = data.circulating_supply,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Double
                });
                p.Add(new SqlParameter
                {
                    ParameterName = "@Datum_total_supply",
                    Value = data.total_supply,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Double
                });
                p.Add(new SqlParameter
                {
                    ParameterName = "@Datum_cmc_rank",
                    Value = data.cmc_rank,
                    Direction = ParameterDirection.Input,
                    DbType = DbType.Int32
                });
                if (data.last_updated != null)
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Datum_last_updated",
                        Value = data.last_updated,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.DateTime
                    });
                }
                if (data.quote != null && data.quote.USD != null)
                {
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_price",
                        Value = data.quote.USD.price,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_volume_24h",
                        Value = data.quote.USD.volume_24h,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_volume_change_24h",
                        Value = data.quote.USD.volume_change_24h,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_1h",
                        Value = data.quote.USD.percent_change_1h,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_24h",
                        Value = data.quote.USD.percent_change_24h,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_7d",
                        Value = data.quote.USD.percent_change_7d,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_30d",
                        Value = data.quote.USD.percent_change_30d,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_60d",
                        Value = data.quote.USD.percent_change_60d,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_percent_change_90d",
                        Value = data.quote.USD.percent_change_90d,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_market_cap",
                        Value = data.quote.USD.market_cap,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_market_cap_dominance",
                        Value = data.quote.USD.market_cap_dominance,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    }); ;
                    p.Add(new SqlParameter
                    {
                        ParameterName = "@Quote_fully_diluted_market_cap",
                        Value = data.quote.USD.fully_diluted_market_cap,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Double
                    });
                    if (data.quote.USD.last_updated != null)
                    {
                        p.Add(new SqlParameter
                        {
                            ParameterName = "@Quote_last_updated",
                            Value = data.quote.USD.last_updated,
                            Direction = ParameterDirection.Input,
                            DbType = DbType.DateTime
                        });
                    }
                }
                var result = ExecuteNonQuery(q, trans, p.ToArray());
                if (result < 0)
                    throw new Exception("No data inserted for symbol:" + data.symbol + " at date:" + timestamp.ToString("yyyy/MM/dd"));
                trans.Commit();
                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                CloseConnection();
                throw ex;
            }

        }

        internal int AddIndicatorData(int symbolId, string symbol, double indicatorData, string indicatorType, DateTime dateStamp)
        {
            string q = "INSERT INTO Indicators(SymbolID,Symbol,IndicatorData,IndicatorType,DateStamp) VALUES(" +
                symbolId + ",'" + symbol + "'," + indicatorData + ",'" + indicatorType + "','" + dateStamp + "')";
            return ExecuteNonQuery(q);
        }

        internal List<KeyValuePair<int, string>> GetSymbols()
        {
            //var q = "Select Datum_ID,max(Datum_symbol) Datum_symbol from CoinMarketData  group by Datum_ID order by Datum_ID";
            var q = "select distinct symbol,symbolid from indicators";
            DataTable dt = ExecuteDataTable(q);
            var dic = new List<KeyValuePair<int, string>>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dic.Add(new KeyValuePair<int, string>(Convert.ToInt32(item["symbolid"]), item["symbol"].ToString()));
                }
            }
            //List<string> symbols = dt.AsEnumerable().Select(row => row.Field<string>(0)).ToList();
            return dic;
        }

        private void OpenConnection()
        {
            if (mssqlDb.State != ConnectionState.Open)
                mssqlDb.Open();
        }

        private void CloseConnection()
        {
            if (mssqlDb.State != ConnectionState.Closed)
                mssqlDb.Close();
        }

        private int ExecuteNonQuery(string q, params SqlParameter[] p)
        {
            try
            {
                OpenConnection();
                var sqlCommand = new SqlCommand(q, mssqlDb);
                foreach (SqlParameter param in p)
                    sqlCommand.Parameters.Add(param);
                int result = sqlCommand.ExecuteNonQuery();
                CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int ExecuteNonQuery(string q, SqlTransaction trans, params SqlParameter[] p)
        {
            try
            {
                var sqlCommand = new SqlCommand(q, mssqlDb, trans);
                foreach (SqlParameter param in p)
                    sqlCommand.Parameters.Add(param);
                int result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ExecuteDataTable(string q, params SqlParameter[] p)
        {
            try
            {
                var sqlCommand = new SqlCommand(q, mssqlDb);
                foreach (SqlParameter param in p)
                    sqlCommand.Parameters.Add(param);
                var sqlDataAdaptor = new SqlDataAdapter(sqlCommand);
                var dtResult = new DataTable();
                sqlDataAdaptor.Fill(dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable ExecuteDataTable(string q)
        {
            try
            {
                var sqlCommand = new SqlCommand(q, mssqlDb);
                var sqlDataAdaptor = new SqlDataAdapter(sqlCommand);
                var dtResult = new DataTable();
                sqlDataAdaptor.Fill(dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    static class Configuration
    {
        static public string SQLConnectionString { get; set; } = "Data Source=localhost;Initial Catalog=CoinMarketCap;User ID=CoinMarketCapUser;Password=r#GHder*2";
    }
}
