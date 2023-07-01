using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Application.Services;
using Application.Commons;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.UserViewModels;
using Application.ViewModels.Customer;
using Application.ViewModels.FilterModels;

namespace WebAPI.Controllers
{
    public class CustomerController : BaseController, IWebController<Customer>
    {
        private readonly ICustomerService _customerService;
        private readonly IBaseUserService _userService;
        private readonly IClaimsService _claimService;

        public CustomerController(ICustomerService customerService, IBaseUserService userService, IClaimsService claimService)
        {
            _customerService = customerService;
            _userService = userService;
            _claimService = claimService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(CustomerRegisterDTO registerObject)
        {
            var checkExist = await _customerService.CheckEmail(registerObject);
            if (checkExist)
            {
                return BadRequest(new
                {
                    Message = "Email has existed, please try again"
                });
            }
            else
            {
                var checkReg = await _customerService.RegisterAsync(registerObject);
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
        public async Task<IActionResult> Add(CustomerRequestDTO entity)
        {
            var result = await _customerService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> Update(Guid id, CustomerRequestUpdateDTO entity)
        {
            if (id == Guid.Empty) id = _claimService.GetCurrentUserId;//Update self

            var exist = await ExistCustomer(id);
            if (!exist) return BadRequest();// if customer exist proceed

            bool result = false;
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            if (role.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                result = await _customerService.UpdateAsync(id, entity);
            }
            else if (role.Value.Equals("Customer", StringComparison.OrdinalIgnoreCase))
            {
                if (id != _claimService.GetCurrentUserId) return Unauthorized(new
                {
                    Message = "Can't update other user profile"
                });


                result = await _customerService.UpdateAsync(id, entity);
            }
            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : BadRequest();
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid id)
        {
            var result = await _customerService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var exist = await ExistCustomer(id);
            if (!exist) return BadRequest();// if customer exist proceed

            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("role"));
            var result = false;
            if (role.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                result = await _customerService.RemoveAsync(id);
            }
            else if (role.Value.Equals("Customer", StringComparison.OrdinalIgnoreCase))
            {
                if (id != _claimService.GetCurrentUserId) return Unauthorized(new
                {
                    Message = "Can't delete other user"
                });
                result = await _customerService.RemoveAsync(id);
            }
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _customerService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListWithFilter(CustomerFilteringModel? entity,
                                                    int pageIndex = 0,
                                                    int pageSize = 10)
        {
            var result = await _customerService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }

        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _customerService.GetCustomerListPagi(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        private async Task<bool> ExistCustomer(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return false;
            return true;
        }
    }
}