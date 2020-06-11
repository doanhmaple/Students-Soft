using DoAnCoSo.Table;
using System;
using System.Data.Entity;
using System.Linq;

namespace DoAnCoSo
{
    public class database : DbContext
    {
        public database() : base("databasehocvusv")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Lop> Lops { get; set; }

        public DbSet<DonVi> DonVis { get; set; }

        public DbSet<DanhMuc> DanhMucs { get; set; }

        public DbSet<HocVu> HocVus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public virtual DbSet<FunctionRole> FunctionRoles { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
    }
}
