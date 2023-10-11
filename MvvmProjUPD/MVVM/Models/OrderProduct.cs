using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models
{
    public class OrderProduct
    {
        public int OrderID { get; set; }
        public string ProductArticleNumber { get; set; }
        public int Count { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
