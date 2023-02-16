using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HSNP.Interface
{
    public interface IServiceImei
    {
        string GetImei();

        string GetSerialNumber();

        string DeviceSoftwareVersion();

        string SimSerialNumber();


    }
}
