using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Models
{
    public class Employee : Person
    {
        #region Field

        private Guid employeeId;
        private string employeeCode;
        private string identityNumber;
        private DateTime? identityDate;
        private string identityPlace;
        private DateTime? joinDate;
        private int? martialStatus;
        private int? educationalBackground;
        private Guid? qualificationId;
        private Guid? departmentId;
        private Guid? positionId;
        private int? workStatus;
        private string personalTaxCode;
        private decimal? salary;

        

        #endregion

        #region Constructor

        public Employee()
        {
            this.employeeId = new Guid();
        }

        //constructor không nhận prop của base
        // chỉ nhận tt bắt buộc
        public Employee(string employeeCode, string identityNumber,
            string fullName, string email, string phoneNumber) : base(fullName, email, phoneNumber)
        {
            this.employeeId = new Guid();
            this.employeeCode = employeeCode;
            this.identityNumber = identityNumber;
        }

        // nhận tất cả thuộc tính
        public Employee(string employeeCode, string firstName, string lastName, string fullName, int? gender,
            DateTime? dateOfBirth, string phoneNumber, string email, string address, string identityNumber,
            DateTime? identityDate, string identityPlace, DateTime? joinDate, int? martialStatus, int? educationalBackground,
            Guid? qualificationId, Guid? departmentId, Guid? positionId, int? workStatus, string personalTaxCode, decimal? salary)
            : base(firstName, lastName, fullName, gender, dateOfBirth, address, email, phoneNumber)
        {
            this.employeeId = new Guid();
            this.employeeCode = employeeCode;
            this.identityNumber = identityNumber;
            this.identityDate = identityDate;
            this.identityPlace = identityPlace;
            this.joinDate = joinDate;
            this.martialStatus = martialStatus;
            this.educationalBackground = educationalBackground;
            this.qualificationId = qualificationId;
            this.departmentId = departmentId;
            this.positionId = positionId;
            this.workStatus = workStatus;
            this.personalTaxCode = personalTaxCode;
            this.salary = salary;
        }

        //constructor nhận cả prop của base
        // dài quá sau viết

        #endregion

        #region Property

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid EmployeeId
        {
            get { return this.employeeId; }
            set { this.employeeId = value; }
        }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode
        {
            get { return this.employeeCode; }
            set { this.employeeCode = value; }
        }

        /// <summary>
        /// Số cmnd/cccd
        /// </summary>
        public string IdentityNumber
        {
            get { return this.identityNumber; }
            set { this.identityNumber = value; }
        }

        /// <summary>
        /// Ngày cấp cmnd/cccd
        /// </summary>
        public DateTime? IdentityDate
        {
            get { return this.identityDate; }
            set { this.identityDate = value; }
        }

        /// <summary>
        /// Nơi cấp cmnd/cccd
        /// </summary>
        public string IdentityPlace
        {
            get { return this.identityPlace; }
            set { this.identityPlace = value; }
        }

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        public DateTime? JoinDate
        {
            get { return this.joinDate; }
            set { this.joinDate = value; }
        }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int? MartialStatus
        {
            get { return this.martialStatus; }
            set { this.martialStatus = value; }
        }

        /// <summary>
        /// Trình độ học vấn
        /// </summary>
        public int? EducationalBackground
        {
            get { return this.educationalBackground; }
            set { this.educationalBackground = value; }
        }

        /// <summary>
        /// Trình độ chuyên môn id
        /// </summary>
        public Guid? QualificationId
        {
            get { return this.qualificationId; }
            set { this.qualificationId = value; }
        }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid? DepartmentId
        {
            get { return this.departmentId; }
            set { this.departmentId = value; }
        }

        /// <summary>
        /// id Vị trí
        /// </summary>
        public Guid? PositionId
        {
            get { return this.positionId; }
            set { this.positionId = value; }
        }

        /// <summary>
        /// Tình trang làm việc: 0-đã nghỉ việc; 1-đang làm việc; 2-đan thử việc
        /// </summary>
        public int? WorkStatus
        {
            get { return this.workStatus; }
            set { this.workStatus = value; }
        }

        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode
        {
            get { return this.personalTaxCode; }
            set { this.personalTaxCode = value; }
        }

        /// <summary>
        /// Lương cơ bản
        /// </summary>
        public decimal? Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
