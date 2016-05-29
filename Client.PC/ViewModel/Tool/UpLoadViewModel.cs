using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using Microsoft.Practices.Prism.Commands;
using System.Collections;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.Tool
{
    public class UpLoadViewModel : BaseNotificationObject, IUpLoadViewModel
    {
        object lockobj = new object();
        public ICommand PauseContinueCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpLoadBackGroundCommand { get; private set; }
        public UpLoadViewModel()
        {
            PauseContinueCommand = new DelegateCommand(PauseContinue, CanPauseContinue);
            StopCommand = new DelegateCommand(Stop, CanStop);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            UpLoadBackGroundCommand = new DelegateCommand(UpLoadBackGround, CanUpLoadBackGround);
        }
        private bool CanPauseContinue()
        {
            return queue.Count > 0;
        }
        private void PauseContinue()
        {
            lock (lockobj)
            {
                switch (UpLoadState)
                {
                    case UpLoadState.Running:
                        UpLoadState = UpLoadState.Pause;
                        break;
                    case UpLoadState.Pause:
                        UpLoadState = UpLoadState.Running;
                        break;
                    case UpLoadState.Stoped:
                        StartUpLoad();
                        break;
                }
            }
        }
        private bool CanStop()
        {
            if (queue.Count <= 0) return false;
            return UpLoadState != UpLoadState.Stoped;
        }
        private void Stop()
        {
            lock (lockobj)
            {
                UpLoadState = UpLoadState.Stoped;
            }
        }

        private bool CanDelete(IList list)
        {
            if (UpLoadState != UpLoadState.Stoped)
                return false;
            if (list == null)
                return false;
            return list.Count > 0;
        }
        private void Delete(IList list)
        {
            try
            {
                if (list == null || list.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var entity = list[i] as UpLoadAttachmentInfoEntity;
                    if (entity != null)
                    {
                        UpLoadAttachmentInfoItems.Remove(entity);
                    }
                }
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private bool CanUpLoadBackGround()
        {
            return UpLoadState == UpLoadState.Running;
        }

        private void UpLoadBackGround()
        {
            base.Close();
        }
        public override void Close()
        {
            UpLoadState = UpLoadState.Stoped;
            base.Close();
        }

        ObservableCollection<UpLoadAttachmentInfoEntity> _UpLoadAttachmentInfoItems = new ObservableCollection<UpLoadAttachmentInfoEntity>();
        public ObservableCollection<UpLoadAttachmentInfoEntity> UpLoadAttachmentInfoItems
        {
            get { return _UpLoadAttachmentInfoItems; }
            set
            {
                _UpLoadAttachmentInfoItems = value;
                RaisePropertyChanged("UpLoadAttachmentInfoItems");
            }
        }

        Queue<UpLoadAttachmentInfoEntity> queue = new Queue<UpLoadAttachmentInfoEntity>();
        private UpLoadState _UpLoadState = UpLoadState.Stoped;
        public UpLoadState UpLoadState
        {
            get { return _UpLoadState; }
            set
            {
                _UpLoadState = value;
                RaisePropertyChanged("UpLoadState");
                (StopCommand as DelegateCommand).RaiseCanExecuteChanged();
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
                (UpLoadBackGroundCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        public void AddUpLoadItem(IEnumerable<UpLoadAttachmentInfoEntity> uploaditems)
        {
            lock (lockobj)
            {
                foreach (var item in uploaditems)
                {
                    UpLoadAttachmentInfoItems.Add(item);
                }
            }
        }
        public void StartUpLoad()
        {
            try
            {
                lock (lockobj)
                {
                    if (UpLoadState == UpLoadState.Running)
                        return;
                    else if (UpLoadState == UpLoadState.Pause)
                    {
                        UpLoadState = UpLoadState.Pause;
                    }
                    queue.Clear();
                    foreach (var item in UpLoadAttachmentInfoItems)
                    {
                        if (string.IsNullOrWhiteSpace(item.Message))
                            queue.Enqueue(item);
                    }
                    UpLoadState = UpLoadState.Running;
                    Thread thread = new Thread(Run);
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        void Run()
        {
            while (true)
            {
                Thread.Sleep(2000);
                UpLoadAttachmentInfoEntity nextEntity = null;
                try
                {
                    lock (lockobj)
                    {
                        if (UpLoadState == UpLoadState.Stoped)
                            break;
                        else if (UpLoadState == UpLoadState.Pause)
                        {
                            continue;
                        }
                        else
                        {
                            if (queue.Count > 0)
                            {
                                nextEntity = queue.Dequeue();
                                (PauseContinueCommand as DelegateCommand).RaiseCanExecuteChanged();
                                (StopCommand as DelegateCommand).RaiseCanExecuteChanged();
                                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
                            }
                            if (nextEntity == null)
                                UpLoadState = UpLoadState.Stoped;
                        }
                    }
                    if (nextEntity != null)
                    {
                        TransferHelper.UploadFile(nextEntity.AttachmentPath, nextEntity.LocalPath, false);
                        nextEntity.Message = Properties.Resources.Info_UploadSuccess;
                    }
                }
                catch (Exception ex)
                {
                    if (nextEntity != null)
                    {
                        nextEntity.Message = ex.Message;
                    }
                }
                finally
                {
                    if (queue.Count <= 0)
                    {
                        lock (lockobj)
                        {
                            if (queue.Count <= 0)
                            {
                                UpLoadState = UpLoadState.Stoped;
                            }
                        }
                    }
                }
            }
        }
        private UpLoadAttachmentInfoEntity _SelectedEntity;
        public UpLoadAttachmentInfoEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
            }
        }
    }
    public enum UpLoadState
    {
        Stoped,
        Running,
        Pause
    }
}
