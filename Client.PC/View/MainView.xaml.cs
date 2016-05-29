using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.Events;
using System.Windows;
using System.Windows.Controls;
using FengSharp.OneCardAccess.Core;
using DevExpress.Xpf.Core;
using System;
using DevExpress.Xpf.Docking;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using FengSharp.OneCardAccess.Client.PC.UI;
using Microsoft.Practices.Unity;
using FengSharp.OneCardAccess.Client.PC.Interfaces;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : BaseUserControl, IMainView
    {
        System.Collections.Generic.Dictionary<object, DocumentPanel> docs = new System.Collections.Generic.Dictionary<object, DocumentPanel>();

        public MainView()
        {
            InitializeComponent();
        }
        public MainView(MainViewModel VM) : base(VM)
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.Loaded += MainView_Loaded;
            }
        }
        protected override void Init()
        {
            base.Init();
            var vm = this.DataContext as MainViewModel;
            vm.ShowDocumentEventSubscriptionToken = DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Subscribe(OnShowDocument);
            vm.UICloseDocumentEventSubscriptionToken = DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Subscribe(OnCloseDocument);
            vm.LoginSucessEventSubscriptionToken = DefaultEventAggregator.Current.GetEvent<NullEvent>().Subscribe(OnLoginSucess);
        }

        protected override void UnInit()
        {
            base.UnInit();
            var vm = this.DataContext as MainViewModel;
            DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Unsubscribe(vm.ShowDocumentEventSubscriptionToken);
            DefaultEventAggregator.Current.GetEvent<UICloseDocumentEvent>().Unsubscribe(vm.UICloseDocumentEventSubscriptionToken);
            DefaultEventAggregator.Current.GetEvent<NullEvent>().Unsubscribe(vm.LoginSucessEventSubscriptionToken);
        }
        bool isfirstlogin = true;
        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isfirstlogin)
                return;
            if (isfirstlogin)
            {
                var vm = this.DataContext as MainViewModel;
                DefaultEventAggregator.Current.GetEvent<LoginEvent>().Publish
                    (vm.LoginEventSubscriptionToken, new LoginEventArgs(LoginState.NewLogin));
                isfirstlogin = false;
            }
        }
        public void OnCloseDocument(SubscriptionToken sender, UICloseDocumentEventArgs args)
        {
            var vm = this.DataContext as MainViewModel;
            if (sender != vm.UICloseDocumentEventSubscriptionToken)
                return;
            if (docs.ContainsKey(args.PanelContent))
            {
                lock (docs)
                {
                    if (docs.ContainsKey(args.PanelContent))
                    {
                        var document = docs[args.PanelContent];
                        dockLayoutManager.DockController.RemovePanel(document);
                        docs.Remove(args.PanelContent);
                        if (args.CallBack != null)
                            args.CallBack();
                    }
                }
            }
        }
        public void OnShowDocument(SubscriptionToken sender, ShowDocumentEventArgs args)
        {
            var vm = this.DataContext as MainViewModel;
            if (sender != vm.ShowDocumentEventSubscriptionToken)
                return;
            var docInfo = args.DocumentInfo;
            var vmdoc = ServiceLoader.LoadService(System.Type.GetType(docInfo.DocumentVMType), docInfo.DocumentName);
            var viewdoc = ServiceLoader.LoadService(System.Type.GetType(docInfo.DocumentType), null, new ParameterOverride("VM", vmdoc));
            if (docs.ContainsKey(viewdoc))
            {
                docs[viewdoc].IsActive = true;
                return;
            }
            var doc = dockLayoutManager.DockController.AddDocumentPanel(mdiContainer);
            try
            {
                doc.AllowDrag = false;
                doc.IsActive = true;
                doc.FloatOnDoubleClick = false;
                doc.Caption = docInfo.DocumentTitle;

                doc.Content = viewdoc;
                doc.CloseCommand = new DelegateCommand<DocumentPanel>((panel) =>
                {
                    if (panel != null && panel.Content != null)
                    {
                        OnCloseDocument(vm.UICloseDocumentEventSubscriptionToken, new UICloseDocumentEventArgs(panel.Content));
                    }
                });
                docs.Add(doc.Content, doc);
            }
            catch (Exception ex)
            {
                dockLayoutManager.DockController.RemovePanel(doc);
                throw ex;
            }
        }
        private void OnLoginSucess(SubscriptionToken sender, NullEventArgs args)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                var vm = this.DataContext as MainViewModel;
                if (sender != vm.LoginSucessEventSubscriptionToken)
                    return;
                var mainwindow = Window.GetWindow(this);
                mainwindow.ShowInTaskbar = true;
                mainwindow.Opacity = 100;
            }));
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((DevExpress.Xpf.Ribbon.ApplicationMenu)ribbonControl1.ApplicationMenu).ClosePopup();
        }
    }
}
