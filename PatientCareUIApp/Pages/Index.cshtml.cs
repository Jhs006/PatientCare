using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PatientCareBL.Models;
using PatientCareUIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatientCareUIApp.Pages
{
    public class IndexModel : PageModel
    {
        #region Private variables 

        private readonly ILogger<IndexModel> _logger;
        private APIHelper _apiHelper;

        #endregion

        public IndexModel(ILogger<IndexModel> logger, APIHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;

        }

        #region Public properties

        [BindProperty]
        public string PageErrorMessage { get; set; }

        /// <summary>
        /// Care Record Id
        /// </summary>
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }

        private List<CareRecord> _careRecords = new List<CareRecord>();

        /// <summary>
        /// List of all Care Records
        /// </summary>
        [BindProperty]
        public List<CareRecord> CareRecords 
        { 
            get { return _careRecords; }
            set { _careRecords = value; } }

        #endregion

        #region Methods


        //public void OnGet()
        public async Task<IActionResult> OnGet()
        {
            // call API to get all care records
            (bool success, string resultCode, List<CareRecord> careRecords) response = await _apiHelper.GetCareRecords();
            if (response.success)
            {
                CareRecords = response.careRecords;
            }
            else
            {
                this.PageErrorMessage = $"Call to Get Patient Care records failed. Statuscode  - { response.resultCode }";
            }
            
            return Page();
        }


        #endregion

    }
}
