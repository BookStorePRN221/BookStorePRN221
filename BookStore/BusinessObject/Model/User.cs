using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class User
    {
        public Guid User_Id { get; set; }
        
        public int Role_Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string User_Account { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string User_Password { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "Tên phải bắt đầu bằng chữ hoa.")]
        public string User_Name { get; set; }
        [Required]
        [EmailAddress]
        public string User_Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string User_Address { get; set; }
        [Required]
        [Phone]
        public string User_Phone { get; set; }
        [Required]
        public string Is_User_Gender { get; set; }
        public bool? Is_User_Status { get; set; }

        public ICollection<Order>? Order { get; set; }
        public ICollection<Importation>? Importation { get; set; }
        public ICollection<Inventory>? Inventory { get; set; }
        public Role? Role { get; set; }
    }
}
