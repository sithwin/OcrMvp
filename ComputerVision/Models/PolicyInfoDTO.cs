using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVision.Models
{
    public class PolicyInfoDTO
    {
        public string PolicyNumber { get; set; }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value.Trim(); }
        }

        private string _idNumber;
        public string IdNumber
        {
            get { return _idNumber; }
            set { _idNumber = value.Replace(" ", "").Trim(); }
        }

        private string _nationality;
        public String Nationality
        {
            get { return _nationality; }
            set { _nationality = value.Trim(); }
        }

        private string _dateOfBirth;
        public String DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value.Trim().Replace(" ", ""); }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value.Trim(); }
        }

        private string _maritalStatus;
        public string MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value.Trim(); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value.Trim(); }
        }

        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value.Trim().Replace(" ", ""); }
        }

        private string _homeNumber;
        public string HomeNumber
        {
            get { return _homeNumber; }
            set { _homeNumber = value.Trim().Replace(" ", ""); }
        }

        private string _planName;
        public string PlanName
        {
            get { return _planName; }
            set { _planName = value.Trim(); }
        }

        private string _sumInsured;
        public string SumInsured
        {
            get { return _sumInsured; }
            set { _sumInsured = value.Trim(); }
        }

        private string _totalPremiumPayable;
        public string TotalPremiumPayable
        {
            get { return _totalPremiumPayable; }
            set { _totalPremiumPayable = value.Trim(); }
        }

        private string _paymentMode;
        public string PaymentMode
        {
            get { return _paymentMode; }
            set { _paymentMode = value.Trim().Replace(" ", ""); }
        }

        private string _paymentMethod;
        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value.Trim().Split(' ')[0]; }
        }

        private string _initialPremium;
        public string InitialPremium
        {
            get { return _initialPremium; }
            set { _initialPremium = value.Trim(); }
        }

        private string _insuredSignedDate;
        public String InsuredSignedDate
        {
            get { return _insuredSignedDate; }
            set { _insuredSignedDate = value.Trim().Replace(" ", ""); }
        }

        private string _representativeSignedDate;
        public String RepresentativeSignedDate
        {
            get { return _representativeSignedDate; }
            set { _representativeSignedDate = value.Trim().Replace(" ", ""); }
        }

        private string _representativeName;
        public string RepresentativeName
        {
            get { return _representativeName; }
            set { _representativeName = value.Trim(); }
        }

        private string _representativeCode;
        public string RepresentativeCode
        {
            get { return _representativeCode; }
            set { _representativeCode = value.Trim().Replace(" ", ""); }
        }
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
