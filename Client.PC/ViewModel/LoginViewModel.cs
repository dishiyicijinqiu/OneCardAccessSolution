using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public IMessageBoxService MessageBoxService
        {
            get
            {
                return GetService<IMessageBoxService>();
            }
        }
        #region Propertys
        public string _UserNo;
        public string UserNo
        {
            get
            {
                return _UserNo;
            }
            set
            {

                if (_UserNo == value) return;
                _UserNo = value;
                RaisePropertyChanged("UserNo");
            }
        }
        public string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {

                if (_Password == value) return;
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }
        #endregion
        public LoginViewModel()
        {
            DialogCommands = new List<UICommand>();
            LoginUICommand = new UICommand()
            {
                Caption = "登录",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(OnLogin)
            };
            DialogCommands.Add(LoginUICommand);
            CancelUICommand = new UICommand()
            {
                Id = System.Windows.MessageBoxResult.Cancel,
                Caption = "取消",
                IsCancel = true,
                IsDefault = false,
            };
            DialogCommands.Add(CancelUICommand);
        }

        #region UICommands
        public List<UICommand> DialogCommands { get; private set; }
        public UICommand LoginUICommand { get; private set; }
        public UICommand CancelUICommand { get; private set; }
        #endregion
        #region  methods
        private void OnLogin(CancelEventArgs e)
        {
            IRBACService irbacservice = ServiceProxyFactory.Create<IRBACService>();
            LoginResult loginresult = irbacservice.Login(this.UserNo, this.Password);
            switch (loginresult.LoginMessage)
            {
                case LoginMessage.UserNotExist:
                    MessageBoxService.ShowMessage(Properties.Resources.UserNotExist, Properties.Resources.MessageBoxError, MessageButton.OK, MessageIcon.Error);
                    e.Cancel = true;
                    return;
                case LoginMessage.UserIsLocked:
                    MessageBoxService.ShowMessage(Properties.Resources.UserIsLocked, Properties.Resources.MessageBoxError, MessageButton.OK, MessageIcon.Error);
                    e.Cancel = true;
                    return;
                case LoginMessage.ErrorPassWord:
                    MessageBoxService.ShowMessage(Properties.Resources.ErrorPassWord, Properties.Resources.MessageBoxError, MessageButton.OK, MessageIcon.Error);
                    e.Cancel = true;
                    return;
                default:
                    UserIdentity.Current = loginresult.UserIdentity;
                    break;
            }
        }
        #endregion
    }
}
