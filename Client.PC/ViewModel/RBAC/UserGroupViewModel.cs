using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserGroupViewModel : BaseNotificationObject, IUserGroupViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public UserGroupViewModel(UserGroupEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstP_CRTempEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.CopyAdd:
                    var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.CopyKey);
                    Entity.CopyValueFrom(copyEntity,
                        new List<string>(PCConfig.CreateAndModifyInfoColNames)
                    {
                            "P_CRTempId"
                    });
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstP_CRTempEntity.CreateEntity();
            IsSee = EditMessage.EntityEditMode == EntityEditMode.See;
        }
    }
}
