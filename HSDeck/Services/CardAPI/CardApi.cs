using Flurl;
using Flurl.Http;
using HSDeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDeck.Services.CardAPI
{
    public class CardApi : ICardApi
    {
        private MashapeData API = new MashapeData();

        public async Task<CardResponse> GetAll()
        {
            var tmpUrl = API.BASE_URL + "/cards?locale=frFR";
            return await tmpUrl
                .WithHeader("X-Mashape-Key", API.MASHAPE_KEY)
                .GetJsonAsync<CardResponse>();
        }

        public async Task<Card> GetSingle(string cardName)
        {
            var tmpUrl = API.BASE_URL + "/cards/" + cardName + "?locale=frFR";
            var cards = await tmpUrl
                .WithHeader("X-Mashape-Key", API.MASHAPE_KEY)
                .GetJsonAsync<IEnumerable<Card>>();

            return cards.First();
        }

        public async Task<IEnumerable<Cardback>> GetAllBacks()
        {
            var tmpUrl = API.BASE_URL + "/cardbacks?locale=frFR";
            return await tmpUrl
                    .WithHeader("X-Mashape-Key", API.MASHAPE_KEY)
                    .GetJsonAsync<IEnumerable<Cardback>>();
        }
        
        public async Task<IEnumerable<Card>> GetAllByClass(string className)
        {
            var tmpUrl = API.BASE_URL + "/cards/classes/" + className + "?locale=frFR";
            var cards =  await tmpUrl
                    .WithHeader("X-Mashape-Key", API.MASHAPE_KEY)
                    .GetJsonAsync<IEnumerable<Card>>();

            return cards;
        }
    }
}
