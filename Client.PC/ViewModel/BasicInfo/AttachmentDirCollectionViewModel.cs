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
using System.Text;
using System.IO;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class AttachmentDirCollectionViewModel : BaseNotificationObject, IAttachmentDirCollectionViewModel
    {
        public ICommand AddChildCommand { get; set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadAttachmentCommand { get; set; }
        public ICommand UpLoadCommand { get; set; }
        public ICommand DeleteAttachmentCommand { get; set; }
        public ICommand ViewAttachmentUpLoadCommand { get; set; }


        public AttachmentDirCollectionViewModel()
        {
            AddChildCommand = new DelegateCommand<FirstAttachmentDirEntity>(AddChild, CanAddChild);
            CopyAddCommand = new DelegateCommand<FirstAttachmentDirEntity>(CopyAdd, CanCopyAdd);
            EditCommand = new DelegateCommand<FirstAttachmentDirEntity>(Edit, CanEdit);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            LoadAttachmentCommand = new DelegateCommand<FirstAttachmentDirEntity>(LoadAttachment);
            UpLoadCommand = new DelegateCommand<FirstAttachmentDirEntity>(UpLoad, CanUpLoad);
            DeleteAttachmentCommand = new DelegateCommand<IList>(DeleteAttachment, CanDeleteAttachment);
            ViewAttachmentUpLoadCommand = new DelegateCommand(ViewAttachmentUpLoad, CanViewAttachmentUpLoad);

            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntitys().
                OrderBy(t => t.AttachmentDirNo).ThenBy(m => m.AttachmentDirName);
            Items = new ObservableCollection<FirstAttachmentDirEntity>(list);
            AttachmentInfoItems = new ObservableCollection<FirstAttachmentInfoEntity>();


            var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
            this.UploadMaximum = vm.GetTotalUpLoadItemCount();
            this.UploadNum = vm.GetUpLoadedItemCount();
            vm.OnUpLoadItemsChanged += Vm_OnAddedUpLoadItem;
            vm.OnUpLoadItemProgress += Vm_OnUpLoadItemProgress;
            vm.OnCompleteUpLoad += Vm_OnCompleteUpLoad;
        }


        private bool CanViewAttachmentUpLoad()
        {
            var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
            if (vm == null) return false;
            return vm.CanUpLoad;
        }

        private void ViewAttachmentUpLoad()
        {
            var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
            this.UploadMaximum = vm.GetTotalUpLoadItemCount();
            this.UploadNum = vm.GetUpLoadedItemCount();
            var view = ServiceLoader.LoadService<IUpLoadView>("IUpLoadView", new ParameterOverride("VM", vm));
            this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            (ViewAttachmentUpLoadCommand as DelegateCommand).RaiseCanExecuteChanged();
        }

        public bool CanDeleteAttachment(IList entitys)
        {
            if (entitys == null) return false;
            if (entitys.Count > 1)
            {

            }
            return entitys.Count > 0;
        }

        public void DeleteAttachment(IList entitys)
        {
            try
            {
                if (entitys == null || entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    var item = entitys[i] as FirstAttachmentInfoEntity;
                    ServiceProxyFactory.Create<IBasicInfoService>().DeleteAttachment(item);
                }
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    var item = entitys[i] as FirstAttachmentInfoEntity;
                    this.AttachmentInfoItems.Remove(item);
                }

                (AddChildCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private bool CanUpLoad(FirstAttachmentDirEntity entity)
        {
            if (entity == null) return false;
            if (entity.TreeSon > 0) return false;
            return true;
        }

        private void UpLoad(FirstAttachmentDirEntity entity)
        {
            try
            {
                string dir = GetFullDir(entity);
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                if (!string.IsNullOrWhiteSpace(entity.Filter))
                {
                    openFileDialog1.Filter = string.Format("{0}|{1}", entity.Filter.Replace(';', ','), entity.Filter);
                }
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
                    vm.AddUpLoadItem(
                        openFileDialog1.FileNames.Select(t =>
                        new UpLoadAttachmentInfoEntity()
                        {
                            AttachmentDirId = entity.AttachmentDirId,
                            AttachmentName = Path.GetFileName(t),
                            AttachmentPath = dir,
                            Message = string.Empty,
                            LocalPath = t
                        }
                        ));
                    var view = ServiceLoader.LoadService<IUpLoadView>("IUpLoadView", new ParameterOverride("VM", vm));
                    vm.StartUpLoad();
                    this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
                    (ViewAttachmentUpLoadCommand as DelegateCommand).RaiseCanExecuteChanged();
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void Vm_OnCompleteUpLoad()
        {
            (ViewAttachmentUpLoadCommand as DelegateCommand).RaiseCanExecuteChanged();
            var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
            if (vm.IsViewVisible) return;
            var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_CompleteUploadIsToView, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
            if (ShowMessage(deleteArgs) != MsgResult.Yes)
                return;
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var view = ServiceLoader.LoadService<IUpLoadView>("IUpLoadView", new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }));
        }
        private void Vm_OnUpLoadItemProgress(double progress)
        {
            this.UploadNum = progress;
        }

        private void Vm_OnAddedUpLoadItem(double totalCount)
        {
            this.UploadMaximum = totalCount;
        }

        private string GetFullDir(FirstAttachmentDirEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            var results = this.Items.Where(t => entity.TreePath.StartsWith(t.TreePath)).OrderBy(t => t.TreeTotal).ToList();
            foreach (var item in results)
            {
                if (!string.IsNullOrWhiteSpace(item.TreeParentNo))
                    sb.AppendFormat("{0}\\", item.AttachmentDirName);
            }
            return sb.ToString().TrimEnd(new char[] { '\\' });
        }

        public void LoadAttachment(FirstAttachmentDirEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    AttachmentInfoItems = new ObservableCollection<FirstAttachmentInfoEntity>();
                    return;
                }
                var list = ServiceProxyFactory.Create<IBasicInfoService>().
                    GetFirstAttachmentInfoEntitysByAttachmentDirId(entity.AttachmentDirId).
                    OrderBy(t => t.AttachmentName);
                AttachmentInfoItems = new ObservableCollection<FirstAttachmentInfoEntity>(list);
                SelectedAttachmentInfoItem = null;

                (AddChildCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private bool CanDelete(IList entitys)
        {
            if (entitys == null || entitys.Count <= 0) return false;
            if (this.AttachmentInfoItems != null && AttachmentInfoItems.Count > 0)
                return false;
            foreach (var item in entitys)
            {
                var entity = item as FirstAttachmentDirEntity;
                if (entity == null) return false;
                if (entity.TreeSon > 0) return false;
            }
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
        private bool CanEdit(FirstAttachmentDirEntity entity)
        {
            if (entity != null && string.IsNullOrWhiteSpace(entity.TreeParentNo))
                return false;
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
            if (entity != null && string.IsNullOrWhiteSpace(entity.TreeParentNo))
                return false;
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
            if (this.AttachmentInfoItems != null && AttachmentInfoItems.Count > 0)
                return false;
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
                        {
                            var oldItem = Items.FirstOrDefault(t => t.AttachmentDirId == EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    var newvm = new AttachmentDirViewModel(new AttachmentDirEditMessage(newItem.TreeParentNo, nextItem.AttachmentDirId, EntityEditMode.Edit));
                                    newvm.OnEntityViewEdited += OnEntityViewEdited;
                                    DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                        Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                                }
                                else
                                {
                                    SelectedEntity = Items[itemIndex];
                                    ShowMessage(Properties.Resources.Info_NoNext);
                                    DefaultEventAggregator.Current.GetEvent<CloseEvent>().
                                        Publish(vm.CloseEventToken, new CloseEventArgs(CloseStyle.NullClose));
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
        #region properties
        public ObservableCollection<FirstAttachmentDirEntity> Items { get; private set; }
        private ObservableCollection<FirstAttachmentInfoEntity> _AttachmentInfoItems;
        public ObservableCollection<FirstAttachmentInfoEntity> AttachmentInfoItems
        {
            get { return _AttachmentInfoItems; }
            set
            {
                _AttachmentInfoItems = value;
                RaisePropertyChanged("AttachmentInfoItems");
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
                (DeleteAttachmentCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
            }
        }

        private FirstAttachmentInfoEntity _SelectedAttachmentInfoItem;
        public FirstAttachmentInfoEntity SelectedAttachmentInfoItem
        {
            get { return _SelectedAttachmentInfoItem; }
            set
            {
                _SelectedAttachmentInfoItem = value;
                RaisePropertyChanged("SelectedAttachmentInfoItem");
                (DeleteAttachmentCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
            }
        }

        private FirstAttachmentDirEntity _SelectedEntity;
        public FirstAttachmentDirEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
                (AddChildCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
                (CopyAddCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
                (EditCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
                (UpLoadCommand as DelegateCommand<FirstAttachmentDirEntity>).RaiseCanExecuteChanged();
            }
        }

        private double _UploadMaximum;
        public double UploadMaximum
        {
            get { return _UploadMaximum; }
            set
            {
                _UploadMaximum = value;
                RaisePropertyChanged("UploadMaximum");
            }
        }
        private double _UploadNum;
        public double UploadNum
        {
            get { return _UploadNum; }
            set
            {
                _UploadNum = value;
                RaisePropertyChanged("UploadNum");
            }
        }

        #endregion
        public override void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.DocumentClose));
            //var vm = ServiceLoader.LoadService<IUpLoadViewModel>();
            //vm.OnUpLoadItemsChanged -= Vm_OnAddedUpLoadItem;
            //vm.OnUpLoadItemProgress -= Vm_OnUpLoadItemProgress;
            //vm.OnCompleteUpLoad -= Vm_OnCompleteUpLoad;
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
