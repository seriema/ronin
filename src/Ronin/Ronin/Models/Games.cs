using System;
using System.Globalization;

namespace Ronin.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/games/{0}", this.GameId);
            }
            set { }
        }
    }
}