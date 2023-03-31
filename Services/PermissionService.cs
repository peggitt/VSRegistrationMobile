using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HSNP.Interfaces;

namespace HSNP.Services
{
    public class PermissionService 
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
