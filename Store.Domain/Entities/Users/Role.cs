using Store.Domain.Entities.Common;
using System.Collections.Generic;

namespace Store.Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
