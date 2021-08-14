using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class CustomerGroup : BaseEntity
    {
        #region Field

        private Guid customerGroupId;
        private string customerGroupName;
        private string description;

        #endregion

        #region Constructor

        public CustomerGroup()
        {

        }

        public CustomerGroup(string customerGroupName, string description)
        {
            this.customerGroupId = Guid.NewGuid();
            this.customerGroupName = customerGroupName;
            this.description = description;
        }

        #endregion

        #region Property

        /// <summary>
        /// id nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId
        {
            get { return this.customerGroupId; }
            set { this.customerGroupId = value; }
        }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName
        {
            get { return this.customerGroupName; }
            set { this.customerGroupName = value; }
        }

        /// <summary>
        /// Sự mô tả nhóm khách hàng
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
