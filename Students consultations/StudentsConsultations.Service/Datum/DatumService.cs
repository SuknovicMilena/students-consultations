using StudentsConsultations.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Datum
{
    public class DatumService : IDatumService
    {
        private readonly IDatabaseManager _databaseManager;

        public DatumService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
    }
}
