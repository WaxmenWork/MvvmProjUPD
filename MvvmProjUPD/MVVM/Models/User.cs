using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserSurname { get; set; }
        public string UserName { get; set; }
        public string UserPatronymic { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int UserRoleID { get; set; }

        // Navigation properties
        public Role UserRole { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
