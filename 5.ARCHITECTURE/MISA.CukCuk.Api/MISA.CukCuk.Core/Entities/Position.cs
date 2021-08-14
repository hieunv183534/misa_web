using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Position : BaseEntity
    {
        #region Field

        private Guid positionId;
        private string positionCode;
        private string positionName;
        private string description;
        private Guid? parentId;

        #endregion

        #region Constructor

        public Position()
        {

        }

        // constructor không khởi tạo các thuộc tính từ base
        public Position(string positionCode, string positionName)
        {
            this.positionId = Guid.NewGuid();
            this.positionCode = positionCode;
            this.positionName = positionName;
        }


        public Position(string positionCode, string positionName, string description, Guid? parentId) : this(positionCode, positionName)
        {
            this.description = description;
            this.parentId = parentId;
        }

        // constructor khởi tạo cả các thuộc tính từ base

        #endregion

        #region Property

        /// <summary>
        /// Id vị trí
        /// </summary>
        public Guid PositionId
        {
            get { return this.positionId; }
            set { this.positionId = value; }
        }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string PositionCode
        {
            get { return this.positionCode; }
            set { this.positionCode = value; }
        }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName
        {
            get { return this.positionName; }
            set { this.positionName = value; }
        }

        /// <summary>
        /// Sự mô tả vị trí
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// id parent
        /// </summary>
        public Guid? ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
