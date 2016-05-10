using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : CrudNotificationObject<RegisterEditMessage, string>
    {
        public ICommand DeleteCommand { get; private set; }
        public RegisterCollectionViewModel()
        {
            DeleteCommand = new DelegateCommand<System.Collections.IList>(DeleteWithConfirm);
            Columns = new ObservableCollection<UI.BaseColumn>() {
                new UI.BaseColumn() { FieldName="RegisterName", Header= Properties.Resources.Entity_Register_RegisterName,Width=150},
                new UI.BaseColumn() { FieldName="RegisterNo", Header= Properties.Resources.Entity_Register_RegisterNo,Width=300},
                new UI.BaseColumn() { FieldName="StandardCode", Header= Properties.Resources.Entity_Register_StandardCode,Width=120},
                new UI.BaseColumn() { FieldName="RegisterName1" , Header= Properties.Resources.Entity_Register_RegisterName1},
                new UI.BaseColumn() { FieldName="RegisterNo1" , Header= Properties.Resources.Entity_Register_RegisterNo1},
                new UI.BaseColumn() { FieldName="StandardCode1", Header= Properties.Resources.Entity_Register_StandardCode1 },

                new UI.BaseColumn() { FieldName="StartDate", Header= Properties.Resources.Entity_Register_StartDate,Width=120, Settings= UI.SettingsType.Date, DisplayFormat=Properties.Resources.Format_DateString },
                new UI.BaseColumn() { FieldName="EndDate", Header= Properties.Resources.Entity_Register_EndDate,Width=120, Settings= UI.SettingsType.Date, DisplayFormat=Properties.Resources.Format_DateString },
                //,DisplayFormat=Properties.Resources.Format_DateString

                new UI.BaseColumn() { FieldName="Creater" , Header= Properties.Resources.Entity_Register_Creater},
                new UI.BaseColumn() { FieldName="CreateDate", Header= Properties.Resources.Entity_Register_CreateDate,Width=120,DisplayFormat=Properties.Resources.Format_TimeString },
                new UI.BaseColumn() { FieldName="LastModifyer" , Header= Properties.Resources.Entity_Register_LastModifyer},
                new UI.BaseColumn() { FieldName="LastModifyDate" , Header= Properties.Resources.Entity_Register_LastModifyDate,Width=120,DisplayFormat=Properties.Resources.Format_TimeString},
                new UI.BaseColumn() { FieldName="Remark", Header= Properties.Resources.Entity_Register_Remark },
            };
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntitys().OrderBy(t => t.RegisterName).ThenBy(m => m.RegisterNo);
            Items = new ObservableCollection<FirstRegisterEntity>(list);
        }

        protected override void OnEntityViewEdited(object sender, EntityEditedEventArgs<RegisterEditMessage, string> args)
        {
            try
            {
                if (sender != this)
                    return;
                switch (args.EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.EditMessage.Key);
                            Items.Add(newItem);
                            if (args.EditMessage.IsContinue)
                                ChangeChildViewDataContext(new RegisterViewModel(this, new RegisterEditMessage()));
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.EditMessage.Key);
                            Items.Add(newItem);
                            if (args.EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.RegisterId == args.EditMessage.CopyKey);
                                SelectedEntity = copyItem;
                                ChangeChildViewDataContext(new RegisterViewModel(this, new RegisterEditMessage(_CopyKey: copyItem.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd)));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.RegisterId == args.EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (args.EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    ChangeChildViewDataContext(new RegisterViewModel(this, new RegisterEditMessage(nextItem.RegisterId, EntityEditMode.Edit)));
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
                var listToDelete = SelectedEntitys.Cast<RegisterEntity>().ToList();
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteRegisterEntitys(listToDelete);
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                foreach (var item in listToDelete)
                {
                    this.Items.Remove(item as FirstRegisterEntity);
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

        public ObservableCollection<UI.BaseColumn> Columns { get; private set; }
        ObservableCollection<FirstRegisterEntity> _Items;
        public ObservableCollection<FirstRegisterEntity> Items
        {
            get
            {
                return _Items;
            }
            protected set
            {
                _Items = value;
                RaisePropertyChanged("Items");
            }
        }

        private FirstRegisterEntity _SelectedEntity;
        public FirstRegisterEntity SelectedEntity
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
    public class RegisterEditMessage : EditMessage<string>
    {
        public RegisterEditMessage(string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
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
