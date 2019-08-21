using OcrService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVision
{
    class JsonMapping : IOCRInterface
    {       
        ReadValues FullName;
        ReadValues IdNumber;
        ReadValues DateOfBirth;    
        ReadValues Gender;        
        ReadValues MaritalStatus;     
        ReadValues Address;        
        ReadValues Mobile;       
        ReadValues HomeNumber;      
        ReadValues PlanName;      
        ReadValues SumInsured;
        ReadValues TotalPremiumPayable;   
        ReadValues PaymentMode;        
        ReadValues PaymentMethod;        
        ReadValues InsuredSignedDate;       
        ReadValues RepresentativeSignedDate;       
        ReadValues RepresentativeName;       
        ReadValues RepresentativeCode;


        public virtual List<ReadValues> setData()
        {
            
            List<ReadValues> lstValues = new List<ReadValues>();
            FullName = new ReadValues();
            FullName.Field = "FullName";
            FullName.firstPosition = "1. Full Name :";
            FullName.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(FullName);
            
            IdNumber = new ReadValues();
            IdNumber.Field = "IdNumber";
            IdNumber.firstPosition = "1. Full Name :";
            IdNumber.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(IdNumber);

            DateOfBirth = new ReadValues();
            DateOfBirth.Field = "DateOfBirth";
            DateOfBirth.firstPosition = "1. Full Name :";
            DateOfBirth.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(DateOfBirth);

            Gender = new ReadValues();
            Gender.Field = "Gender";
            Gender.firstPosition = "1. Full Name :";
            Gender.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(Gender);

            MaritalStatus = new ReadValues();
            MaritalStatus.Field = "MaritalStatus";
            MaritalStatus.firstPosition = "1. Full Name :";
            MaritalStatus.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(MaritalStatus);

            Address = new ReadValues();
            Address.Field = "Address";
            Address.firstPosition = "1. Full Name :";
            Address.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(Address);

            Mobile = new ReadValues();
            Mobile.Field = "Mobile";
            Mobile.firstPosition = "1. Full Name :";
            Mobile.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(Mobile);

            HomeNumber = new ReadValues();
            IdNumber.Field = "IdNumber";
            HomeNumber.firstPosition = "1. Full Name :";
            HomeNumber.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(HomeNumber);

            PlanName = new ReadValues();
            PlanName.Field = "PlanName";
            PlanName.firstPosition = "1. Full Name :";
            PlanName.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(PlanName);

            SumInsured = new ReadValues();
            IdNumber.Field = "IdNumber";
            SumInsured.firstPosition = "1. Full Name :";
            SumInsured.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(SumInsured);

            TotalPremiumPayable = new ReadValues();
            TotalPremiumPayable.Field = "TotalPremiumPayable";
            TotalPremiumPayable.firstPosition = "1. Full Name :";
            TotalPremiumPayable.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(TotalPremiumPayable);

            PaymentMode = new ReadValues();
            PaymentMode.Field = "PaymentMode";
            PaymentMode.firstPosition = "1. Full Name :";
            PaymentMode.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(PaymentMode);

            PaymentMethod = new ReadValues();
            PaymentMethod.Field = "PaymentMethod";
            PaymentMethod.firstPosition = "1. Full Name :";
            PaymentMethod.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(PaymentMethod);

            InsuredSignedDate = new ReadValues();
            InsuredSignedDate.Field = "InsuredSignedDate";
            InsuredSignedDate.firstPosition = "1. Full Name :";
            InsuredSignedDate.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(InsuredSignedDate);

            RepresentativeSignedDate = new ReadValues();
            RepresentativeSignedDate.Field = "RepresentativeSignedDate";
            RepresentativeSignedDate.firstPosition = "1. Full Name :";
            RepresentativeSignedDate.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(RepresentativeSignedDate);

            RepresentativeSignedDate = new ReadValues();
            RepresentativeSignedDate.Field = "RepresentativeSignedDate";
            RepresentativeSignedDate.firstPosition = "1. Full Name :";
            RepresentativeSignedDate.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(RepresentativeSignedDate);

            RepresentativeName = new ReadValues();
            RepresentativeName.Field = "RepresentativeName";
            RepresentativeName.firstPosition = "1. Full Name :";
            RepresentativeName.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(RepresentativeName);

            RepresentativeCode = new ReadValues();
            RepresentativeCode.Field = "RepresentativeCode";
            RepresentativeCode.firstPosition = "1. Full Name :";
            RepresentativeCode.lastPosition = "2. NRIC / Passport / FIN :";
            lstValues.Add(RepresentativeCode);

            return lstValues;
        }
    }

   
}
