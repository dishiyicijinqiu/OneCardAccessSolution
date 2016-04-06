using FengSharp.OneCardAccess.TEntity.RBAC;
using System.Data.Entity;

namespace FengSharp.OneCardAccess.Services
{
    internal class OneCardAccessDbContext : DbContext
    {
        public OneCardAccessDbContext() : base("name=DefaultConnectionString")
        {
        }
        public virtual DbSet<T_UserInfo> T_UserInfos { get; set; }
    }
}
