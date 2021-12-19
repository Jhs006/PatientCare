using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientCareBL.Models;
using PatientCareUIApp.Helpers;

namespace PatientCareUIApp.Forms
{
    public class AddUpdateCareRecordModel : PageModel
    {
        #region Private variables

        private APIHelper _apiHelper;
       
        #endregion

        public AddUpdateCareRecordModel(APIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #region Public Properties / enums

        [BindProperty]
        public CareRecord CareRecord { get; set; }

        /// <summary>
        /// Care Record Id
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }


        [BindProperty]
        public string PageErrorMessage { get; set; }

        [BindProperty]
        public PageModeEnum PageMode
        {
            get
            {
                if (Id > 0)
                { return PageModeEnum.Update; }
                else
                { return PageModeEnum.Add; }
            }
        }

        public enum PageModeEnum
        {
            Add,
            Update
        }

        #endregion

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            if (PageMode == PageModeEnum.Update)
            {
                // retrieve care record based on Id
                (bool success, string resultCode, CareRecord careRecord) response = await _apiHelper.GetCareRecord(Id);
                if (response.success)
                {
                    CareRecord = response.careRecord;
                }
                else
                {
                    this.PageErrorMessage = $"Unable to retrieve Patient Care record. Statuscode  - { response.resultCode }";
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool success = false;
            if (PageMode == PageModeEnum.Add)
            {
                // call API to add the new care record
                (bool success, string resultCode, int? newRecordId) response = await _apiHelper.CreateCareRecord(CareRecord);
                if (response.success)
                {
                    CareRecord.Id = response.newRecordId;
                    success = true;
                }
                else
                {
                    this.PageErrorMessage = $"Call to Add Patient Care record failed. Statuscode  - { response.resultCode }";
                }
            }
            else
            {
                // call API to update the care record
                // NOTE - hack to fix issue with Id being null on CareRecord 
                CareRecord.Id = this.Id;
                (bool success, string resultCode) response = await _apiHelper.UpdateCareRecord(CareRecord);
                if (response.success)
                {
                    success = true;
                }
                else
                {
                    this.PageErrorMessage = $"Call to Update Patient Care record failed. Statuscode  - { response.resultCode }";
                }
            }

            if (success)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                // stay on page and display errors
                return Page();
            }
        }

        #endregion
    }
}
