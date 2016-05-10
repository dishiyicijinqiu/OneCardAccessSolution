using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl
    {
        public RegisterCollectionView()
        {
            InitializeComponent();
            this.Loaded += RegisterCollectionView_Loaded;
        }

        private void RegisterCollectionView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = new RegisterCollectionViewModel();
            }
            catch (Exception ex)
            {
                ex.HandleException(this);
                InterCloseDocument();
            }
        }

        protected override void Init()
        {
            base.Init();
            DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object, CreateViewEventArgs<RegisterEditMessage, string>, RegisterEditMessage, string>>().Subscribe(OnCreateRegisterView);
        }

        protected override void UnInit()
        { 
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object, CreateViewEventArgs<RegisterEditMessage, string>, RegisterEditMessage, string>>().Unsubscribe(OnCreateRegisterView);
        }

        private void OnCreateRegisterView(object sender, CreateViewEventArgs<RegisterEditMessage, string> args)
        {
            if (sender == this.DataContext)
            {
                var window = new UI.BaseRibbonWindow();
                window.Title = Properties.Resources.View_RegisterView_Title;
                window.Style = FindResource("DialogWindowStyle") as Style;
                window.Content = new RegisterView(this.DataContext, args.EditMessage);
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
            }
        }
    }
}
