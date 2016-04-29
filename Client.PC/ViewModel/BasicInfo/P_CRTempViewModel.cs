using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempViewModel : NotificationObject
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        public ICommand EditTempCommand { get; private set; }
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        P_CRTempEditMessage Parameter;
        object ParentViewModel;
        public P_CRTempViewModel(object ParentViewModel, P_CRTempEditMessage parameter)
        {
            watcher.Path = PCConfig.TempDir;
            watcher.Changed -= Watcher_Changed;
            watcher.Changed += Watcher_Changed;
            this.ParentViewModel = ParentViewModel;
            Parameter = parameter;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            CloseCommand = new DelegateCommand(Close);
            EditTempCommand = new DelegateCommand(EditTemp);
            DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().Subscribe(OnP_CRTempViewDataContextChange);
        }

        private void OnP_CRTempViewDataContextChange(object sender, P_CRTempViewDataContextChangeEventArgs args)
        {
            if (sender == this.ParentViewModel)
            {
                if (args == null || args.P_CRTempEditMessage == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_NoNext));
                    DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
                    return;
                }
                this.ParentViewModel = sender;
                this.Parameter = args.P_CRTempEditMessage;
                this.Init();
            }
        }

        public void Init()
        {
            try
            {
                Entity = P_CRTempEntity.CreateEntity();
                if (Parameter == null)
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
                switch (Parameter.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newEntity = P_CRTempEntity.CreateEntity();
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(Parameter.CopyKey);
                        Entity = P_CRTempEntity.CreateEntity();
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "P_CRTempId"
                        });
                        break;
                    case EntityEditMode.Edit:
                        {
                            var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(Parameter.Key);
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = P_CRTempEntity.CreateEntity();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().
             Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ischanged = true;
        }

        #region propertys
        private P_CRTempEntity _Entity;

        public P_CRTempEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        private string _RealTempPath;

        public string RealTempPath
        {
            get { return _RealTempPath; }
            set
            {
                _RealTempPath = value;
                RaisePropertyChanged("RealTempPath");
            }
        }
        #endregion
        #region methods
        public void SaveAndNew()
        {
            P_CRTempEditMessage paramsg = this.Parameter as P_CRTempEditMessage;
            paramsg.IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            this.SaveCore();
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        void SaveCore()
        {
            try
            {
                P_CRTempEditMessage paramsg = this.Parameter as P_CRTempEditMessage;
                paramsg.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveP_CRTempEntity(this.Entity);
                if (paramsg.Key <= 0)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Error_SaveFiled));
                    return;
                }
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SaveSuccess));
                DefaultEventAggregator.Current.GetEvent<P_CRTempViewEditedEvent<object>>().Publish(this.ParentViewModel, new P_CRTempViewEditedEventArgs(this.Parameter));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().
             Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        bool ischanged = false;
        public void EditTemp()
        {
            try
            {
                List<BusinessEntity.RBAC.UserInfoEntity> list = new List<BusinessEntity.RBAC.UserInfoEntity>();
                list.Add(new FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity()
                {
                    UserNo = "UserNo",
                    UserName = "UserName",
                    UserId = 1,
                    Remark = "Remark",
                    IsLock = false,
                    IsSuper = false,
                    PassWord = "PassWord"
                });
                list.Add(new FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity()
                {
                    UserNo = "UserNo1",
                    UserName = "UserName1",
                    UserId = 2,
                    Remark = "Remark1",
                    IsLock = true,
                    IsSuper = true,
                    PassWord = "PassWord1"
                });
                var report = new View.BasicInfo.P_CRTempReport();
                report.DataSource = list;
                if (string.IsNullOrWhiteSpace(this.Entity.CRTempPath))
                {
                    string newfile = Guid.NewGuid().ToString() + ".repx";
                    RealTempPath = Path.Combine(PCConfig.TempDir, newfile);
                    report.SaveLayout(RealTempPath);
                }
                else
                {
                    RealTempPath = TransferHelper.DownloadFileToTemp("P_CRTemp", this.Entity.CRTempPath);
                }
                watcher.Filter = Path.GetFileName(RealTempPath);
                watcher.EnableRaisingEvents = true;
                var form = new ReportTool.RibbonrEportDesignerForm(RealTempPath);
                form.Text = Properties.Resources.View_P_CRTempView_Title;
                form.ShowDialog();
                if (ischanged)
                {
                    this.Entity.CRTempPath = TransferHelper.UploadFile("P_CRTemp", RealTempPath, true);
                }
                watcher.EnableRaisingEvents = false;
                ischanged = false;
            }
            catch (System.Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        #endregion
    }
}




//var vm = this;
//                if (vm == null || string.IsNullOrWhiteSpace(vm.RealTempPath))
//                {




//                    System.Net.WebClient webclient = new System.Net.WebClient();
//webclient.Headers["key"] = PCConfig.UpLoadKey;
//                    webclient.Headers["FileType"] = "P_CRTemp";
//                    webclient.Headers["SaveName"] = newfile;
//                    byte[] responseArray = webclient.UploadFile("http://localhost/OneCardAccessServer/UploadHandler.ashx", "Post", newfilewithpath);
//string getPath = System.Text.Encoding.GetEncoding("UTF-8").GetString(responseArray);

//vm.RealTempPath = newfile;
//                }
//                {
//                    System.Net.WebClient webclient = new System.Net.WebClient();
//                    using (Stream stream = webclient.OpenRead("http://localhost/OneCardAccessServer/FileAttachMent/P_CRTemp/" + vm.RealTempPath))
//                    {
//                        report.LoadLayout(stream);
//                    }
//                }
