using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;

namespace FengSharp.OneCardAccess.Services
{
    public class BasicInfoService : IBasicInfoService
    {
        public RegisterEntity GetRegisterEntityById(int RegisterId)
        {
            using (OneCardAccessDbContext db = new OneCardAccessDbContext())
            {
                var list = from r in db.T_Registers
                           join cu in db.T_UserInfos on r.CreateId equals cu.UserId
                           join mu in db.T_UserInfos on r.LastModifyId equals mu.UserId
                           where r.RegisterId == RegisterId
                           select new { r = r, Creater = cu.UserName, LastModifyer = mu.UserName };
                var item = list.FirstOrDefault();
                if (item == null) return null;
                var result = RegisterEntity.CreateEntity();
                result.CopyValueFrom(item.r);
                result.Creater = item.Creater;
                result.LastModifyer = item.LastModifyer;
                return result;
            }
        }

        public List<RegisterEntity> GetRegisterList()
        {
            using (OneCardAccessDbContext db = new OneCardAccessDbContext())
            {
                var list = from r in db.T_Registers
                           join cu in db.T_UserInfos on r.CreateId equals cu.UserId
                           join mu in db.T_UserInfos on r.LastModifyId equals mu.UserId
                           where !r.Deleted
                           select new { r = r, Creater = cu.UserName, LastModifyer = mu.UserName };
                var results = new List<RegisterEntity>();
                foreach (var item in list)
                {
                    var result = RegisterEntity.CreateEntity();
                    result.CopyValueFrom(item.r);
                    result.Creater = item.Creater;
                    result.LastModifyer = item.LastModifyer;
                    results.Add(result);
                }
                return results;

                //var list = from r in db.T_Registers
                //           join cu in db.T_UserInfos on r.CreateId equals cu.UserId into cujoin
                //           from _cu in cujoin.DefaultIfEmpty()
                //           join mu in db.T_UserInfos on r.LastModifyId equals mu.UserId into mujoin
                //           from _mu in mujoin.DefaultIfEmpty()
                //           where !r.Deleted
                //           select new { r = r, Creater = _cu.UserName, LastModifyer = _mu.UserName };
                //var results = new List<RegisterEntity>();
                //foreach (var item in list)
                //{
                //    var result = RegisterEntity.CreateEntity();
                //    result.CopyValueFrom(item.r);
                //    result.Creater = item.Creater;
                //    result.LastModifyer = item.LastModifyer;
                //    results.Add(result);
                //}
                //return results;

            }
        }
    }
}
