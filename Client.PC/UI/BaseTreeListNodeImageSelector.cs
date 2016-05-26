using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    public class BaseTreeListNodeImageSelector : TreeListNodeImageSelector
    {
        public override ImageSource Select(TreeListRowData rowData)
        {
            if (rowData == null)
                return base.Select(rowData);
            if (rowData.Node.HasChildren)
                return HasChildrenImage;
            return NoChildrenImage;
        }
        static ImageSource _HasChildrenImage;
        static ImageSource HasChildrenImage
        {
            get
            {
                if (_HasChildrenImage == null)
                {
                    _HasChildrenImage = new BitmapImage(new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/leaf_32x32.png", UriKind.Absolute));
                }
                return _HasChildrenImage;
            }
        }
        static ImageSource _NoChildrenImage;
        static ImageSource NoChildrenImage
        {
            get
            {
                if (_NoChildrenImage == null)
                {
                    _NoChildrenImage = new BitmapImage(new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/childleaf_32x32.png", UriKind.Absolute));
                }
                return _NoChildrenImage;
            }
        }
    }
}
