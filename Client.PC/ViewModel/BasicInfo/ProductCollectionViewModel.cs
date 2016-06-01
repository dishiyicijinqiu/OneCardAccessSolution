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
    public class ProductCollectionViewModel : BaseNotificationObject, IProductCollectionViewModel
    {
        public event OnSelectedItems<ProductEntity> OnSelectedItems;
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand AddChildCommand { get; set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }
        public ICommand DeepInCommand { get; private set; }
        public ICommand DeepOutCommand { get; private set; }
        public ProductCollectionViewModel() : this(ViewStyle.View) { }
        public ProductCollectionViewModel(ViewStyle ViewStyle)
        {
            this.ViewStyle = ViewStyle;
            AddCommand = new DelegateCommand(Add, CanAdd);
            AddChildCommand = new DelegateCommand<FirstProductEntity>(AddChild, CanAddChild);
            CopyAddCommand = new DelegateCommand<FirstProductEntity>(CopyAdd, CanCopyAdd);
            EditCommand = new DelegateCommand<FirstProductEntity>(Edit, CanEdit);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            ConfirmCommand = new DelegateCommand<IList>(Confirm, CanConfirm);
            DeepInCommand = new DelegateCommand<FirstProductEntity>(DeepIn, CanDeepIn);
            DeepOutCommand = new DelegateCommand(DeepOut, CanDeepOut);
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductTreeEntitysByTreeParentNo(string.Empty).OrderBy(t => t.ProductNo).ThenBy(m => m.ProductName);
            Items = new ObservableCollection<FirstProductEntity>(list);
        }

        private bool CanDeepOut()
        {
            if (FatherEntity == null) return false;
            return true;
        }

        private void DeepOut()
        {
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductTreeEntitysByTreeParentNo(FatherEntity.TreeNo).OrderBy(t => t.ProductNo).ThenBy(m => m.ProductName);
            Items = new ObservableCollection<FirstProductEntity>(list);
        }

        private bool CanDeepIn(FirstProductEntity entity)
        {
            if (entity == null) return false;
            if (entity.TreeSon > 0)
                return true;
            return false;
        }

        private void DeepIn(FirstProductEntity entity)
        {
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductTreeEntitysByTreeParentNo(entity.TreeNo).OrderBy(t => t.ProductNo).ThenBy(m => m.ProductName);
            Items = new ObservableCollection<FirstProductEntity>(list);
            this.FatherEntity = entity;
        }

        public void Confirm(IList entitys)
        {
            try
            {
                if (entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                this.Close();
                var SelectedItems = entitys.Cast<ProductEntity>().ToList();
                OnSelectedItems?.Invoke(SelectedItems);
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanConfirm(IList entitys)
        {
            return true;
        }

        public bool CanDelete(IList entitys)
        {
            if (entitys == null) return false;
            foreach (var item in entitys)
            {
                var entity = item as FirstProductEntity;
                if (entity.TreeSon > 0) return false;
                if (entity.SkuSon > 0) return false;
                if (string.IsNullOrEmpty(entity.TreeParentNo)) return false;
            }
            return true;
        }

        public void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteProductEntitys(entitys.Cast<ProductEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Items.Remove(entitys[i] as FirstProductEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanEdit(FirstProductEntity entity)
        {
            if (entity == null) return false;
            if (string.IsNullOrEmpty(entity.TreeParentNo)) return false;
            return true;
        }

        public void Edit(FirstProductEntity entity)
        {
            try
            {
                if (!CanEdit(entity)) return;
                var vm = ServiceLoader.LoadService<IProductViewModel>(new ParameterOverride("EditMessage",
                    new ProductEditMessage(entity.TreeParentNo, entity.ProductId, EntityEditMode.Edit)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IProductView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanCopyAdd(FirstProductEntity entity)
        {
            if (entity == null) return false;
            if (string.IsNullOrEmpty(entity.TreeParentNo)) return false;
            return true;
        }

        public void CopyAdd(FirstProductEntity entity)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IProductViewModel>(new ParameterOverride("EditMessage",
                    new ProductEditMessage(entity.TreeParentNo, _CopyKey: entity.ProductId, _EntityEditMode: EntityEditMode.CopyAdd)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IProductView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanAddChild(FirstProductEntity entity)
        {
            if (entity == null) return false;
            if (entity.SkuSon > 0)
                return false;
            if (entity.SkuSon > 0)
                return false;
            return true;
        }

        public void AddChild(FirstProductEntity entity)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IProductViewModel>(new ParameterOverride("EditMessage",
        new ProductEditMessage(entity.TreeNo)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IProductView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanAdd()
        {
            return true;
        }

        public void Add()
        {
            try
            {
                string treeparentno = string.Empty;
                if (FatherEntity != null)
                    treeparentno = FatherEntity.TreeNo;
                var vm = ServiceLoader.LoadService<IProductViewModel>(new ParameterOverride("EditMessage",
        new ProductEditMessage(treeparentno)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IProductView>(new ParameterOverride("VM", vm));
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
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductEntityById(EditMessage.Key);
                            var father = Items.First(t => t.TreeNo == newItem.TreeParentNo);

                            int newindex = Items.IndexOf(father) + Items.Count(t => t.TreeParentNo == father.TreeNo) + 1;
                            Items.Insert(newindex, newItem);
                            if (EditMessage.IsContinue)
                            {
                                var newvm = new ProductViewModel(new ProductEditMessage(newItem.TreeParentNo, _EntityEditMode: EntityEditMode.Add));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public override void Close()
        {
            switch (ViewStyle)
            {
                case ViewStyle.View:
                    DefaultEventAggregator.Current.GetEvent<CloseEvent>().Publish(this.CloseEventToken, new CloseEventArgs(CloseStyle.DocumentClose));
                    break;
                default:
                    base.Close();
                    break;
            }
        }
        #region propertys
        public ObservableCollection<FirstProductEntity> Items { get; private set; }
        public FirstProductEntity FatherEntity { get; private set; }

        private FirstProductEntity _SelectedEntity;
        public FirstProductEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
            }
        }

        private ViewStyle _ViewStyle;

        public ViewStyle ViewStyle
        {
            get { return _ViewStyle; }
            set
            {
                _ViewStyle = value;
                RaisePropertyChanged("ViewStyle");
            }
        }
        #endregion
    }
    public class ProductEditMessage : TreeEditMessage<string>
    {
        public ProductEditMessage(string _TreeParentNo = "", string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
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
