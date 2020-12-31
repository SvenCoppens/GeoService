using DataLaag;
using DataLaag.DataModel;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public class RiverRepository : IRiverRepository
    {
        private CountryContext Context;
        public RiverRepository(CountryContext context)
        {
            Context = context;
        }

        public River AddRiver(River river)
        {
            DataRiver data = DataModelConverter.ConvertRiverToRiverData(river);
            Context.Rivers.Add(data);
            Context.SaveChanges();
            return DataModelConverter.ConvertRiverDataToRiver(data);
        }

        public void DeleteRiver(int riverId)
        {
            DataRiver data = Context.Rivers.Find(riverId);
            Context.Rivers.Remove(data);
            Context.SaveChanges();
        }

        public River GetRiverForId(int id)
        {
            DataRiver river = Context.Rivers.Find(id);
            return DataModelConverter.ConvertRiverDataToRiver(river);

        }

        public River UpdateRiver(River river)
        {
            DataRiver data = DataModelConverter.ConvertRiverToRiverData(river);
            DataRiver original = Context.Rivers.Find(data.Id);
            original.CountryLink = data.CountryLink;
            original.Length = data.Length;
            original.Name = data.Name;
            Context.Rivers.Update(original);
            Context.SaveChanges();
            return DataModelConverter.ConvertRiverDataToRiver(original);
        }
    }
}
