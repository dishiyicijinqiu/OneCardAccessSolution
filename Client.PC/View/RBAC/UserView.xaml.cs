﻿using DevExpress.Xpf.Grid;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;
using System.Windows.Input;
using System;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Editors;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : BaseUserControl, IUserView
    {
        public UserView(UserViewModel VM) : base(VM)
        {
            InitializeComponent();
        }

        public UserView()
        {
            InitializeComponent();
        }

        private void lookupedit_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            var lookupedit = e.Source as LookUpEdit;
            if (lookupedit == null)
                return;
            var entity = lookupedit.SelectedItem as UserGroupEntity;
            if (entity == null)
                return;
            e.DisplayText = string.Format("{0}|{1}", entity.UserGroupNo, entity.UserGroupName);
            e.Handled = true;
        }
    }

    public class BaseSearchLookUpEditStyleSettings : SearchLookUpEditStyleSettings
    {
        //override GetClosePopupOnMouseUp
        protected override bool GetClosePopupOnMouseUp(LookUpEditBase editor)
        {
            var grid = DevExpress.Xpf.Core.Native.LayoutHelper.FindElementByName(DevExpress.Xpf.Editors.Native.LookUpEditHelper.GetPopupContentOwner(editor).Child, "PART_GridControl") as GridControl;
            var entity = grid.SelectedItem as UserGroupEntity;
            if (entity != null && entity.TreeSon > 0) return false;
            return base.GetClosePopupOnMouseUp(editor);
        }
    }
    //public class UserGroupNameConverter : MarkupExtension, IMultiValueConverter
    //{
    //    //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    //{
    //    //    var entity = value as UserGroupEntity;
    //    //    if (entity == null)
    //    //        return null;
    //    //    return string.Format("{0}|{1}", entity.UserGroupNo, entity.UserGroupName);
    //    //}

    //    //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        return this;
    //    }
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var entity = values[1] as UserGroupEntity;
    //        if (entity == null)
    //            return null;
    //        return string.Format("{0}|{1}", entity.UserGroupNo, entity.UserGroupName);
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
