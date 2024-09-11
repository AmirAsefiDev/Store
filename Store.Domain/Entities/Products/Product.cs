using Store.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }


        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        public virtual List<ProductImage> ProductImage { get; set; }
        public virtual List<ProductFeatures> ProductFeatures { get; set; }
    }

    public class ProductImage : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public string Src { get; set; }
    }

    public class ProductFeatures : BaseEntity
    {
       public virtual Product Product { get; set; }
       public long ProductId { get; set; }
       public string DisplayName { get; set; } 
       public string Value { get; set; }
            
    }
}
