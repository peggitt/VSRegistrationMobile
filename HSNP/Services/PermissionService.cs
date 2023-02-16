using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using HSNP.Interfaces;

namespace HSNP.Services
{
    public class PermissionService : IPermissionService
    {

        
        public PermissionService()
        {
        }

        public NetworkAccess CheckNetworkConnectivity()
        {
            var networkAccess = Connectivity.NetworkAccess;
            // returns the network status
            return networkAccess;
        }


    }
}
