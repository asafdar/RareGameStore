using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RareGameStore.Models
{
    public class Game
    {
        public Game()
        {
            this.GameCartProducts = new HashSet<GameCartProduct>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public decimal? Price { get; set; }
        public string ImagePath { get; set; }
        public Platform Platform { get; set; }
        public string PlatformName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public ICollection<GameCartProduct> GameCartProducts { get; set; }
    }
}
