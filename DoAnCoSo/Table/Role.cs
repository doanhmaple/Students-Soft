using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Table
{
    public partial class Role
    {
        public Role()
        {
            FunctionRoles = new HashSet<FunctionRole>();
            Users = new HashSet<User>();
        }
        [Key]
        public int RolesID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenRole { get; set; }
        public virtual ICollection<FunctionRole> FunctionRoles { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
