using ArelApp.Entities.Concrete;
using System.Collections.Generic;


namespace ArelApp.UI.Models
{

        public class RoleViewModel
        {
            public string Name { get; set; }
            public int RoleId { get; set; }
            public List<Role> Roles { get; set; }
        }
    
}
