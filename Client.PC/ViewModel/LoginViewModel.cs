using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System.ComponentModel;
using FengSharp.OneCardAccess.Core;
using DevExpress.Xpf.Core;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Services
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        #endregion
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


                //IConnectService ConnectService = ServiceLoader.LoadService<IConnectService>();
                LoginResult loginresult = ConnectService.Login(this.UserNo, this.Password);
                switch (loginresult)
                {
                    case LoginResult.UserIsEmpty:
                        MessageBoxService.ShowMessage(Properties.Resources.Error_UserIsEmpty, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        return;
                    case LoginResult.UserNotExist:
                        MessageBoxService.ShowMessage(Properties.Resources.Error_UserNotExist, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        return;
                    case LoginResult.UserIsLocked:
                        MessageBoxService.ShowMessage(Properties.Resources.Error_UserIsLocked, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        return;
                    case LoginResult.ErrorPassWord:
                        MessageBoxService.ShowMessage(Properties.Resources.Error_PassWordIsError, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        return;
                    default:
                        if (Session.Current == null)
                        {
                            throw new BusinessException(Properties.Resources.Error_UnableSession);
                        }
                        Messenger.Default.Send<LoginFormResult>(LoginFormResult.Success);
                        App.Current.MainWindow.Opacity = 100;
                        CurrentWindowService.Close();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }

        public void OnLoaded()
        {
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
            if (Session.Current != null)
            {
                IsReLogin = true;
                this.UserNo = Session.Current.SessionClientNo;
            }
            else
            {
                IsReLogin = false;
                this.UserNo = string.Empty;
            }
        }
        public void Closing(CancelEventArgs args)
        {
            if (Session.Current == null)
                Messenger.Default.Send<LoginFormResult>(LoginFormResult.Failed);
        }

        public void Cancel()
        {
            if (Session.Current != null)
            {
                var result = MessageBoxService.ShowMessage(Properties.Resources.Info_ConfirmToExit, Properties.Resources.Info_Title, MessageButton.YesNo, MessageIcon.Information);
                if (result == MessageResult.Yes)
                {
                    Session.Current = null;
                    CurrentWindowService.Close();
                }
            }
            else
                CurrentWindowService.Close();
        }
        #endregion
    }
    public enum LoginFormResult
    {
        Success,
        Failed
    }
}
