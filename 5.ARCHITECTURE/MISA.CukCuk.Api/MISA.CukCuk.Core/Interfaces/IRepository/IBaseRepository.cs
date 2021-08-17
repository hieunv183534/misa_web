using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.IRepository
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(MISAEntity entity);

        /// <summary>
        /// Sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy theo một prop
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        MISAEntity GetByProp(string propName, object propValue);
    }
}
