using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
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
    public class P_CRTempViewModel : BaseNotificationObject, IP_CRTempViewModel
    {
        FileSystemWatcher watcher = new FileSystemWatcher();

        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand EditTempCommand { get; private set; }
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public P_CRTempViewModel(P_CRTempEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            watcher.Path = PCConfig.TempDir;
            watcher.Changed -= Watcher_Changed;
            watcher.Changed += Watcher_Changed;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            EditTempCommand = new DelegateCommand(EditTemp);
            Entity = FirstP_CRTempEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.CopyAdd:
                    var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.CopyKey);
                    Entity.CopyValueFrom(copyEntity,
                        new List<string>(PCConfig.CreateAndModifyInfoColNames)
                    {
                            "P_CRTempId"
                    });
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstP_CRTempEntity.CreateEntity();
            IsSee = EditMessage.EntityEditMode == EntityEditMode.See;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ischanged = true;
        }

        #region propertys
        bool _IsSee = false;
        public bool IsSee
        {
            get { return _IsSee; }
            set
            {
                _IsSee = value;
                RaisePropertyChanged("IsSee");
            }
        }
        private FirstP_CRTempEntity _Entity;

        public FirstP_CRTempEntity Entity
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
            (this.Parameter as P_CRTempEditMessage).IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            (this.Parameter as P_CRTempEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as P_CRTempEditMessage;
                para.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveP_CRTempEntity(this.Entity);
                if (para.Key == null || para.Key.Length != 36)
                {
                    ShowMessage(Properties.Resources.Error_SaveFiled);
                    return false;
                }
                ShowMessage(Properties.Resources.Info_SaveSuccess);
                OnEntityViewEdited?.Invoke(this, para);
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
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
                    UserId = Guid.NewGuid().ToString(),
                    Remark = "Remark",
                    IsLock = false,
                    PassWord = "PassWord"
                });
                list.Add(new FengSharp.OneCardAccess.BusinessEntity.RBAC.UserInfoEntity()
                {
                    UserNo = "UserNo1",
                    UserName = "UserName1",
                    UserId = Guid.NewGuid().ToString(),
                    Remark = "Remark1",
                    IsLock = true,
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
                    RealTempPath = TransferHelper.DownloadFile("P_CRTemp", this.Entity.CRTempPath);
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
                ShowException(ex);
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
