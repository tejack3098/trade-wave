using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpclient;
        private IConfiguration _config;

        public FMPService(HttpClient httpclient, IConfiguration config)
        {
            _httpclient = httpclient;
            _config = config;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
           try{

                var result = await _httpclient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMP_KEY"]}");

                if(result.IsSuccessStatusCode){
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if(stock != null){
                        return stock.ToStockFromFMP();
                    }

                    return null;
                }
                return null;
           }
           catch (Exception ex)
           {
                Console.WriteLine(ex.Message);
                return null;
           }
        }

    }
}
