using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Models
{
    public class BaseEntity
    {
        #region Field
        private DateTime? createdDate;
        private string createdBy;
        private DateTime? modifiedDate;
        private string modifiedBy;
        #endregion

        #region Constructor
        public BaseEntity()
        {

        }

        public BaseEntity(DateTime? createdDate, string createdBy, DateTime? modifiedDate, string modifiedBy)
        {
            this.createdDate = createdDate;
            this.createdBy = createdBy;
            this.modifiedDate = modifiedDate;
            this.modifiedBy = modifiedBy;
        }
        #endregion

        #region Property

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate
        {
            get { return this.modifiedDate; }
            set { this.modifiedDate = value; }
        }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy
        {
            get { return this.modifiedBy; }
            set { this.modifiedBy = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
