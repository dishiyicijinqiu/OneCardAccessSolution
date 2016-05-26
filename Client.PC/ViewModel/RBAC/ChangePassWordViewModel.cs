using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class ChangePassWordViewModel : BaseNotificationObject, IChangePassWordViewModel
    {
        public ICommand ChangePasswordCommand { get; private set; }
        public ChangePassWordViewModel()
        {
            ChangePasswordCommand = new DelegateCommand(ChangePassword, CanChangePassword);
        }

        public bool CanChangePassword()
        {
            if (string.IsNullOrEmpty(this.OldPassword))
                return false;
            if (string.IsNullOrEmpty(this.NewPassword))
                return false;
            if (string.Compare(this.NewPassword, this.RNewPassword, false) != 0)
                return false;
            if (this.NewPassword.Length < 3 || this.NewPassword.Length > 20)
                return false;
            return true;
        }

        public void ChangePassword()
        {
            try
            {
                ServiceProxyFactory.Create<IRBACService>().ChangePassword(this.OldPassword, this.NewPassword);
                ShowMessage(Properties.Resources.Info_SaveSuccess);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        #region propertys
        private string _OldPassword;

        public string OldPassword
        {
            get { return _OldPassword; }
            set
            {
                _OldPassword = value;
                RaisePropertyChanged("OldPassword");
                (ChangePasswordCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        private string _NewPassword;

        public string NewPassword
        {
            get { return _NewPassword; }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged("NewPassword");
                (ChangePasswordCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        private string _RNewPassword;

        public string RNewPassword
        {
            get { return _RNewPassword; }
            set
            {
                _RNewPassword = value;
                RaisePropertyChanged("RNewPassword");
                (ChangePasswordCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        #endregion
    }
}
