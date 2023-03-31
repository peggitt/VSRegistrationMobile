using System;
using System.Collections.Generic;
using System.Text;

namespace HSNP.Mobile.Interfaces
{
     public interface IToaster
    {
         Task SendToast(string message);
         Task SendToastAsync(string message);

        Task SendSnackbar(string message);
        Task SendSnackbarAsync(string message);
    }
}
