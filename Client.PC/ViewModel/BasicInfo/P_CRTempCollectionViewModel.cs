using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempCollectionViewModel : CrudNotificationObject<P_CRTempEditMessage, string>
    {
        public ICommand DeleteCommand { get; private set; }
        public P_CRTempCollectionViewModel()
        {
            DeleteCommand = new DelegateCommand<System.Collections.IList>(DeleteWithConfirm);
            Columns = new ObservableCollection<UI.BaseColumn>() {
                new UI.BaseColumn() { FieldName="CRTempName", Header= Properties.Resources.Entity_P_CRTemp_CRTempName},
                new UI.BaseColumn() { FieldName="CRTempPath", Header= Properties.Resources.Entity_P_CRTemp_CRTempPath,Visible=false},
                new UI.BaseColumn() { FieldName="MaterIden", Header= Properties.Resources.Entity_P_CRTemp_MaterIden},
                new UI.BaseColumn() { FieldName="IsEnable" , Header= Properties.Resources.Entity_P_CRTemp_IsEnable, Settings= UI.SettingsType.CheckEdit},
                new UI.BaseColumn() { FieldName="CateNo" , Header= Properties.Resources.Entity_P_CRTemp_CateNo},
                new UI.BaseColumn() { FieldName="Remark", Header= Properties.Resources.Entity_P_CRTemp_Remark },
                new UI.BaseColumn() { FieldName="Creater" , Header= Properties.Resources.Entity_P_CRTemp_Creater},
                new UI.BaseColumn() { FieldName="CreateDate", Header= Properties.Resources.Entity_P_CRTemp_CreateDate,DisplayFormat=Properties.Resources.Format_TimeString },
                new UI.BaseColumn() { FieldName="LastModifyer" , Header= Properties.Resources.Entity_P_CRTemp_LastModifyer},
                new UI.BaseColumn() { FieldName="LastModifyDate" , Header= Properties.Resources.Entity_P_CRTemp_LastModifyDate,DisplayFormat=Properties.Resources.Format_TimeString},
            };
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntitys().OrderBy(t => t.CateNo).ThenBy(m => m.CRTempName);
            Items = new ObservableCollection<FirstP_CRTempEntity>(list);
        }

        protected override void OnEntityViewEdited(object sender, EntityEditedEventArgs<P_CRTempEditMessage, string> args)
        {
            try
            {
                if (sender == null || sender != this)
                    return;
                switch (args.EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(args.EditMessage.Key);
                            Items.Add(newItem);
                            if (args.EditMessage.IsContinue)
                                ChangeChildViewDataContext(new P_CRTempViewModel(this, new P_CRTempEditMessage()));
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(args.EditMessage.Key);
                            Items.Add(newItem);
                            if (args.EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.P_CRTempId == args.EditMessage.CopyKey);
                                SelectedEntity = copyItem;
                                ChangeChildViewDataContext(new P_CRTempViewModel(this, new P_CRTempEditMessage(_CopyKey: copyItem.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd)));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.P_CRTempId == args.EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(args.EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (args.EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    ChangeChildViewDataContext(new P_CRTempViewModel(this, new P_CRTempEditMessage(nextItem.P_CRTempId, EntityEditMode.Edit)));
                                }
                                else
                                {
                                    SelectedEntity = Items[itemIndex];
                                    ShowMessage(Properties.Resources.Info_NoNext);
                                    CloseChild();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void DeleteCore(MsgResult msgResult, object[] param)
        {
            try
            {
                if (msgResult != MsgResult.Yes)
                    return;
                var SelectedEntitys = param[0] as System.Collections.IList;
                if (SelectedEntitys == null || SelectedEntitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var listToDelete = SelectedEntitys.Cast<P_CRTempEntity>().ToList();
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteP_CRTempEntitys(listToDelete);
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                foreach (var item in listToDelete)
                {
                    this.Items.Remove(item as FirstP_CRTempEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void DeleteWithConfirm(System.Collections.IList SelectedEntitys)
        {
            var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, null, MsgButton.YesNo, MsgImage.Information, DeleteCore, SelectedEntitys);
            ShowMessage(deleteArgs);
        }

        #region propertys
        public ObservableCollection<FirstP_CRTempEntity> Items { get; private set; }
        public ObservableCollection<UI.BaseColumn> Columns { get; private set; }

        private P_CRTempEntity _SelectedEntity;
        public P_CRTempEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
            }
        }
        #endregion
    }
    #region message
    public class P_CRTempEditMessage : EditMessage<string>
    {
        public P_CRTempEditMessage(string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
        {
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public string CopyKey { get; set; }
    }

    #endregion
}
