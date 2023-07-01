using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Interfaces.Services;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Application.ViewModels.FilterModels;
using Application.ViewModels.Drivers;
using Application.Services;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class DriverController : BaseController, IWebController<Driver>
    {
        private readonly IDriverService _driverService;
        private readonly IClaimsService _claimService;

        public DriverController(IDriverService driverService, IClaimsService claimService)
        {
            _driverService = driverService;
            _claimService = claimService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(Application.ViewModels.Drivers.DriverRegisterDTO registerObject)
        {
            var checkExist = await _driverService.CheckEmail(registerObject);
            if (checkExist)
            {
                return BadRequest(new
                {
                    Message = "Email has existed, please try again"
                });
            }
            else
            {
                var checkReg = await _driverService.RegisterAsync(registerObject);
                if (checkReg)
                {
                    return Ok(new
                    {
                        Message = "Register Success"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "Register fail"
                    });
                }
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(DriverRequestDTO entity)
        {
            var result = await _driverService.AddAsync(entity);
            return result ? Ok(new
            {
                message = "Add Successfully"
            }) : BadRequest();
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> Update(Guid id, DriverRequestUpdateDTO entity)
        {
            if (id == Guid.Empty) id = _claimService.GetCurrentUserId;
            var exist = await ExistDriver(id);
            if (!exist) return BadRequest();

            bool result = false;
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            if (role.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                result = await _driverService.Update(id, entity);
            }
            else if (role.Value.Equals("Driver", StringComparison.OrdinalIgnoreCase))
            {
                if (id != _claimService.GetCurrentUserId) return Unauthorized(new
                {
                    Message = "Can't update other driver profile"
                });

                result = await _driverService.Update(id, entity);
            }
            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : BadRequest();
        }
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> GetByIDAsync(Guid id)
        {
            var result = await _driverService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var exist = await ExistDriver(id);
            if (!exist) return BadRequest();

            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            var result = false;
            if (role.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                result = await _driverService.RemoveAsync(id);
            }
            else if (role.Value.Equals("Driver", StringComparison.OrdinalIgnoreCase))
            {
                if (id != _claimService.GetCurrentUserId) return Unauthorized(new
                {
                    Message = "Can't delete other driver"
                });
                result = await _driverService.RemoveAsync(id);
            }
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _driverService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListWithFilter(DriverFilteringModel? entity,
                                                    int pageIndex = 0,
                                                    int pageSize = 10)
        {
            var result = await _driverService.GetFilterAsync(entity, pageIndex, pageSize);
            return result != null ? Ok(result) : NotFound();
        }
        private async Task<bool> ExistDriver(Guid id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null) return false;
            return true;
        }
    }
}