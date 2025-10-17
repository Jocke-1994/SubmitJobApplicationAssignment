using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitJobApplicationAssignment
{
    public enum ApplicationStatus { Applied, Interview, Offer, Rejected }
    public class JobApplication
    {
        //CompanyName | string
        public string CompanyName { get; set; }


        //PositionTitle | string
        public string PositionTitle { get; set; }


        //Status | enum - (Applied, Interview, Offer, Rejected)
        public ApplicationStatus Status { get; set; }


        //ApplicationDate | DateTime - Datum när ansökan skickades
        public DateTime ApplicationDate { get; set; }


        //ResponseDate | DateTime? - Datum när svar mottogs
        public DateTime? ResponseDate { get; set; }


        //SalaryExpectation | int - Önskad lön i kronor
        public int SalaryExpectation;

        public JobApplication(string companyName, string positionTitle, int salary, DateTime date, ApplicationStatus status = ApplicationStatus.Applied)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            SalaryExpectation = salary;
            ApplicationDate = date;
            Status = status; // standardvärde
        }

        public int GetDaysSinceApplied() //GetDaysSinceApplied() – returnerar antal dagar sedan ansökan skickades.
        {
            if (ApplicationDate == default) return -1; // eller kasta exception beroende på krav

            int days = (DateTime.Now.Date - ApplicationDate.Date).Days;
            return days < 0 ? 0 : days; //för att undvika ett negativt värde om man sätter ett datum i framtiden.
        }

        public string GetSummary() // returnerar en kort sammanfattning av ansökan.
        {
            return $"Jobbet du har sökt är hos {CompanyName}, som {PositionTitle}, just nu är status {Status}. Du sökte jobbet {ApplicationDate}";
        }
    }
}
