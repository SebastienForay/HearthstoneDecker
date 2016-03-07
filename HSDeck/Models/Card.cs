using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace HSDeck.Models
{
    public class Card
    {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string text { get; set; }
        public string locale { get; set; }
        public int health { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public Mechanic11[] mechanics { get; set; }
        public string playerClass { get; set; }
        public int attack { get; set; }
        public string race { get; set; }
        public string rarity { get; set; }
        public string artist { get; set; }
        public int cost { get; set; }
        public string flavor { get; set; }
        public bool collectible { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public int durability { get; set; }
        public bool elite { get; set; }

        public ImageSource Image { get; set; }
        public string counter { get; set; } // == "" si la carte n'est pas en double dans le deck, == "2" si elle est en double dans le deck
        public string imgCardEmpty = "https://www.dropbox.com/s/zmrid731a7rm0kc/blank_card.png?dl=0";
    }

    public class CardResponse
    {
        public Basic[] Basic { get; set; }
        public Classic[] Classic { get; set; }
        public Credit[] Credits { get; set; }
        public Naxxramas[] Naxxramas { get; set; }
        public Debug[] Debug { get; set; }
        public GoblinsVsGnome[] GoblinsvsGnomes { get; set; }
        public Mission[] Missions { get; set; }
        public Promotion[] Promotion { get; set; }
        public Reward[] Reward { get; set; }
        public System[] System { get; set; }
        public BlackrockMountain[] BlackrockMountain { get; set; }
        public HeroSkin[] HeroSkins { get; set; }
        public TavernBrawl[] TavernBrawl { get; set; }
        public TheGrandTournament[] TheGrandTournament { get; set; }
        public TheLeagueOfExplorer[] TheLeagueofExplorers { get; set; }
    }

    public class Basic : Card { }
    public class Classic : Card { }
    public class Naxxramas : Card { }
    public class Credit : Card { }
    public class Debug : Card { }
    public class GoblinsVsGnome : Card { }
    public class Mission : Card { }
    public class Promotion : Card { }
    public class Reward : Card { }
    public class System : Card { }
    public class BlackrockMountain : Card { }
    public class HeroSkin : Card { }
    public class TavernBrawl : Card { }
    public class TheGrandTournament : Card { }
    public class TheLeagueOfExplorer : Card { }

    public class Mechanic
    {
        public string Name;
    }
    public class Mechanic1
    {
        public string name { get; set; }
    }
    public class Mechanic2
    {
        public string name { get; set; }
    }
    public class Mechanic3
    {
        public string name { get; set; }
    }
    public class Mechanic4
    {
        public string name { get; set; }
    }
    public class Mechanic5
    {
        public string name { get; set; }
    }
    public class Mechanic6
    {
        public string name { get; set; }
    }
    public class Mechanic7
    {
        public string name { get; set; }
    }
    public class Mechanic8
    {
        public string name { get; set; }
    }
    public class Mechanic9
    {
        public string name { get; set; }
    }
    public class Mechanic10
    {
        public string name { get; set; }
    }
    public class Mechanic11
    {
        public string name { get; set; }
    }
}