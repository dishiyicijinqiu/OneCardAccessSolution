using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;

namespace FengSharp.OneCardAccess.Services
{
    public class BasicInfoService : IBasicInfoService
    {
        public List<RegisterEntity> GetRegisterList()
        {
            using (OneCardAccessDbContext db = new OneCardAccessDbContext())
            {
                var datalist = db.T_Registers.Where(t => !t.Deleted).ToList();
                return datalist.Select(t => new RegisterEntity()
                {
                    CreateDate = t.CreateDate,
                    CreateId = t.CreateId,
                    EndDate = t.EndDate,
                    LastModifyDate = t.LastModifyDate,
                    LastModifyId = t.LastModifyId,
                    RegisterFile = t.RegisterFile,
                    RegisterId = t.RegisterId,
                    RegisterNo = t.RegisterNo,
                    RegisterNo1 = t.RegisterNo1,
                    RegisterProductName = t.RegisterProductName,
                    RegisterProductName1 = t.RegisterProductName1,
                    Remark = t.Remark,
                    StandardCode = t.StandardCode,
                    StandardCode1 = t.StandardCode1,
                    StartDate = t.StartDate
                }).ToList();
            }
        }
    }
}
