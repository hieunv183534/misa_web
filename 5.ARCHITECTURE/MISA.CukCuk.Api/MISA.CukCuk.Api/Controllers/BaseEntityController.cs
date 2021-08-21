using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {

        IBaseService<MISAEntity> _baseService;

        public BaseEntityController(IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
        }

        #region Lấy toàn bộ ds bản ghi

        /// <summary>
        /// Lấy toàn bộ danh sách 
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// Author hieunv 16/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            var serviceResult = _baseService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Lấy thông tin 1 bản ghi theo id

        /// <summary>
        /// Lấy thông tin theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Khách hàng</returns>
        /// Author hieunv 16/08/2021
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            var serviceResult = _baseService.GetById(entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Thêm mới một bản ghi

        /// <summary>
        /// Thêm mới một bản ghi vào database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>số hàng được thêm thành công</returns>
        /// Author hieunv 16/08/2021
        [HttpPost]
        public virtual IActionResult Post([FromBody] MISAEntity entity)
        {
            var serviceResult = _baseService.Add(entity);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Cập nhật một bản ghi có id được chọn

        /// <summary>
        /// Cập nhật một bản ghi với entityId trước
        /// </summary>
        /// <param name="entityId"> lấy từ route</param>
        /// <param name="entity"> lấy từ body</param>
        /// <returns>1 nếu sửa thành công và 0 là ngược lại</returns>
        /// Author hieunv 16/08/2021
        [HttpPut("{entityId}")]
        public virtual IActionResult Put([FromRoute] Guid entityId, [FromBody] MISAEntity entity)
        {
            var serviceResult = _baseService.Update(entity, entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion

        #region Xóa một bản ghi theo id

        /// <summary>
        /// Xóa một bản ghi theo entityId
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>1 nếu xóa thành công và 0 là ngược lại</returns>
        /// Author hieunv 16/08/2021
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            var serviceResult = _baseService.Delete(entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        #endregion
    }
}
