using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RareGameStore.Models
{
    public class Platform
    {

        public Platform()
        {
            //If you create a new Platform object, this ensures that the newly created category doesn't have a "NULL" value for products.
            this.Games = new HashSet<Game>();

            //e.g. 
            //Platform bc = new Platform();
            //bc.Games = new List<Game>();
            //bc.Games.Add(...)
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
