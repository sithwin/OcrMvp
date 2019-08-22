using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OcrMvp.Models
{
    public class PolicyInfoViewModel
    {
        [Display(Name = "Policy Number")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Id Number")]
        public string IdNumber { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Date Of Birth")]
        public String DateOfBirth { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        [Display(Name = "Home Number")]
        public string HomeNumber { get; set; }

        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Display(Name = "Sum Insured")]
        public decimal SumInsured { get; set; }

        [Display(Name = "Total Premium Payable")]
        public decimal TotalPremiumPayable { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Insured Signed Date")]
        public String InsuredSignedDate { get; set; }

        [Display(Name = "Representative Signed Date")]
        public String RepresentativeSignedDate { get; set; }

        [Display(Name = "Representative Name")]
        public string RepresentativeName { get; set; }

        [Display(Name = "Representative Code")]
        public string RepresentativeCode { get; set; }
    }
}
