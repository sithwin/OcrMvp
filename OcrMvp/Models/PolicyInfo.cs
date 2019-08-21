using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcrMvp.Models
{
    public class PolicyInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string PolicyNumber { get; set; }

        [BsonElement]
        public string FullName { get; set; }

        [BsonElement]
        public string IdNumber { get; set; }

        [BsonElement]
        public string Nationality { get; set; }

        [BsonElement]
        public String DateOfBirth { get; set; }

        [BsonElement]
        public string Gender { get; set; }

        [BsonElement]
        public string MaritalStatus { get; set; }

        [BsonElement]
        public string Address { get; set; }

        [BsonElement]
        public string Mobile { get; set; }

        [BsonElement]
        public string HomeNumber { get; set; }

        [BsonElement]
        public string PlanName { get; set; }

        [BsonElement]
        public decimal SumInsured { get; set; }

        [BsonElement]
        public decimal TotalPremiumPayable { get; set; }

        [BsonElement]
        public string PaymentMode { get; set; }

        [BsonElement]
        public string PaymentMethod { get; set; }

        [BsonElement]
        public String InsuredSignedDate { get; set; }

        [BsonElement]
        public String RepresentativeSignedDate { get; set; }

        [BsonElement]
        public string RepresentativeName { get; set; }

        [BsonElement]
        public string RepresentativeCode { get; set; }
    }
}
