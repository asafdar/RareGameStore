using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RareGameStore.Models
{
    public class GameOrder
    {
        public GameOrder()
        {
            this.GameOrderProducts = new HashSet<GameOrderProduct>();
        }
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public ICollection<GameOrderProduct> GameOrderProducts { get; set; }
    }
}
