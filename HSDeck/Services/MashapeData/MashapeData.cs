using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDeck.Services.CardAPI
{
    public class MashapeData
    {
        private const string _baseUrl = "https://omgvamp-hearthstone-v1.p.mashape.com";
        public string BASE_URL { get { return _baseUrl; } }

        private const string _mashapeKey = "YOUR_KEY_HERE";
        public string MASHAPE_KEY { get { return _mashapeKey; } }
    }
}
