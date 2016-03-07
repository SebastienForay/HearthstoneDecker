using Flurl;
using Flurl.Http;
using HSDeck.Models;
using HSDeck.Services.CardAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDeck.Services.InfosAPI
{
    public class InfosAPI : IInfosAPI
    {
        private MashapeData API = new MashapeData();

        public async Task<Infos> GetAllInfos()
        {
            var tmpUrl = API.BASE_URL + "/info";
            return await tmpUrl
                .WithHeader("X-Mashape-Key", API.MASHAPE_KEY)
                .GetJsonAsync<Infos>();
        }
    }
}
