using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models
{
    public class Product
    {
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public Byte[] ProductPhoto { get; set; }
        public string ProductManufacturer { get; set; }
        public Decimal ProductCost { get; set; }
        public Byte ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public string ProductMeasurement { get; set; }
        public Byte ProductMaxDiscount { get; set; }
        public string ProductProvider { get; set; }
    }
}
