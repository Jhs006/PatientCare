using Microsoft.Extensions.Configuration;
using PatientCareBL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatientCareUIApp.Helpers
{
    public class APIHelper
    {
        private IConfiguration _config;
        private const string PATIENT_CARE_API_BASE_URL = "PatientCareAPIBaseUrl";
        private const string PATIENT_CARE_API = "api/carerecords/";


        public APIHelper(IConfiguration config)
        {
            _config = config;
        }

        #region Methods

        /// <summary>
        /// Call API to add the new care record
        /// </summary>
        /// <param name="careRecord"></param>
        /// <returns></returns>
        public async Task<(bool success, string resultCode, int? newRecordId)> CreateCareRecord(CareRecord careRecord)
        {
       
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_config.GetValue<string>(PATIENT_CARE_API_BASE_URL));
                var json = JsonSerializer.Serialize(careRecord);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(PATIENT_CARE_API, data);
                var strRecordID = await response.Content.ReadAsStringAsync();

                int? newRecordID = null ;
                bool success = false;
                string resultCode = response.StatusCode.ToString(); 
                if (response.IsSuccessStatusCode)
                {
                    newRecordID =  Int32.Parse(strRecordID);
                    success = true;
                }
                
                (bool success, string resultCode, int? newRecordId) result = (success, resultCode, newRecordID);
                
                return result;
            }

        }

        /// <summary>
        /// Call API to update the care record
        /// </summary>
        /// <param name="careRecord"></param>
        /// <returns></returns>
        public async Task<(bool success, string resultCode)> UpdateCareRecord(CareRecord careRecord)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_config.GetValue<string>(PATIENT_CARE_API_BASE_URL));
                var json = JsonSerializer.Serialize(careRecord);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(PATIENT_CARE_API + careRecord.Id, data);
               
                var success = false;
                string resultCode = response.StatusCode.ToString();
                if (response.IsSuccessStatusCode)
                {
                    success = true;
                }
                (bool success, string resultCode) result = (success, resultCode);

                return result;
            }
        }


        /// <summary>
        /// Call API to retrieve a care record by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(bool success, string resultCode, CareRecord careRecord)> GetCareRecord(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_config.GetValue<string>(PATIENT_CARE_API_BASE_URL));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(PATIENT_CARE_API + id).Result;

                var success = false;
                string resultCode = response.StatusCode.ToString();
                CareRecord careRecord = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    careRecord = JsonSerializer.Deserialize<CareRecord>(json);
                    success = true;
                }
               
                (bool success, string resultCode, CareRecord careRecord) result = (success, resultCode, careRecord);

                return result;
            }
        }

        public async Task<(bool success, string resultCode, List<CareRecord> careRecords)> GetCareRecords()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_config.GetValue<string>(PATIENT_CARE_API_BASE_URL));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(PATIENT_CARE_API).Result;

                var success = false;
                string resultCode = response.StatusCode.ToString();
                List<CareRecord> records = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    records = JsonSerializer.Deserialize<List<CareRecord>>(json);
                    success = true;
                }
                (bool success, string resultCode, List<CareRecord> careRecords) result = (success, resultCode, records);
                return result;
            }
        }

        #endregion

    }
}
