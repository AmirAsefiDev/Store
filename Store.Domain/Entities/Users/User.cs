using System.Collections.Generic;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Orders;

namespace Store.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}