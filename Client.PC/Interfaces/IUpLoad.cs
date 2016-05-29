using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IUpLoadView : IView
    {
    }
    public interface IUpLoadViewModel : IViewModel
    {
        void AddUpLoadItem(IEnumerable<UpLoadAttachmentInfoEntity> uploaditems);
        void StartUpLoad();
    }
}
