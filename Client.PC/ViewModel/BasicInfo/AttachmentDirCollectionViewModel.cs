using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using Microsoft.Practices.Unity;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class AttachmentDirCollectionViewModel : BaseNotificationObject, IAttachmentDirCollectionViewModel
    {
        public ICommand AddChildCommand { get; set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; set; }
        public AttachmentDirCollectionViewModel()
        {
            AddChildCommand = new DelegateCommand<FirstAttachmentDirEntity>(AddChild, CanAddChild);
            CopyAddCommand = new DelegateCommand<FirstAttachmentDirEntity>(CopyAdd, CanCopyAdd);
            EditCommand = new DelegateCommand<FirstAttachmentDirEntity>(Edit, CanEdit);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntitys().
                OrderBy(t => t.AttachmentDirNo).ThenBy(m => m.AttachmentDirName);
            Items = new ObservableCollection<FirstAttachmentDirEntity>(list);
        }
        private bool CanDelete(IList entitys)
        {
            return true;
        }
        private void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteAttachmentDirEntitys(entitys.Cast<AttachmentDirEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Items.Remove(entitys[i] as FirstAttachmentDirEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private bool CanEdit(FirstAttachmentDirEntity entitys)
        {
            return true;
        }
        private void Edit(FirstAttachmentDirEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IAttachmentDirViewModel>(new ParameterOverride("EditMessage",
                    new AttachmentDirEditMessage(entity.TreeParentNo, entity.AttachmentDirId, EntityEditMode.Edit)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IAttachmentDirView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private bool CanCopyAdd(FirstAttachmentDirEntity entity)
        {
            return true;
        }
        private void CopyAdd(FirstAttachmentDirEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IAttachmentDirViewModel>(new ParameterOverride("EditMessage",
                    new AttachmentDirEditMessage(entity.TreeParentNo, _CopyKey: entity.AttachmentDirId, _EntityEditMode: EntityEditMode.CopyAdd)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IAttachmentDirView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));

            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private bool CanAddChild(FirstAttachmentDirEntity entity)
        {
            if (entity == null) return false;
            return true;
        }
        private void AddChild(FirstAttachmentDirEntity entity)
        {
            try
            {
                string TreeParentNo = null;
                string CopyId = null;
                if (entity != null)
                {
                    TreeParentNo = entity.TreeNo;
                    CopyId = entity.AttachmentDirId;
                }
                var editmessage = new AttachmentDirEditMessage(TreeParentNo, _EntityEditMode: EntityEditMode.CopyAdd, _CopyKey: CopyId);
                var vm = ServiceLoader.LoadService<IAttachmentDirViewModel>(new ParameterOverride("EditMessage", editmessage));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IAttachmentDirView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void OnEntityViewEdited(IViewModel vm, EditMessage<string> EditMessage)
        {
            try
            {
                switch (EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.Key);
                            var father = Items.First(t => t.TreeNo == newItem.TreeParentNo);
                            int newindex = Items.IndexOf(father) + Items.Count(t => t.TreeParentNo == father.TreeNo) + 1;
                            Items.Insert(newindex, newItem);
                            if (EditMessage.IsContinue)
                            {
                                var newvm = new AttachmentDirViewModel(new AttachmentDirEditMessage(newItem.TreeParentNo, _EntityEditMode: EntityEditMode.Add));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.Key);
                            var father = Items.First(t => t.TreeNo == newItem.TreeParentNo);
                            int newindex = Items.IndexOf(father) + Items.Count(t => t.TreeParentNo == father.TreeNo) + 1;
                            Items.Insert(newindex, newItem);
                            if (EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.AttachmentDirId == (EditMessage as AttachmentDirEditMessage).CopyKey);
                                SelectedEntity = copyItem;

                                var newvm = new AttachmentDirViewModel(new AttachmentDirEditMessage(newItem.TreeParentNo, _CopyKey: copyItem.AttachmentDirId, _EntityEditMode: EntityEditMode.CopyAdd));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
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
        #region properties
        public ObservableCollection<FirstAttachmentDirEntity> Items { get; private set; }

        private FirstAttachmentDirEntity _SelectedEntity;
        public FirstAttachmentDirEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
            }
        }
        #endregion
        public override void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.DocumentClose));
        }
    }

    public class AttachmentDirEditMessage : TreeEditMessage<string>
    {
        public AttachmentDirEditMessage(string _TreeParentNo = "", string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
        {
            TreeParentNo = _TreeParentNo;
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public string CopyKey { get; set; }
    }
}
