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

        protected IBaseRepository<MISAEntity> _baseRepository;
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
                var isValidMode = Validate(entity, "add");

                switch (isValidMode)
                {
                    case 1:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_NullField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 2:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
                            userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 3:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    default:
                        break;
                }

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
        public virtual ServiceResult GetAll()
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

                // xử lí nghiệp vụ thêm
                var isValidMode = Validate(entity, "update");

                switch (isValidMode)
                {
                    case 1:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_NullField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 2:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
                            userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 3:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    default:
                        break;
                }

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mode"></param>
        /// <returns>0: hợp lệ; 1:trống; 2: trùng; 3: sai định dạng</returns>
        private int Validate(MISAEntity entity, string mode)
        {
            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                // kiểm tra trường bắt buộc nhập reqiued !!!! 1
                if (prop.IsDefined(typeof(Requied), false))
                {
                    var propValue = prop.GetValue(entity);
                    if ((propValue == null) ||((string)propValue == ""))
                    {
                        return 1;
                    }
                    // kiểm tra trường không cho phép trùng !!!! 2
                }
                if (prop.IsDefined(typeof(NotAllowDuplicate), false))
                {
                    var entityDuplicate = _baseRepository.GetByProp(prop.Name, prop.GetValue(entity));
                    if (mode == "add" && entityDuplicate != null)
                    {
                        return 2;
                    }
                    else if (mode == "update")
                    {
                        if (
                            entityDuplicate.GetType().GetProperty($"{typeof(MISAEntity).Name}Id").GetValue(entityDuplicate)
                            != entity.GetType().GetProperty($"{typeof(MISAEntity).Name}Id").GetValue(entity)
                            )
                        {
                            return 2;
                        }
                    }
                    // kiểm tra email hợp lệ  !!!! 3
                }
                if (prop.Name == "Email")
                {
                    if (!Common.IsValidEmail((string)prop.GetValue(entity)))
                    {
                        return 3;
                    }
                }
            }
            return 0;
        }

        #region AddMany

        /// <summary>
        /// Xử lí nghiệp vụ thêm nhiều
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public ServiceResult AddMany(List<MISAEntity> entities)
        {
            foreach (var entity in entities)
            {
                var isValidMode = Validate(entity, "add");
                switch (isValidMode)
                {
                    case 1:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_NullField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_NullField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 2:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_DuplicateFiled,
                            userMsg = Resources.ResourceVN.MISA_Error_User_DuplicateField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    case 3:
                        _serviceResult.Data = new
                        {
                            devMsg = Resources.ResourceVN.MISA_Error_Dev_InvalidField,
                            userMsg = Resources.ResourceVN.MISA_Error_User_InvalidField,
                        };
                        _serviceResult.StatusCode = 400;
                        return _serviceResult;
                    default:
                        break;
                }
            }

            // lưu tất cả cac bản ghi vào db
            var rowAffect = _baseRepository.AddMany(entities);
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

        #endregion

    }
}
