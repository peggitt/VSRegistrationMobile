using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HSNP.Interfaces
{
    public interface IPermissionService
    {
        /// <summary>
        /// Checks the network connectivity.
        /// </summary>
        /// <returns>The network connectivity status.</returns>
        NetworkAccess CheckNetworkConnectivity();
    }
}
