using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.AddNewProduct
{
    internal interface IAddNewProductService
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }

    public class RequestAddNewProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; } 
        public bool Displayed { get; set; }
        //public List<IFromFile> Images { get; set; }
        public List<AddNewProduct_Features> Features { get; set; }
    }
    public class AddNewProduct_Features
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
