using PatientCareBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientCareBL.Factories
{
    public class CareRecordFactory
    {

        public CareRecordFactory()
        {
            InitializeMockData();
        }

        #region Private Properties

        private List<ICareRecord> CareRecords { get; set; }

        #endregion

        #region Private Methods

        private void InitializeMockData()
        {
            // initialize mock records
            this.CareRecords = new List<ICareRecord> {
                                    new CareRecord{ Id = 1,
                                                    Title = "Mr",
                                                    PatientName = "Donald Duck",
                                                    UserName = "dduck",
                                                    ActualStartDateTime = new DateTime(2021, 1, 1),
                                                    TargetDateTime = new DateTime(2021, 1, 15),
                                                    Reason = "Bumped his head and needs a checkup.",
                                                    Action =  "Perform a scan",
                                                    Completed =  false,
                                                    EndDateTime = null },
                                    new CareRecord{ Id = 2,
                                                    Title = "Miss",
                                                    PatientName = "Minnie Mouse",
                                                    UserName = "minnie13",
                                                    ActualStartDateTime = new DateTime(2021, 5, 16),
                                                    TargetDateTime = new DateTime(2021, 4, 14),
                                                    Reason = "Feeling nauseous",
                                                    Action =  "Keep under observation.",
                                                    Completed =  true,
                                                    EndDateTime = new DateTime(2021, 5, 28),
                                                    Outcome = "All fine."},
                                    new CareRecord {Id = 3,
                                                    Title = "Mr",
                                                    PatientName = "Mickey Mouse",
                                                    UserName = "mmouse2021",
                                                    ActualStartDateTime = new DateTime(2021, 10, 1),
                                                    TargetDateTime = new DateTime(2021, 10, 3),
                                                    Reason = "Fell off his bike.",
                                                    Action = "Complete X-rays and take bloods.",
                                                    Completed = true,
                                                    EndDateTime = new DateTime(2021, 12, 25),
                                                    Outcome = "No issues found. Patient discharged with a follow up from GP in 2 weeks."}
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This should call a sproc to retrieve all care records.
        /// But for now we return the mock data.
        /// </summary>
        /// <returns></returns>
        public List<ICareRecord> GetAllCareRecords()
        {            
            return this.CareRecords;
        }

        /// <summary>
        /// This should call a sproc to retrieve a care record based on id.
        /// But for now we return a record from the mock data.
        /// </summary>
        public ICareRecord GetCareRecord(int id)
        {
            return this.CareRecords.Where(c => ((int)c.Id == id)).FirstOrDefault();
        }

        /// <summary>
        /// This should call a sproc to add a new Care Record into the database.
        /// But for now we add a record to the mock data.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int? AddCareRecord(ICareRecord record)
        {
            if (record != null)
            {
                // set the new record ID 
                int? lastRecordId = this.CareRecords.Select(c => c.Id).Max();
                int newRecordId = ((int)lastRecordId) + 1;

                // add record
                record.Id = newRecordId;
                this.CareRecords.Add(record);
                return newRecordId;
            }
            return null;
        }

        /// <summary>
        /// This should call a sproc to update a Care Record in the database.
        /// But for now we update the record in our mock data.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateCareRecord(ICareRecord record)
        {
            if (record != null)
            {
                ICareRecord recToUpdate = this.CareRecords.Where(c => c.Id == record.Id).FirstOrDefault();
                var index = CareRecords.IndexOf(recToUpdate);
                if (index != -1)
                {
                    CareRecords[index] = record;
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
