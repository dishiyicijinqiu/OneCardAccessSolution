using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;
using System;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class LoginViewModel : BaseNotificationObject, ILoginViewModel
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
                //IConnectService channelB = new System.ServiceModel.ChannelFactory<IConnectService>("ConnectService").CreateChannel();
                //var result = channelB.Login(this.UserNo, this.Password);
                IConnectService ConnectService = ServiceProxyFactory.Create<IConnectService>();
                var loginresult = ConnectService.Login(this.UserNo, this.Password);
                switch (loginresult)
                {
                    case LoginResult.Success:
                        DefaultEventAggregator.Current.GetEvent<NullEvent>().Publish(ServiceLoader.LoadService<IMainViewModel>().LoginSucessEventSubscriptionToken);
                        this.OKClose();
                        break;
                    case LoginResult.UserNotExist:
                        ShowError(Properties.Resources.Error_UserNotExist);
                        return;
                    case LoginResult.UserIsLocked:
                        ShowError(Properties.Resources.Error_UserIsLocked);
                        return;
                    case LoginResult.ErrorPassWord:
                        ShowError(Properties.Resources.Error_PassWordIsError);
                        return;
                    case LoginResult.UserIsEmpty:
                        ShowError(Properties.Resources.Error_UserIsEmpty);
                        return;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        #endregion
    }

    #region EventsDef
    public class LoginEvent : SenderEvent<LoginEventArgs> { }
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
    public class LoginedEvent : SenderEvent<LoginedEventArgs> { }
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
