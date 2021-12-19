using PatientCareBL;
using PatientCareBL.Factories;
using PatientCareBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientCareAPI.Controllers
{
    public class CareRecordsController : ApiController
    {
        private CareRecordFactory careRecordFactory;
        public CareRecordFactory CareRecordFactory {
            get
            {
                if (careRecordFactory == null)
                {
                    careRecordFactory = new CareRecordFactory();
                }
                return careRecordFactory;
            }}


        // GET api/carerecords
        public IEnumerable<ICareRecord> Get()
        {
            return CareRecordFactory.GetAllCareRecords();
        }


        // GET api/carerecords/5
        public ICareRecord Get(int id)
        {
            return CareRecordFactory.GetCareRecord(id);
        }

        // POST api/carerecords
        public int? Post([FromBody] CareRecord record)
        {            
            int? newId = CareRecordFactory.AddCareRecord(record);
            return newId;
        }

        // PUT api/carerecords/5
        public bool Put(int id, [FromBody] CareRecord record)
        {
            record.Id = id;
            bool success = CareRecordFactory.UpdateCareRecord(record);
            return success;
        }


    }
}
