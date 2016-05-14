using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : CrudNotificationObject<RegisterEditMessage, string>, IRegisterCollectionView, IRegisterCollectionSelect
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public RegisterCollectionViewModel(CollectionViewStyle style = CollectionViewStyle.CollectionView)
        {
            this.CollectionViewStyle = style;
            AddCommand = new DelegateCommand(Add);
            CopyAddCommand = new DelegateCommand<FirstRegisterEntity>(CopyAdd);
            EditCommand = new DelegateCommand<FirstRegisterEntity>(Edit);
            DeleteCommand = new DelegateCommand<IList>(Delete);
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
        private void Add()
        {
            try
            {
                var vm = ServiceLoader.LoadService<IRegisterEdit>(
                 new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("ParentViewModel",this),
                    new Microsoft.Practices.Unity.ParameterOverride("EditMessage",new RegisterEditMessage())
                });
                var view = ServiceLoader.LoadService<IView>("RegisterView",
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("vm",vm),
                });
                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_RegisterView_Title));
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
                var vm = ServiceLoader.LoadService<IRegisterEdit>(
                 new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("ParentViewModel",this),
                    new Microsoft.Practices.Unity.ParameterOverride("EditMessage",new RegisterEditMessage(_CopyKey: entity.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd))
                });
                var view = ServiceLoader.LoadService<IView>("RegisterView",
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("vm",vm),
                });
                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_RegisterView_Title));
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
                var vm = ServiceLoader.LoadService<IRegisterEdit>(
                 new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("ParentViewModel",this),
                    new Microsoft.Practices.Unity.ParameterOverride("EditMessage",new RegisterEditMessage(entity.RegisterId, EntityEditMode.Edit))
                });
                var view = ServiceLoader.LoadService<IView>("RegisterView",
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("vm",vm),
                });
                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_RegisterView_Title));
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
                foreach (var item in entitys)
                {
                    this.Items.Remove(item as FirstRegisterEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
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



        private CollectionViewStyle _CollectionViewStyle;
        public CollectionViewStyle CollectionViewStyle
        {
            get { return _CollectionViewStyle; }
            set
            {
                _CollectionViewStyle = value;
                RaisePropertyChanged("CollectionViewStyle");
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
