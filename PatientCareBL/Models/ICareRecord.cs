using System;

namespace PatientCareBL.Models
{
    public interface ICareRecord
    {
        string Action { get; set; }
        DateTime ActualStartDateTime { get; set; }
        bool Completed { get; set; }
        DateTime? EndDateTime { get; set; }
        int? Id { get; set; }
        string Outcome { get; set; }
        string PatientName { get; set; }
        string Reason { get; set; }
        DateTime TargetDateTime { get; set; }
        string Title { get; set; }
        string UserName { get; set; }
    }
}