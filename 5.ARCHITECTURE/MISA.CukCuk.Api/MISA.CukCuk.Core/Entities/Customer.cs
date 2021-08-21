using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Customer : Person
    {
        #region Field

        private Guid customerId;
        private string customerCode;
        private Guid? customerGroupId;
        private double? debitAmount;
        private string memberCardCode;
        private string companyName;
        private string companyTaxCode;
        private int? isStopFollow;
        private string customerGroupName;

        #endregion

        #region Constructor

        public Customer()
        {
            this.customerId = new Guid();
        }

        //constructor không nhận prop của base
        //chỉ các tt bắt buộc
        public Customer(string customerCode, string fullName, string email, string phoneNumber) : base(fullName, email, phoneNumber)
        {
            this.customerId = new Guid();
            this.customerCode = customerCode;
        }

        // tất cả các thuộc tính
        public Customer(string customerCode, string firstName, string lastName, string fullName, int? gender, string address,
            DateTime? dateOfBirth, string email, string phoneNumber, Guid? customerGroupId, double? debitAmount,
            string memberCardCode, string companyName, string companyTaxCode, int? isStopFollow)
            : base(firstName, lastName, fullName, gender, dateOfBirth, address, email, phoneNumber)
        {
            this.customerId = new Guid();
            this.customerCode = customerCode;
            this.customerGroupId = customerGroupId;
            this.memberCardCode = memberCardCode;
            this.companyName = companyName;
            this.companyTaxCode = companyTaxCode;
            this.isStopFollow = isStopFollow;
        }

        //constructor nhận cả prop của base
        // dài quá viết sau
        #endregion

        #region Property

        [PrimaryKey]
        public Guid CustomerId
        {
            get { return this.customerId; }
            set { this.customerId = value; }
        }

        [Requied]
        [NotAllowDuplicate]
        public string CustomerCode
        {
            get { return this.customerCode; }
            set { this.customerCode = value; }
        }

        public Guid? CustomerGroupId
        {
            get { return this.customerGroupId; }
            set { this.customerGroupId = value; }
        }

        public double? DebitAmount
        {
            get { return this.debitAmount; }
            set { this.debitAmount = value; }
        }

        public string MemberCardCode
        {
            get { return this.memberCardCode; }
            set { this.memberCardCode = value; }
        }

        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; }
        }

        public string CompanyTaxCode
        {
            get { return this.companyTaxCode; }
            set { this.companyTaxCode = value; }
        }

        public int? IsStopFollow
        {
            get { return this.isStopFollow; }
            set { this.isStopFollow = value; }
        }

        [NotMap]
        public string CustomerGroupName
        {
            get { return this.customerGroupName; }
            set { this.customerGroupName = value; }
        }

        [NotMap]
        public List<string> ErrorList { get; set; }
        #endregion

        #region Method

        #endregion
    }
}
