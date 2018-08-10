using System;

namespace RareGameStore.Models
{
    public class GameOrderProduct
    {
        public Guid ID { get; set; }
        public GameOrder GameOrder { get; set; }
        public Guid GameOrderID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}