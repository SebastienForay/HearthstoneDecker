using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace HSDeck.Models
{
    public class Cardback
    {
        public string cardBackId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public string sourceDescription { get; set; }
        public bool enabled { get; set; }
        public string img { get; set; }
        public string imgAnimated { get; set; }
        public string sortCategory { get; set; }
        public string sortOrder { get; set; }
        public string locale { get; set; }

        public ImageSource Image { get; set; }
    }
}
