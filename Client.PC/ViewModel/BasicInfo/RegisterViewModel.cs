using FengSharp.OneCardAccess.BusinessEntity;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : CrudNotificationObject<RegisterEditMessage, string>
    {

        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpLoadRegisterFileCommand { get; private set; }
        public ICommand ViewRegisterFileCommand { get; private set; }
        public RegisterViewModel(object ParentViewModel, RegisterEditMessage editmessage)
        {
            this.ParentViewModel = ParentViewModel;
            this.EditMessage = editmessage;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            UpLoadRegisterFileCommand = new DelegateCommand(UpLoadRegisterFile);
            ViewRegisterFileCommand = new DelegateCommand<Register_FileEntity>(ViewRegisterFile, CanViewRegisterFile);
             
            Entity = SecondRegisterEntity.CreateEntity();
            if (this.EditMessage == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            switch (this.EditMessage.EntityEditMode)
            {
                case EntityEditMode.Add:
                    {
                        var newEntity = SecondRegisterEntity.CreateEntity();
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
                case EntityEditMode.CopyAdd:
                    var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(this.EditMessage.CopyKey);
                    Entity = SecondRegisterEntity.CreateEntity();
                    Entity.CopyValueFrom(copyEntity,
                        new List<string>(PCConfig.CreateAndModifyInfoColNames)
                    {
                            "RegisterId","Register_FileEntitys"
                    });
                    break;
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(this.EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
                default:
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
            }
            if (Entity == null)
                Entity = SecondRegisterEntity.CreateEntity();
        }

        private bool CanViewRegisterFile(Register_FileEntity fileentity)
        {
            return fileentity != null;
        }
        #region propertys
        private SecondRegisterEntity _Entity;

        public SecondRegisterEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        #endregion

        private void ViewRegisterFile(Register_FileEntity fileentity)
        {
            try
            {
                string filename = TransferHelper.DownloadFile(Transfer_FileType.Register_File.ToString(), fileentity.FilePath);
                if (!File.Exists(filename))
                {
                    throw new Exception(Properties.Resources.Info_FileNotFound);
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void UpLoadRegisterFile()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "pdf 文件 (*.pdf)|*.pdf";
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var filenames = openFileDialog1.FileNames;
                    foreach (var filename in filenames)
                    {
                        string savename = TransferHelper.UploadFile("Register_File", filename, true);
                        {
                            var entity = new Register_FileEntity();
                            entity.FileName = System.IO.Path.GetFileName(filename);
                            entity.FilePath = savename;
                            entity.SortNo = Entity.Register_FileEntitys.Count;
                            Entity.Register_FileEntitys.Add(entity);
                        }
                        ShowMessage(savename);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public void SaveAndNew()
        {
            this.EditMessage.IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            this.EditMessage.IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        bool SaveCore()
        {
            try
            {
                this.EditMessage.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveRegisterEntity(this.Entity);
                if (this.EditMessage.Key == null || this.EditMessage.Key.Length != 36)
                {
                    ShowMessage(Properties.Resources.Error_SaveFiled);
                    return false;
                }
                ShowMessage(Properties.Resources.Info_SaveSuccess);
                EntityEdited();
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }
    }
}
