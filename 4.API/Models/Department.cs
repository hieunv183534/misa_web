using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Models
{
    public class Department
    {

        #region Field

        private Guid departmentId;
        private string departmentCode;
        private string departmentName;
        private string description;

        #endregion

        #region Constructor

        public Department()
        {

        }

        public Department(string departmentCode, string departmentName)
        {
            this.departmentId = Guid.NewGuid();
            this.departmentCode = departmentCode;
            this.departmentName = departmentName;
        }

        public Department(string departmentCode, string departmentName, string description) : this(departmentCode, departmentName)
        {
            this.description = description;
        }

        #endregion

        #region Property
        /// <summary>
        /// id phòng ban
        /// </summary>
        public Guid DepartmentId
        {
            get { return this.departmentId; }
            set { this.departmentId = value; }
        }

        /// <summary>
        /// mã phòng ban
        /// </summary>
        public string DepartmentCode
        {
            get { return this.departmentCode; }
            set { this.departmentCode = value; }
        }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName
        {
            get { return this.departmentName; }
            set { this.departmentName = value; }
        }

        /// <summary>
        /// Sự mô tả phòng ban
        /// </summary>
        public string Descriptition
        {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region Method

        #endregion

    }
}
