using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Common;

namespace FengSharp.OneCardAccess.Core
{
    public class LoadingCallHandler : IRealProxyCallHandler
    {
        public void AfterInvoke()
        {
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
        }

        public void BeforeInvoke()
        {
            if (DXSplashScreen.IsActive)
                DXSplashScreen.Close();
            DXSplashScreen.Show<LoadingView>();
        }
    }
}
