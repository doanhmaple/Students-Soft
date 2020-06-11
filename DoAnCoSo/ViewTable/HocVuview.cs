using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo.ViewTable
{
    public class HocVuview
    {
        public int HocVuID { get; set; }
        public DateTime NgayTao { get; set; }
        public string NoiDung { get; set; }
        public bool TinhTrang { get; set; }
        public int ParentID { get; set; }
        public DateTime NgayHen { get; set; }
        public string UserName { get; set; }
        public string TenDanhMuc { get; set; }
        
        public string TenDonVi { get; set; }
    }
}
