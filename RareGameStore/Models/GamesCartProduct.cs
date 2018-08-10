using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RareGameStore.Models
{
    public class GameCartProduct
    {
        public int ID { get; set; }
        public GameCart GameCart { get; set; }
        public int GameCartID { get; set; }
        public Game Game { get; set; }
        public int GameID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

    }
}
