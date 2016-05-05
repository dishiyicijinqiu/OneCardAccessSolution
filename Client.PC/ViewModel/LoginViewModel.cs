using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class LoginViewModel : NotificationObject
    {

        #region Commands
        public ICommand LoginCommand { get; private set; }
        #endregion
        public LoginViewModel(bool isReLogin)
        {
            this.IsReLogin = isReLogin;
            LoginCommand = new DelegateCommand(Login);
            if (Session.Current != null)
            {
                this.UserNo = Session.Current.SessionClientNo;
            }
            else
            {
                this.UserNo = string.Empty;
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
        public bool _IsReLogin;
        public bool IsReLogin
        {
            get
            {
                return _IsReLogin;
            }
            set
            {

                if (_IsReLogin == value) return;
                _IsReLogin = value;
                RaisePropertyChanged("IsReLogin");
            }
        }
        #endregion
        #region  methods
        public void Login()
        {
            try
            {
                IConnectService ConnectService = ServiceProxyFactory.Create<IConnectService>();
                LoginResult loginresult = ConnectService.Login(this.UserNo, this.Password);
                DefaultEventAggregator.Current.GetEvent<LoginedEvent>().Publish(new LoginedEventArgs(loginresult));
            }
            catch (System.Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        #endregion
    }
    #region EventsDef
    public class LoginEvent : CompositePresentationEvent<LoginEventArgs> { }
    public class LoginEventArgs
    {
        public LoginEventArgs(LoginState LoginState)
        {
            this.LoginState = LoginState;
        }
        public LoginState LoginState { get; set; }
    }
    public enum LoginState
    {
        NewLogin = 0,
        ReLogin = 1,
        TimeOutLogin = 2
    }
    public class LoginedEvent : CompositePresentationEvent<LoginedEventArgs> { }
    public class LoginedEventArgs
    {
        public LoginedEventArgs(LoginResult loginResult)
        {
            this.LoginResult = loginResult;
        }
        public LoginResult LoginResult { get; set; }
    }
    public class LoginSuccessEvent : NullEvent
    {

    }
    #endregion
}
