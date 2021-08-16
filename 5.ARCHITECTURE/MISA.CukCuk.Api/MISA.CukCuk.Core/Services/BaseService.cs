using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using MISA.CukCuk.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {

        IBaseRepository<MISAEntity> _baseRepository;
        public ServiceResult _serviceResult;

        //Ctor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        #region Add

        /// <summary>
        /// Xử lí nghiệp vụ thêm 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual ServiceResult Add(MISAEntity entity)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Add(entity);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Xử lí nghiệp vụ xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult Delete(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.Delete(entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Xử lí nghiệp vụ lấy tất cả
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetAll()
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _baseRepository.GetAll();
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        #endregion

        #region GetById
        /// <summary>
        /// Xử lí nghiệp vụ lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult GetById(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ lấy 1 dữ liệu
                // lấy bản ghi theo id
                var entity = _baseRepository.GetById(entityId);
                if (entity != null)
                {
                    _serviceResult.Data = entity;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// Xử lí nghiệp vụ sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public virtual ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ sửa
                // cập nhật dữ liệu vào db
                var rowAffect = _baseRepository.Update(entity, entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.MISA_Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        #endregion

    }
}
