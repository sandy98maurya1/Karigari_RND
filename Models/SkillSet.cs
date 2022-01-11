using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class SkillSet
    {
        [Key]
        public long OccupationId { get; set; }
        public long ProfileEmpId { get; set; }
        public List<JobSectors>  JobSector { get; set; }
        public List<JobType> AreaOfExpertise { get; set; } 
        public int Experience { get; set; } 
        public string LastCompanyWorked { get; set; }
        public string LastCompanyWorkYear { get; set; }
        public TypeOfWork TypeOfWorkLookingFor { get; set; } 
        public DateTime? NewJobStartDate { get; set; }
        public Education Qualification { get; set; }
        public bool IsReadyToWorkInOtherArea { get; set; }
    }
}
