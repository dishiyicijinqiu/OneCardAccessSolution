using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Ribbon;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : UserControl
    {
        public RegisterCollectionView()
        {
            InitializeComponent();
            DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().Subscribe(OnShowDocument);
            this.DataContext = new RegisterCollectionView();
        }

        private void OnShowDocument(MainViewModel sender, ShowDocumentEventArgs args)
        {
            if (sender == this.DataContext)
            {
                var docInfo = args.DocumentInfo;

                var doc = new DocumentPanel();
                doc.AllowDrag = false;
                doc.IsActive = true;
                doc.FloatOnDoubleClick = false;
                doc.Caption = docInfo.DocumentTitle;
                var type = System.Type.GetType(docInfo.DocumentType);
                doc.Content = Activator.CreateInstance(type);
                mdiContainer.Add(doc);
            }
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //WindowService windowService = new WindowService();
            //windowService.WindowStyle = Resources["DialogWindowStyle"] as Style;
            //windowService.Show(null, null);
            //this.grid.GetFocusedValue();
            //this.tableview.FocusedRowHandle;
            //this.tableview.SetCurrentValue();
            //this.tableview.FocusedRowData;
            //this.tableview.FocusedRow
        }
    }
}
