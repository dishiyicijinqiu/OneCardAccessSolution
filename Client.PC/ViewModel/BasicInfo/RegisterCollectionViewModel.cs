using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : BaseNotificationObject, IRegisterCollectionViewModel, IRegisterCollectionSelectViewModel
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }
        public RegisterCollectionViewModel() : this(ViewStyle.View) { }
        public RegisterCollectionViewModel(ViewStyle ViewStyle)
        {
            this.ViewStyle = ViewStyle;
            AddCommand = new DelegateCommand(Add);
            CopyAddCommand = new DelegateCommand<FirstRegisterEntity>(CopyAdd);
            EditCommand = new DelegateCommand<FirstRegisterEntity>(Edit);
            DeleteCommand = new DelegateCommand<IList>(Delete);
            ConfirmCommand = new DelegateCommand<IList>(Confirm);
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntitys().OrderBy(t => t.RegisterName).ThenBy(m => m.RegisterNo);
            Items = new ObservableCollection<FirstRegisterEntity>(list);
        }
        private void OnEntityViewEdited(IViewModel vm, Core.EditMessage<string> EditMessage)
        {
            try
            {
                switch (EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                            {
                                var newvm = new RegisterViewModel(new RegisterEditMessage());
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.RegisterId == (EditMessage as RegisterEditMessage).CopyKey);
                                SelectedEntity = copyItem;
                                var newvm = new RegisterViewModel(new RegisterEditMessage(_CopyKey: copyItem.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));


                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.RegisterId == EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    //ChangeChildViewDataContext(new RegisterViewModel(this, new RegisterEditMessage(nextItem.RegisterId, EntityEditMode.Edit)));

                                    var newvm = new RegisterViewModel(new RegisterEditMessage(nextItem.RegisterId, EntityEditMode.Edit));
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
        private void Add()
        {
            try
            {
                var vm = ServiceLoader.LoadService<IRegisterViewModel>(new ParameterOverride("EditMessage", new RegisterEditMessage()));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IRegisterView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void CopyAdd(FirstRegisterEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IRegisterViewModel>(new ParameterOverride("EditMessage", new RegisterEditMessage(_CopyKey: entity.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IRegisterView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Edit(FirstRegisterEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IRegisterViewModel>(new ParameterOverride("EditMessage", new RegisterEditMessage(entity.RegisterId, EntityEditMode.Edit)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IRegisterView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteRegisterEntitys(entitys.Cast<RegisterEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Items.Remove(entitys[i] as FirstRegisterEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Confirm(IList entitys)
        {
            try
            {
                if (entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                SelectItems = entitys.Cast<RegisterEntity>().ToList();
                this.Close();
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
        private RegisterEntity _SelectedEntity;
        public RegisterEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
            }
        }

        public List<RegisterEntity> SelectItems { get; set; }

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
}
