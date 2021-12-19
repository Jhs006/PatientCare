using PatientCareBL.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCareBL.Models
{
    public class CareRecord : ICareRecord
    {

        public int? Id { get; set; }

        [Required]
        [StringLength(450, ErrorMessage = "Title length can't be more than 450 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(450, ErrorMessage = "Patient Name length can't be more than 450 characters.")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [StringLength(450, ErrorMessage = "User Name length can't be more than 450 characters.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Actual Start Date Time")]
        public DateTime ActualStartDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Target Date Time")]
        public DateTime TargetDateTime { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Reason length can't be more than 1000 characters.")]
        public string Reason { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Action length can't be more than 1000 characters.")]
        public string Action { get; set; }

        [RequiredIfFieldChecked("EndDateTime", ErrorMessage = "End Date Time must be specified if Completed is ticked.")]
        public bool Completed { get; set; }

        [Display(Name = "End Date Time")]
        [DateMoreThan("ActualStartDateTime", ErrorMessage = "End Date Time must be later than Actual Start Date Time.")]
        public DateTime? EndDateTime { get; set; }

        [StringLength(1000, ErrorMessage = "Outcome can't be more than 1000 characters.")]
         public string Outcome { get; set; }

    }
}
