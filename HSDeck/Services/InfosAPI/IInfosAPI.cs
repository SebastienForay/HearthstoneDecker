using HSDeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDeck.Services.InfosAPI
{
    public interface IInfosAPI
    {
        Task<Infos> GetAllInfos();
    }
}
