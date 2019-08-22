using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OcrService.Data;
using OcrService.Models;

namespace OcrService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewBusinessController : ControllerBase
    {
        public readonly MongoContext context = new MongoContext();

        [HttpPost]
        public void Post([FromBody] PolicyInfoDto model)
        {
            var policyInfo = new PolicyInfo();
            policyInfo.PolicyNumber = model.PolicyNumber;
            policyInfo.FullName = model.FullName;
            policyInfo.IdNumber = model.IdNumber;
            policyInfo.Nationality = model.Nationality;
            policyInfo.DateOfBirth = model.DateOfBirth;
            policyInfo.Gender = model.Gender;
            policyInfo.MaritalStatus = model.MaritalStatus;
            policyInfo.Address = model.Address;
            policyInfo.Mobile = model.Mobile;
            policyInfo.HomeNumber = model.HomeNumber;
            policyInfo.PlanName = model.PlanName;
            policyInfo.SumInsured = model.SumInsured;
            policyInfo.TotalPremiumPayable = model.TotalPremiumPayable;
            policyInfo.PaymentMode = model.PaymentMode;
            policyInfo.PaymentMethod = model.PaymentMethod;
            policyInfo.InsuredSignedDate = model.InsuredSignedDate;
            policyInfo.RepresentativeSignedDate = model.RepresentativeSignedDate;
            policyInfo.RepresentativeName = model.RepresentativeName;
            policyInfo.RepresentativeCode = model.RepresentativeCode;
            context.PolicyInfo.InsertOne(policyInfo);
        }
    }
}