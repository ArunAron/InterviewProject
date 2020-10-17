using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Material
    {
        public int ID { get; set; }
        public string  GUID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialType { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public int Status { get; set; }
    }
}
