using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Unity;

namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// UserAndGroupManager.xaml 的交互逻辑
    /// </summary>
    public partial class UserAndGroupManagerView : BaseUserControl, IUserAndGroupManagerView
    {
        public UserAndGroupManagerView()
        {
            InitializeComponent();
        }
        public UserAndGroupManagerView(UserAndGroupManagerViewModel VM) : base(VM)
        {
            InitializeComponent();
            this.Loaded += UserAndGroupManagerView_Loaded;
        }

        bool isloaded = false;
        private void UserAndGroupManagerView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (!isloaded)
                {
                    try
                    {
                        UserAndGroupManagerViewModel VM = this.DataContext as UserAndGroupManagerViewModel;
                        this.UserGroupCollectionViewContent.Content =
                            ServiceLoader.LoadService<IUserGroupCollectionView>(new ParameterOverride("VM", VM.UserGroupCollectionViewModel));
                        this.UserCollectionViewContent.Content =
                            ServiceLoader.LoadService<IUserCollectionView>(new ParameterOverride("VM", VM.UserCollectionViewModel));
                    }
                    catch (Exception ex)
                    {
                        ex.HandleException(this);
                        this.Close(CloseStyle.DocumentClose);
                    }
                    isloaded = true;
                }
            }
        }
    }
}
