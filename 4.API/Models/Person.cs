using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Models
{
    public class Person:BaseEntity
    {
        #region Field
        private string firstName;
        private string lastName;
        private string fullName;
        private int? gender;
        private DateTime? dateOfBirth;
        private string address;
        private string email;
        private string phoneNumber;
        #endregion

        #region Constructor

        public Person()
        {

        }

        // costructor những trường bắt buộc
        public Person(string fullName, string email, string phoneNumber)
        {
            this.fullName = fullName;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        // constructor tất cả các trường
        public Person(string firstName, string lastName, string fullName, int? gender,
            DateTime? dateOfBirth, string address, string email, string phoneNumber) : this(fullName, email, phoneNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
        }

        //constructor những trường bắt buộc + base
        public Person(string fullName, string email, string phoneNumber, 
            DateTime? createdDate, string createdBy, DateTime? modifiedDate, string modifiedBy)
            :base(createdDate,createdBy,modifiedDate,modifiedBy)
        {
            this.fullName = fullName;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        // constructor tất cả các trường + base
        public Person(string firstName, string lastName, string fullName, int? gender,
            DateTime? dateOfBirth, string address, string email, string phoneNumber,
            DateTime? createdDate, string createdBy, DateTime? modifiedDate, string modifiedBy) 
            : this(fullName, email, phoneNumber, createdDate, createdBy, modifiedDate, modifiedBy)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
        }



        #endregion

        #region Property

        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        /// <summary>
        /// Tên
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        /// <summary>
        /// Giới tính: 0-nữ; 1-nam; 2-không xác định
        /// </summary>
        public int? Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
