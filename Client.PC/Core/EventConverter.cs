using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
    public class MyEventArgsToDataRowConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs args)
        {
            //var argtest = args as IDataRowEventArgs;


            //object row = (args as IDataRowEventArgs).With(x => x.Row);
            //if (row != null)
            //    return row;
            var converter = EventArgsConverterHelper.GetSpecificConverter<IDataRowEventArgsConverter>(args);
            var routedArgs = args as RoutedEventArgs;
            //return (converter != null && routedArgs != null) ? converter.GetDataRow(routedArgs) : null;

            return null;
        }
    }
}
