using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public int OrderPickupPointID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public int ReceiveCode { get; set; }

        // Navigation properties
        public User User { get; set; }
        public PickupPoint OrderPickupPoint { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
