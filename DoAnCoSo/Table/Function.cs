using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo.Table
{
    public partial class Function
    {
        public Function()
        {
            FunctionRoles = new HashSet<FunctionRole>();
        }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string Formname { get; set; }

        public virtual ICollection<FunctionRole> FunctionRoles { get; set; }
    }
}
