using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OcrMvp.Models
{
    public class PolicyInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        [DisplayName("Policy Number")]
        public string PolicyNumber { get; set; }

        [BsonElement]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [BsonElement]
        [DisplayName("NRIC / Passport / FIN")]
        public string IdNumber { get; set; }

        [BsonElement]
        [DisplayName("Nationality")]
        public string Nationality { get; set; }

        [BsonElement]
        [DisplayName("Date of Birth")]
        public String DateOfBirth { get; set; }

        [BsonElement]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [BsonElement]
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }

        [BsonElement]
        [DisplayName("Residental Address")]
        public string Address { get; set; }

        [BsonElement]
        [DisplayName("Contact Details")]
        public string Mobile { get; set; }

        [BsonElement]
        [DisplayName("Home Number")]
        public string HomeNumber { get; set; }

        [BsonElement]
        [DisplayName("Plan Name")]
        public string PlanName { get; set; }

        [BsonElement]
        [DisplayName("Sum Insured(S$)")]
        public decimal SumInsured { get; set; }

        [BsonElement]
        [DisplayName("Total Premium Payable (S$)")]
        public decimal TotalPremiumPayable { get; set; }

        [BsonElement]
        [DisplayName("Payment Mode")]
        public string PaymentMode { get; set; }

        [BsonElement]
        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }

        [BsonElement]
        [DisplayName("Date of Proposed Life Insured")]
        public String InsuredSignedDate { get; set; }

        [BsonElement]
        [DisplayName("Date of Representative")]
        public String RepresentativeSignedDate { get; set; }

        [BsonElement]
        [DisplayName("Rep Name")]
        public string RepresentativeName { get; set; }

        [BsonElement]
        [DisplayName("Rep Code")]
        public string RepresentativeCode { get; set; }
    }
}
