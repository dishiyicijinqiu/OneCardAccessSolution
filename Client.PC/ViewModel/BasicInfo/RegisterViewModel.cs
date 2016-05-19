using FengSharp.OneCardAccess.BusinessEntity;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : BaseNotificationObject, IRegisterViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        #region commands
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpLoadRegisterFileCommand { get; private set; }
        public ICommand MoveUpRegisterFileCommand { get; private set; }
        public ICommand MoveDownRegisterFileCommand { get; private set; }
        public ICommand DeleteRegisterFileCommand { get; private set; }
        public ICommand ViewRegisterFileCommand { get; private set; }
        public ICommand AddRegisterTempCommand { get; private set; }
        public ICommand MoveUpRegisterTempCommand { get; private set; }
        public ICommand MoveDownRegisterTempCommand { get; private set; }
        public ICommand DeleteRegisterTempCommand { get; private set; }
        public ICommand ViewP_CRTempCommand { get; private set; }
        #endregion
        public RegisterViewModel(RegisterEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            UpLoadRegisterFileCommand = new DelegateCommand(UpLoadRegisterFile);
            MoveUpRegisterFileCommand = new DelegateCommand<Register_FileEntity>(MoveUpRegisterFile);
            MoveDownRegisterFileCommand = new DelegateCommand<Register_FileEntity>(MoveDownRegisterFile);
            DeleteRegisterFileCommand = new DelegateCommand<IList>(DeleteRegisterFile);
            ViewRegisterFileCommand = new DelegateCommand<Register_FileEntity>(ViewRegisterFile);

            AddRegisterTempCommand = new DelegateCommand(AddRegisterTemp);
            MoveUpRegisterTempCommand = new DelegateCommand<FirstP_CRTemp_To_RegisterEntity>(MoveUpRegisterTemp);
            MoveDownRegisterTempCommand = new DelegateCommand<FirstP_CRTemp_To_RegisterEntity>(MoveDownRegisterTemp);
            DeleteRegisterTempCommand = new DelegateCommand<IList>(DeleteRegisterTemp);
            ViewP_CRTempCommand = new DelegateCommand<FirstP_CRTemp_To_RegisterEntity>(ViewP_CRTemp);



            Entity = SecondRegisterEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.Add:
                    break;
                case EntityEditMode.CopyAdd:
                    var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(EditMessage.CopyKey);
                    Entity = SecondRegisterEntity.CreateEntity();
                    Entity.CopyValueFrom(copyEntity,
                        new List<string>(PCConfig.CreateAndModifyInfoColNames)
                    {
                            "RegisterId","Register_FileEntitys","P_CRTemp_To_RegisterEntitys"
                    });
                    break;
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(EditMessage.Key);
                        newEntity.Register_FileEntitys = new ObservableCollection<Register_FileEntity>(newEntity.Register_FileEntitys.OrderBy(t => t.SortNo));
                        newEntity.P_CRTemp_To_RegisterEntitys = new ObservableCollection<FirstP_CRTemp_To_RegisterEntity>(newEntity.P_CRTemp_To_RegisterEntitys.OrderBy(t => t.SortNo));
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = SecondRegisterEntity.CreateEntity();
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
        #region commandmethods
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
                    }
                    ShowMessage(Properties.Resources.Info_UploadSuccess);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void MoveUpRegisterTemp(FirstP_CRTemp_To_RegisterEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                int oldindex = this.Entity.P_CRTemp_To_RegisterEntitys.IndexOf(entity);
                int newindex = oldindex - 1;
                if (newindex < 0)
                    return;
                this.Entity.P_CRTemp_To_RegisterEntitys.Move(oldindex, newindex);
                for (int i = 0; i <= this.Entity.P_CRTemp_To_RegisterEntitys.Count - 1; i++)
                {
                    this.Entity.P_CRTemp_To_RegisterEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void MoveDownRegisterTemp(FirstP_CRTemp_To_RegisterEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                int count = this.Entity.P_CRTemp_To_RegisterEntitys.Count;
                int oldindex = this.Entity.P_CRTemp_To_RegisterEntitys.IndexOf(entity);
                int newindex = oldindex + 1;
                if (newindex >= count)
                    return;
                this.Entity.P_CRTemp_To_RegisterEntitys.Move(oldindex, newindex);
                for (int i = 0; i <= this.Entity.P_CRTemp_To_RegisterEntitys.Count - 1; i++)
                {
                    this.Entity.P_CRTemp_To_RegisterEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void DeleteRegisterTemp(IList entitys)
        {
            try
            {
                if (entitys == null || entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Entity.P_CRTemp_To_RegisterEntitys.Remove(entitys[i] as FirstP_CRTemp_To_RegisterEntity);
                }
                for (int i = 0; i <= this.Entity.P_CRTemp_To_RegisterEntitys.Count - 1; i++)
                {
                    this.Entity.P_CRTemp_To_RegisterEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
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

        private void AddRegisterTemp()
        {
            try
            {
                var vm = ServiceLoader.LoadService<IP_CRTempCollectionSelectViewModel>("IP_CRTempCollectionSelectViewModel",
                    new ParameterOverride("style", ViewStyle.MulSelect));
                vm.OnSelectedItems += Vm_OnSelectedItems;
                var view = ServiceLoader.LoadService<IP_CRTempCollectionView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Vm_OnSelectedItems(List<P_CRTempEntity> SelectItems)
        {
            try
            {
                if (SelectItems == null)
                    return;
                foreach (var item in SelectItems)
                {
                    if (Entity.P_CRTemp_To_RegisterEntitys.FirstOrDefault(t => t.P_CRTempId == item.P_CRTempId) == null)
                    {
                        var newitem = new FirstP_CRTemp_To_RegisterEntity();
                        newitem.CRTempName = item.CRTempName;
                        newitem.CRTempPath = item.CRTempPath;
                        newitem.P_CRTempId = item.P_CRTempId;
                        newitem.RegisterId = this.Entity.RegisterId;
                        newitem.Remark = string.Empty;
                        Entity.P_CRTemp_To_RegisterEntitys.Add(newitem);
                    }
                }
                for (int i = 0; i < Entity.P_CRTemp_To_RegisterEntitys.Count; i++)
                {
                    var entity = Entity.P_CRTemp_To_RegisterEntitys[i];
                    entity.SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void MoveUpRegisterFile(Register_FileEntity entity)
        {
            try
            {

                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                int oldindex = this.Entity.Register_FileEntitys.IndexOf(entity);
                int newindex = oldindex - 1;
                if (newindex < 0)
                    return;
                this.Entity.Register_FileEntitys.Move(oldindex, newindex);
                for (int i = 0; i <= this.Entity.Register_FileEntitys.Count - 1; i++)
                {
                    this.Entity.Register_FileEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void MoveDownRegisterFile(Register_FileEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                int count = this.Entity.Register_FileEntitys.Count;
                int oldindex = this.Entity.Register_FileEntitys.IndexOf(entity);
                int newindex = oldindex + 1;
                if (newindex >= count)
                    return;
                this.Entity.Register_FileEntitys.Move(oldindex, newindex);
                for (int i = 0; i <= this.Entity.Register_FileEntitys.Count - 1; i++)
                {
                    this.Entity.Register_FileEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void DeleteRegisterFile(IList entitys)
        {
            try
            {
                if (entitys == null || entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Entity.Register_FileEntitys.Remove(entitys[i] as Register_FileEntity);
                }
                for (int i = 0; i <= this.Entity.Register_FileEntitys.Count - 1; i++)
                {
                    this.Entity.Register_FileEntitys[i].SortNo = i;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void ViewP_CRTemp(FirstP_CRTemp_To_RegisterEntity entity)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IP_CRTempViewModel>(new ParameterOverride("EditMessage", new P_CRTempEditMessage(entity.P_CRTempId, EntityEditMode.See)));
                var view = ServiceLoader.LoadService<IP_CRTempView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public void SaveAndNew()
        {
            (this.Parameter as RegisterEditMessage).IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            (this.Parameter as RegisterEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        #endregion
        #region methods
        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as RegisterEditMessage;
                para.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveRegisterEntity(this.Entity);
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
        #endregion
    }
}
