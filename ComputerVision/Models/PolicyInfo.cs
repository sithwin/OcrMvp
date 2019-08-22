using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVision.Models
{
    public class PolicyInfo
    {
        public string PolicyNumber { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public String Nationality { get; set; }
        public String DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string HomeNumber { get; set; }
        public string PlanName { get; set; }
        public string SumInsured { get; set; }
        public string TotalPremiumPayable { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentMethod { get; set; }
        public String InsuredSignedDate { get; set; }
        public String RepresentativeSignedDate { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativeCode { get; set; }
    }

    public class MappingElement
    {
        public string Field { get; set; }
        public string InitialPosition { get; set; }
        public string FinalPosition { get; set; }
        public int AdjustIndex { get; set; }
               
    }

    public class Mappings
    {
        public List<MappingElement> MappingElement { get; set; }
    }
}
