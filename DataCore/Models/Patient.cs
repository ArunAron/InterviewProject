using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public string PatientType { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Age { get; set; }
        public string AgeUnit { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }
    }
}
