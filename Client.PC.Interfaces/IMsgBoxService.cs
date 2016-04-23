using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IMsgBoxService
    {
        MsgResult Show(string messageText, string caption = null, MsgButton button = MsgButton.OK, MsgImage icon = MsgImage.None, MsgResult defaultResult = MsgResult.None);
        MsgResult Show(IView owner, string messageText, string caption = null, MsgButton button = MsgButton.OK, MsgImage icon = MsgImage.None, MsgResult defaultResult = MsgResult.None);
    }

    //
    // 摘要:
    //     指定显示在消息框上的按钮。用作 Overload:System.Windows.MessageBox.Show 方法的参数。
    public enum MsgButton
    {
        //
        // 摘要:
        //     消息框显示“确定”按钮。
        OK = 0,
        //
        // 摘要:
        //     消息框显示“确定”和“取消”按钮。
        OKCancel = 1,
        //
        // 摘要:
        //     消息框显示“是”、“否”和“取消”按钮。
        YesNoCancel = 3,
        //
        // 摘要:
        //     消息框显示“是”和“否”按钮。
        YesNo = 4
    }
    //
    // 摘要:
    //     指定用户单击的消息框按钮。System.Windows.MessageBoxResult 由 Overload:System.Windows.MessageBox.Show
    //     方法返回。
    public enum MsgResult
    {
        //
        // 摘要:
        //     消息框未返回值。
        None = 0,
        //
        // 摘要:
        //     消息框的结果值为“确定”。
        OK = 1,
        //
        // 摘要:
        //     消息框的结果值为“取消”。
        Cancel = 2,
        //
        // 摘要:
        //     消息框的结果值为“是”。
        Yes = 6,
        //
        // 摘要:
        //     消息框的结果值为“否”。
        No = 7
    }
    //
    // 摘要:
    //     指定消息框所显示的图标。
    public enum MsgImage
    {
        //
        // 摘要:
        //     不显示图标。
        None = 0,
        //
        // 摘要:
        //     消息框显示一个“手形”图标。
        Hand = 16,
        //
        // 摘要:
        //     消息框显示一个“停止”图标。
        Stop = 16,
        //
        // 摘要:
        //     消息框显示一个“错误”图标。
        Error = 16,
        //
        // 摘要:
        //     消息框显示一个“问号”图标。
        Question = 32,
        //
        // 摘要:
        //     消息框显示一个“感叹号”图标。
        Exclamation = 48,
        //
        // 摘要:
        //     消息框显示一个“警告”图标。
        Warning = 48,
        //
        // 摘要:
        //     消息框显示一个“星号”图标。
        Asterisk = 64,
        //
        // 摘要:
        //     消息框显示一个“信息”图标。
        Information = 64
    }
}
