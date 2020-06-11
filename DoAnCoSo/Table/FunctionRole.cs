using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo.Table
{
    public partial class FunctionRole
    {
        public int ID { get; set; }
        public int? FunctionID { get; set; }
        public int? RolesID { get; set; }
        public virtual Function Function { get; set; }

        public virtual Role Role { get; set; }
    }
}
