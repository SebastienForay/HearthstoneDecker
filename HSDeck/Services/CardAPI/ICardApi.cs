using HSDeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDeck.Services.CardAPI
{
    public interface ICardApi
    {
        Task<CardResponse> GetAll();
        Task<Card> GetSingle(string cardName);
        Task<IEnumerable<Cardback>> GetAllBacks();
        Task<IEnumerable<Card>> GetAllByClass(string className);
    }
}
