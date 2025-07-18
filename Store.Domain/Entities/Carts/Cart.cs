using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
        public User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool Finished { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }

    public class CartItem : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("CartId")] public Cart Cart { get; set; }
        public long CartId { get; set; }
    }
}