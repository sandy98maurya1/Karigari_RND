using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    /// <summary>
    /// Contains Company Profile details
    /// </summary>
    public class Company
    {

        [Key]
        public long Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessOwner { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string BusinessEmail { get; set; }
        public string Password { get; set; }
        public string? BusinessContactNo { get; set; }
        public string? Fax { get; set; }
        public Address Address { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }

    }

    public class BusinessProfile
    {
        [Key]
        public int BusinessProfileId { get; set; }
        //public int BusinessId { get; set; }
        public List<int> TypesOfBussiness { get; set; }
        public string Function { get; set; }
       
    }


    public class CompanyJobDetails
    {
        public int CompanyJobID { get; set; }
        public string OpeningAtLocation { get; set; }
        public int NumberOfPositionOpen { get; set; }
        public int JobType { get; set; }
        public WorkerType TypeOfWorkerLookingFor { get; set; }
        public String OpeningForDepartments { get; set; }
        public int WorkDurationInDays { get; set; }
        public DateTime StartDateOfNewPosition { get; set; }
        public bool IsSpecificEducationRequire { get; set; }
        public Education SelectEducation { get; set; }
        public bool IsAccomodationProvided { get; set; }
        public String ParDayPayment { get; set; }//200-400

        public String JobDescriptions { get; set; }
    }

}
