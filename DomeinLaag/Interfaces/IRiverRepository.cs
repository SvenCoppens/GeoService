using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public interface IRiverRepository
    {
        River AddRiver(River river);
        River GetRiverForId(int id);
        River UpdateRiver(River river);
        void DeleteRiver(int riverId);
    }
}
