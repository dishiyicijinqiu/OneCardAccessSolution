using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.TEntity.BasicInfo;

namespace FengSharp.OneCardAccess.Services
{
    public class BasicInfoService : IBasicInfoService
    {
        public RegisterEntity GetRegisterEntityById(int RegisterId)
        {
            using (OneCardAccessDbContext dc = new OneCardAccessDbContext())
            {
                var dbentity = dc.T_Registers.FirstOrDefault(t => t.RegisterId == RegisterId);
                if (dbentity == null) return null;
                var result = new RegisterEntity();
                result.CopyValueFrom(dbentity, new string[] { "Deleted" });
                return result;
            }
        }

        public List<RegisterEntity> GetRegisterList()
        {
            using (OneCardAccessDbContext db = new OneCardAccessDbContext())
            {
                var datalist = db.T_Registers.Where(t => !t.Deleted).ToList();
                var results = new List<RegisterEntity>(datalist.Count);
                ClassValueCopier.CopyArray(results, datalist);
                return results;
            }
        }
    }
}
