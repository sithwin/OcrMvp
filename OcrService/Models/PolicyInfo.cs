using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcrService.Models
{
    public class PolicyInfo
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string HomeNumber { get; set; }
        public string PlanName { get; set; }
        public decimal SumInsured { get; set; }
        public decimal TotalPremiumPayable { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime InsuredSignedDate { get; set; }
        public DateTime RepresentativeSignedDate { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativeCode { get; set; }
    }
}
