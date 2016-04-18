using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using FengSharp.OneCardAccess.BusinessEntity;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.ComponentModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
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
        #region  methods
        public void Login()
        {
            try
            {
                IRBACService irbacservice = ServiceProxyFactory.Create<IRBACService>();
                LoginResult loginresult = irbacservice.Login(this.UserNo, this.Password);
                switch (loginresult)
                {
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
                        var ticket = ApplicationContext.Current.Ticket;
                        var session = ApplicationContext.Current["Session"];
                        Messenger.Default.Send<LoginFormResult>(LoginFormResult.Success);
                        CurrentWindowService.Close();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBoxService.ShowMessage(ex.Message);
                //Exception exceptionToRethrow;
                //if (ExceptionPolicy.HandleException(ex, "ErrorPolicy", out exceptionToRethrow))
                //{
                //    if (exceptionToRethrow != null)
                //        MessageBoxService.ShowMessage(exceptionToRethrow.Message);
                //    else
                //    {
                //        MessageBoxService.ShowMessage(ex.Message);
                //    }
                //}
                //else
                //{
                //    MessageBoxService.ShowMessage(ex.Message);
                //}
            }
        }

        public void OnLoaded()
        {
        }
        public void Closing(CancelEventArgs args)
        {
            if (ApplicationContext.Current.Count <= 0)
                Messenger.Default.Send<LoginFormResult>(LoginFormResult.Failed);
        }

        public void Cancel()
        {
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
