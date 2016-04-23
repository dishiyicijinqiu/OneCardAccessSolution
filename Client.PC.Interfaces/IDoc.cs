using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    //
    // 摘要:
    //     Provides methods to work with a document created with the IDocumentManagerService.
    public interface IDoc
    {
        //
        // 摘要:
        //     Specifies the document content.
        object Content { get; }
        //
        // 摘要:
        //     Specifies whether to release the memory reserved for the document after it is
        //     closed.
        bool DestroyOnClose { get; set; }
        //
        // 摘要:
        //     Specifies the document ID.
        object Id { get; set; }
        //
        // 摘要:
        //     Specifies the document title.
        object Title { get; set; }

        //
        // 摘要:
        //     Closes the document.
        //
        // 参数:
        //   force:
        //     true, to disable the confirmation logic; otherwise, false.
        void Close(bool force = true);
        //
        // 摘要:
        //     Hides the document.
        void Hide();
        //
        // 摘要:
        //     Shows the document.
        void Show();
    }
}
