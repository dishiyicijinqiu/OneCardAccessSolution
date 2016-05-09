using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : CrudNotificationObject<RegisterEditMessage, string>
    {

        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpLoadRegisterFileCommand { get; private set; }
        public RegisterViewModel(object ParentViewModel, RegisterEditMessage editmessage)
        {
            this.ParentViewModel = ParentViewModel;
            this.EditMessage = editmessage;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            UpLoadRegisterFileCommand = new DelegateCommand(UpLoadRegisterFile);
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
            Register_FileEntitys = new ObservableCollection<Register_FileEntity>(Entity.Register_FileEntitys);
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
        public ObservableCollection<Register_FileEntity> Register_FileEntitys { get; private set; }
        private void UpLoadRegisterFile()
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
                        //entity.SortNo = Entity.Register_FileEntitys.Count;
                        //Entity.Register_FileEntitys.Add(entity);
                        entity.SortNo = Register_FileEntitys.Count;
                        Register_FileEntitys.Add(entity);
                    }
                    ShowMessage(savename);
                }
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
        #endregion
    }
}
