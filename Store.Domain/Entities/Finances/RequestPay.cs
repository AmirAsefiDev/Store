using System;
using System.Collections.Generic;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Finances
{
    public class RequestPay : BaseEntity
    {
        public Guid Guid { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public decimal Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

        public ICollection<Order> Orders { get; set; }
    }
}