using System.Collections.Generic;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Finances;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public virtual RequestPay RequestPay { get; set; }
        public long RequestPayId { get; set; }

        public OrderState OrderState { get; set; }

        public string Address { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}