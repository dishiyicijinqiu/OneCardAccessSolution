using FengSharp.OneCardAccess.TEntity.RBAC;
using FengSharp.OneCardAccess.TEntity.BasicInfo;
using System.Data.Entity;

namespace FengSharp.OneCardAccess.Services
{
    internal class OneCardAccessDbContext : DbContext
    {
        public OneCardAccessDbContext() : base("name=DefaultConnectionString")
        {
        }
        public virtual DbSet<T_UserInfo> T_UserInfos { get; set; }
        public virtual DbSet<T_Register> T_Registers { get; set; }
    }
}
