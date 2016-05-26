using DevExpress.Xpf.Grid.LookUp.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.Windows.Input;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Editors.Native;
using System.ComponentModel;
using System.Reflection;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    public class BaseGridControlVisualClientOwner : GridControlVisualClientOwner
    {
        new LookUpEdit Editor { get { return base.Editor as LookUpEdit; } }
        internal GridControl Grid { get { return InnerEditor as GridControl; } }
        public BaseGridControlVisualClientOwner(PopupBaseEdit editor) : base(editor)
        {
        }
        protected override void SubscribeEvents()
        {
            if (Grid == null)
                return;
            base.SubscribeEvents();
            var routedevent = ((System.Windows.RoutedEvent)(typeof(System.Windows.UIElement).GetField("MouseLeftButtonUpEvent").GetValue(Grid)));
            var p = typeof(System.Windows.UIElement).GetProperty("EventHandlersStore", BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var eventhandlersstore = p.GetValue(Grid, null);
            var method = eventhandlersstore.GetType().GetMethod("GetRoutedEventHandlers");
            System.Windows.RoutedEventHandlerInfo[] results = method.Invoke(eventhandlersstore, new object[] { routedevent }) as System.Windows.RoutedEventHandlerInfo[];
            foreach (var item in results)
            {
                if (item.Handler != null)
                    Grid.RemoveHandler(routedevent, item.Handler);
            }
            Grid.MouseLeftButtonUp += GridMouseUp;
        }
        private void GridMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
                return;
            int rowHandle = Grid.View.GetRowHandleByMouseEventArgs(e);
            if (IsDataRowRowHandle(rowHandle) && LookUpEditHelper.GetClosePopupOnMouseUp(Editor))
                Editor.ClosePopup();
        }
        bool IsDataRowRowHandle(int rowHandle)
        {
            if (Grid.IsGroupRowHandle(rowHandle))
                return false;
            return rowHandle != DataControlBase.InvalidRowHandle && rowHandle != DataControlBase.NewItemRowHandle &&
                   rowHandle != DataControlBase.AutoFilterRowHandle;
        }
        protected override void UnsubscribeEvents()
        {
            if (Grid == null)
                return;
            Grid.MouseLeftButtonUp -= GridMouseUp;
            base.UnsubscribeEvents();
        }
    }

    public class BaseLookUpEdit : LookUpEdit
    {
        protected override VisualClientOwner CreateVisualClient()
        {
            return new BaseGridControlVisualClientOwner(this);
        }
    }
}
