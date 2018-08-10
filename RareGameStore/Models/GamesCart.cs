using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RareGameStore.Models
{
    public class GameCart
    {
        public GameCart()
        {
            //Not really necessary for this, but helpful for unit tests.
            this.GameCartProducts = new HashSet<GameCartProduct>();
        }

        public int ID { get; set; }
        public ICollection<GameCartProduct> GameCartProducts { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }
    }
}
